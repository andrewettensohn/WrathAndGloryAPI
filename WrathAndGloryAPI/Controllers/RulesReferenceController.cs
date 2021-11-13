using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrathAndGloryAPI.Interfaces;
using WrathAndGloryModels;

namespace WrathAndGloryAPI.Controllers
{
    [ApiController]
    [Route("rulesReference")]
    public class RulesReferenceController : ControllerBase
    {
        private readonly IRulesReferenceRepository _repository;

        public RulesReferenceController(IRulesReferenceRepository rulesReferenceRepository)
        {
            _repository = rulesReferenceRepository;
        }

        [HttpGet("getAll")]
        public IActionResult GetRuleReferences()
        {
            try
            {
                List<RuleReference> ruleReferences = _repository.GetRuleReferences();

                return Ok(ruleReferences);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("add")]
        public IActionResult AddRuleReference(RuleReference ruleReference)
        {
            try
            {
                _repository.AddRuleReference(ruleReference);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("update")]
        public IActionResult UpdateRuleReference(RuleReference ruleReference)
        {
            try
            {
                _repository.UpdateRuleReference(ruleReference);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteRuleReference(Guid id)
        {
            try
            {
                RuleReference reference = _repository.GetReference(id);
                _repository.DeleteRuleReference(reference);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
