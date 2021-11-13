using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrathAndGloryModels;

namespace WrathAndGloryAPI.Interfaces
{
    public interface IRulesReferenceRepository
    {
        List<RuleReference> GetRuleReferences();

        RuleReference GetReference(Guid id);

        void AddRuleReference(RuleReference ruleReference);

        void UpdateRuleReference(RuleReference ruleReference);

        void DeleteRuleReference(RuleReference ruleReference);
    }
}
