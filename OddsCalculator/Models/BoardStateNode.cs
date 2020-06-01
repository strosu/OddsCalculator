using System.Collections.Generic;

namespace OddsCalculator.Models
{
    public class BoardStateNode
    {
        public BoardState Value { get; set; }

        public BoardState Parent { get; set; }

        public List<BoardState> Children { get; set; }
    }
}