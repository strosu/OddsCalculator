using System;
using OddsCalculator.Models;

namespace OddsCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Engine.OddsCalculator(new BoardState());
            Console.WriteLine(calculator.GetCurrentWinChance());
        }
    }
}
