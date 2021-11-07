using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrathAndGloryModels.Interfaces;

namespace WrathAndGloryModels
{
    public class SyncModel : ISyncModel
    {
        [Key]
        public Guid Id { get; set; }

        public ModelType ModelType { get; set; }

        public string Json { get; set; }

        public DateTime LastUpdateDateTime { get; set; }
    }
}
