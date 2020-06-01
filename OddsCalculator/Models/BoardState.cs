using System.Collections.Generic;

namespace OddsCalculator.Models
{
    public class BoardState
    {
        public List<Minion> FriendlyMinions;

        public List<Minion> EnemyMinions;

        public double Probability { get; set; }
    }
}