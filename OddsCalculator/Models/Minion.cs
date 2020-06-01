using System;

namespace OddsCalculator.Models
{
    public class Minion
    {
        public int BoardIndex { get; set; }

        public MinionType Type { get; set; }
        
        public int Attack { get; set; }

        public int Health { get; set; }

        public bool IsPoisonous { get; set; }

        public bool HasDivineShield { get; set; }

        public bool HasTaunt { get; set; }

        public event EventHandler OnBeingAttacked;

        public event EventHandler OnAttack;

        public event EventHandler OnDeath;
        
        public event EventHandler OnOverKill;

        public event EventHandler OnMinionSummon;

        public event EventHandler OnMinionDeath;

        public event EventHandler OnRoundStart;

        public void AttackEnemy(Minion otherMinion)
        {
            OnAttack?.Invoke(null, EventArgs.Empty);

            if (HasDivineShield)
            {
                HasDivineShield = false;
            }
            else
            {
                Health -= otherMinion.Attack;
            }

            if (otherMinion.HasDivineShield)
            {
                otherMinion.HasDivineShield = false;
            }
            else
            {
                otherMinion.Health -= Attack;
            }
        }

        public Minion Clone()
        {
            return (Minion) MemberwiseClone();
        }

        public void Kill()
        {
            OnDeath?.Invoke(null, EventArgs.Empty);
        }
    }
}