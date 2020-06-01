using System;

namespace OddsCalculator.Models
{
    public class Minion
    {
        public MinionType Type { get; set; }
        
        public uint Attack { get; set; }

        public uint Health { get; set; }

        public bool IsPoisonous { get; set; }

        public bool HasDivineShield { get; set; }

        public event EventHandler OnBeingAttacked;

        public event EventHandler OnAttack;

        public event EventHandler OnDeath;

        public event EventHandler OnMinionSummon;

        public event EventHandler OnMinionDeath;
    }
}