using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrathAndGloryAPI.Interfaces;
using WrathAndGloryModels;

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
                _repository.AddOrUpdate(syncModels);
                return Ok();
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
                _repository.Delete(modelToDelete);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
