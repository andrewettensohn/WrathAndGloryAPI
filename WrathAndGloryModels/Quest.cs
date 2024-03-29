﻿using WrathAndGloryModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathAndGloryModels
{
    public class Quest : ICoreModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsMainQuest { get; set; }

        public bool IsComplete { get; set; }

        public string BriefDescription { get; set; }

        public string FullDescription { get; set; }
    }
}
