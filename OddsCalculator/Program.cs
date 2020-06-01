using System;
using OddsCalculator.Models;

namespace OddsCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //var calculator = new Engine.OddsCalculator(Samples.SmallWithHealth);
            //Console.WriteLine(calculator.GetCurrentWinChance());

            var calculator2 = new Engine.OddsCalculator(Samples.SmallEqualBoards);
            Console.WriteLine(calculator2.GetCurrentWinChance());
        }
    }
}
