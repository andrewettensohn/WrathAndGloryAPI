using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrathAndGloryAPI.Interfaces;
using WrathAndGloryModels;
using WrathAndGloryModels.Extensions;
using WrathAndGloryModels.Interfaces;

namespace WrathAndGloryAPI.Controllers
{
    [ApiController]
    [Route("syncModel")]
    public class SyncModelController : ControllerBase
    {
        private readonly ISyncModelRepository _repository;

        public SyncModelController(ISyncModelRepository syncModelRepository)
        {
            _repository = syncModelRepository;
        }


        [HttpGet("getAll")]
        public IActionResult GetSyncModels()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("getModelType/{modelType}")]
        public IActionResult GetModelType(ModelType modelType)
        {
            try
            {
                return Ok(_repository.Filter((x) => x.ModelType == modelType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("newCharacter")]
        public IActionResult AddNewCharacterSyncModel()
        {
            try
            {
                Character character = new Character
                {
                    Id = Guid.NewGuid(),
                    Name = "New Character",
                    Skills = new Skills(),
                    Attributes = new Attributes(),
                    Ammo = new Ammo(),
                    CharacterGear = new List<Gear>(),
                    Armor = new List<Armor>(),
                    Weapons = new List<Weapon>(),
                    Talents = new List<Talent>(),
                    PsychicPowers = new List<PyschicPower>(),
                };

                SyncModel syncModel = new SyncModel
                {
                    Id = character.Id,
                    LastUpdateDateTime = DateTime.Now,
                    ModelType = ModelType.Character,
                    Json = JsonConvert.SerializeObject(character)
                };

                _repository.Add(syncModel);

                return Ok(syncModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }            
        }

        [HttpPost("addOrUpdate")]
        public IActionResult AddOrUpdate(List<SyncModel> syncModels)
        {
            try
            {
                syncModels.ForEach(x =>
                {
                    if (x.Id == Guid.Empty)
                    {
                        x.Id = Guid.NewGuid();
                    }

                    JObject jsonModel = JObject.Parse(x.Json);

                    if (jsonModel["Id"].ToString() == Guid.Empty.ToString())
                    {
                        jsonModel["Id"] = x.Id.ToString();
                    }

                    x.Json = jsonModel.ToString();
                });

                _repository.AddOrUpdate(syncModels);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// If a SyncModel has been updated then characters may need to be updated since the old value is saved as JSON.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="effectedModelType"></param>
        /// <returns></returns>
        [HttpPost("validateCharacterModels/{id}/{effectedModelType}")]
        public IActionResult ValidateCharacterModels(Guid id, ModelType effectedModelType)
        {
            try
            {
                //If the syncModel is not present in the database anymore then it has been deleted
                SyncModel modifiedSyncModel = _repository.GetById(id);
                bool isDelete = modifiedSyncModel == null;

                //Get all characters
                List<SyncModel> syncModels = _repository.Filter(x => x.ModelType == ModelType.Character && x.Json.Contains(id.ToString()), true).ToList();
                List<Character> characters = syncModels.ConvertSyncModelsToCoreModels<Character>();

                List<SyncModel> updatedModels = new List<SyncModel>();

                foreach(Character character in characters)
                {
                    if (effectedModelType == ModelType.Gear)
                    {
                        ValidateCharacterCoreModelList(character.CharacterGear, modifiedSyncModel, isDelete, id);
                    }
                    else if (effectedModelType == ModelType.Armor)
                    {
                        ValidateCharacterCoreModelList(character.Armor, modifiedSyncModel, isDelete, id);
                    }
                    else if (effectedModelType == ModelType.Weapon)
                    {
                        ValidateCharacterCoreModelList(character.Weapons, modifiedSyncModel, isDelete, id);
                    }
                    else if (effectedModelType == ModelType.Talent)
                    {
                        ValidateCharacterCoreModelList(character.Talents, modifiedSyncModel, isDelete, id);
                    }
                    else if (effectedModelType == ModelType.Archetype && isDelete)
                    {
                        character.Archetype = null;
                    }
                    else if(effectedModelType == ModelType.Archetype && !isDelete)
                    {
                        character.Archetype = modifiedSyncModel.ConvertSyncModelToCoreModel<Archetype>();
                    }
                    else
                    {
                        continue;
                    }

                    updatedModels.Add(new SyncModel
                    {
                        Id = character.Id,
                        Json = JsonConvert.SerializeObject(character),
                        LastUpdateDateTime = DateTime.Now,
                        ModelType = ModelType.Character
                    });
                }

                _repository.AddOrUpdate(updatedModels);

                return Ok(updatedModels.Count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                SyncModel modelToDelete = _repository.GetById(id);

                if(modelToDelete == null)
                {
                    return NotFound("Unable to find a model for the provided ID.");
                }

                _repository.Delete(modelToDelete);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Removes the model from the character core model list if it no longer exists, otherwise update it
        /// </summary>
        /// <typeparam name="CoreModel"></typeparam>
        /// <param name="coreModelList"></param>
        /// <param name="modifedSyncModel"></param>
        /// <param name="isDelete"></param>
        /// <param name="modifedModelId"></param>
        private void ValidateCharacterCoreModelList<CoreModel>(List<CoreModel> coreModelList, SyncModel modifedSyncModel, bool isDelete, Guid modifedModelId)
            where CoreModel : ICoreModel
        {
            coreModelList.RemoveAll(x => x.Id == modifedModelId);

            if (!isDelete)
            {
                CoreModel modifedGear = modifedSyncModel.ConvertSyncModelToCoreModel<CoreModel>();
                coreModelList.Add(modifedGear);
            }
        }

    }
}
