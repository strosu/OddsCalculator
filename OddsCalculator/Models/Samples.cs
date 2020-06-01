using System.Collections.Generic;

namespace OddsCalculator.Models
{
    public static class Samples
    {
        public static BoardState SmallEqualBoards = new BoardState
        {
            FriendlyMinions = new List<Minion>
            {                
                new Minion
                {
                    Attack = 2,
                    Health = 2
                },
                new Minion
                {
                    Attack = 1,
                    Health = 1
                }
            },
            EnemyMinions = new List<Minion>
            {
                new Minion
                {
                    Attack = 2,
                    Health = 2
                },
                new Minion
                {
                    Attack = 1,
                    Health = 1
                }
            }
        };

        public static BoardState SmallWithHealth = new BoardState
        {
            FriendlyMinions = new List<Minion>
            {
                new Minion
                {
                    Attack = 1,
                    Health = 2
                }
            },
            EnemyMinions = new List<Minion>
            {
                new Minion
                {
                    Attack = 1,
                    Health = 2
                }
                }
        };
    }
}