using System;
using System.Collections.Generic;
using System.Text;

namespace WrathAndGloryModels
{
    public class RuleReference
    {
        public Guid Id { get; set; }

        public RuleReferenceType RuleReferenceType { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Body { get; set; }

        public string SecondaryBody { get; set; }
    }
}
