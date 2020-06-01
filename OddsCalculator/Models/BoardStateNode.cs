using System.Collections.Generic;

namespace OddsCalculator.Models
{
    public class BoardStateNode
    {
        public BoardState Value { get; set; }

        public BoardStateNode Parent { get; set; }

        public List<BoardStateNode> Children { get; set; } = new List<BoardStateNode>();
    }
}