using System;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Providers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.Write("{0}", message);
        }
    }
}
