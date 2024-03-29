﻿using WrathAndGloryModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrathAndGloryModels
{
    public class Archetype : ICoreModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int XPCost { get; set; }

        public int Tier { get; set; }

        public string ArchetypeAbility { get; set; }

        public int AttributeBonus { get; set; }

        public int SkillBonus { get; set; }

        public int Influence { get; set; }

        public string Keywords { get; set; }
    }
}
