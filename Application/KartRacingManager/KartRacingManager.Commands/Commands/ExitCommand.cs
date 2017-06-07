using System;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;
using Bytes2you.Validation;

namespace KartRacingManager.Commands.Commands
{
    public class ExitCommand : ICommand
    {
        private readonly IWriter writer;

        public ExitCommand(IWriter writer)
        {
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            this.writer.Write("Goodbye!");
            this.writer.Write(Environment.NewLine);
        }
    }
}

