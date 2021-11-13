using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrathAndGloryAPI.Interfaces;
using WrathAndGloryModels;

namespace WrathAndGloryAPI.Data
{
    public class RulesReferenceRepository : IRulesReferenceRepository
    {
        private readonly WrathAndGloryContext _context;

        public RulesReferenceRepository(WrathAndGloryContext context)
        {
            _context = context;
        }

        public List<RuleReference> GetRuleReferences()
        {
            return _context.RuleReferences.ToList();
        }

        public RuleReference GetReference(Guid id)
        {
            return _context.RuleReferences.FirstOrDefault(x => x.Id == id);
        }

        public void AddRuleReference(RuleReference ruleReference)
        {
            _context.RuleReferences.Add(ruleReference);
            _context.SaveChanges();
        }

        public void UpdateRuleReference(RuleReference ruleReference)
        {
            _context.RuleReferences.Update(ruleReference);
            _context.SaveChanges();
        }

        public void DeleteRuleReference(RuleReference ruleReference)
        {
            _context.RuleReferences.Remove(ruleReference);
            _context.SaveChanges();
        }
    }
}
