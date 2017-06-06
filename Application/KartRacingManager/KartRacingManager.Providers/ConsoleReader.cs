using System;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Providers
{
    public class ConsoleReader : IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
