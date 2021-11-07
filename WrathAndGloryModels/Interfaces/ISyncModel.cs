using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrathAndGloryModels.Interfaces
{
    public interface ISyncModel
    {
        public Guid Id { get; set; }
        public string Json { get; set; }

    }
}
