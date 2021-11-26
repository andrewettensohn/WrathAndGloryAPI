using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WrathAndGloryModels.Interfaces;

namespace WrathAndGloryModels
{
    public class Threat : ICoreModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Wounds { get; set; }

        public int Shock { get; set; }

        public int Defence { get; set; }

        public int Resilience { get; set; }

        public int Conviction { get; set; }

        public int Resolve { get; set; }

        public int Speed { get; set; }

        public string Size { get; set; }

        public string Description { get; set; }

        public string AvatarPath { get; set; }

        public Attributes Attributes { get; set; }

        public Skills Skills { get; set; }

        public List<Armor> Armor { get; set; }

        public List<Talent> Talents { get; set; }

        public List<Weapon> Weapons { get; set; }

        public List<PyschicPower> PsychicPowers { get; set; }
    }
}
