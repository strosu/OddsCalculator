using System;
using OddsCalculator.Models;

namespace OddsCalculator.Engine
{
    public class OddsCalculator
    {
        private readonly BoardState _startingState;
        private Lazy<BoardStateNode> _oddsTree;

        public OddsCalculator(BoardState startingState)
        {
            _startingState = startingState;
            _oddsTree = new Lazy<BoardStateNode>(CalculateOdds);
        }

        private BoardStateNode CalculateOdds()
        {
            return null;
        }

        public double GetCurrentWinChance()
        {
            return 0.0;
        }

        public void ProgressTo(BoardState state)
        {

        }
    }
}