using KartRacingManager.Interfaces.Providers;
using KartRacingManager.Interfaces.Commands;
using System;

namespace KartRacingManager.Commands.Commands
{
    public class ShwangShwingCommand : ICommand
    {
        private readonly IWriter writer;

        public ShwangShwingCommand(IWriter writer)
        {
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            writer.Write("ShwangShwing is the greatest programmer in the universe!!!1!11!!1");
            writer.Write(Environment.NewLine);
        }
    }
}
