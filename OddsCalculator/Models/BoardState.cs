using System;
using System.Collections.Generic;
using System.Linq;

namespace OddsCalculator.Models
{
    public class BoardState
    {
        public List<Minion> FriendlyMinions;

        public List<Minion> EnemyMinions;

        public AttacksNext AttacksNext;

        public int NextAttackingFriendlyIndex = 0;

        public int NextAttackingEnemyMinionIndex = 0;

        public double Probability { get; set; }

        public bool IsWin => EnemyMinions.Count == 0 && !IsDraw;

        public bool IsLoss => FriendlyMinions.Count == 0 && !IsDraw;

        public bool IsDraw => EnemyMinions.Count == 0 && FriendlyMinions.Count == 0;

        public bool IsLeaf => IsDraw || IsWin || IsLoss;

        public BoardState Clone()
        {
            return new BoardState
            {
                FriendlyMinions = FriendlyMinions.Select(x => x.Clone()).ToList(),
                EnemyMinions = EnemyMinions.Select(x => x.Clone()).ToList(),
                AttacksNext = AttacksNext,
                NextAttackingEnemyMinionIndex = NextAttackingEnemyMinionIndex,
                NextAttackingFriendlyIndex = NextAttackingFriendlyIndex,
                Probability = Probability
            };
        }

        public void RemoveDeadMinions()
        {
            RemoveDeadMinions(FriendlyMinions);
            RemoveDeadMinions(EnemyMinions);
        }

        private void RemoveDeadMinions(List<Minion> minions)
        {
            var deadMinions = new List<Minion>();

            foreach (var minion in minions.Where(minion => minion.Health <= 0))
            {
                minion.Kill();
                deadMinions.Add(minion);
            }

            foreach (var dead in deadMinions)
            {
                minions.Remove(dead);
            }
        }
    }
}