using System;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;
using Bytes2you.Validation;

namespace KartRacingManager.Commands.Commands
{
    public class EchoCommand : ICommand
    {
        private readonly IWriter writer;

        public EchoCommand(IWriter writer)
        {
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            writer.Write("Echo command start:");
            writer.Write(Environment.NewLine);
            for (int i = 0; i < commandParameters.Length; i++)
            {
                string message = string.Format($"Param {i}: {commandParameters[i]}{Environment.NewLine}");
                writer.Write(message);
            }

            writer.Write("Echo command end.");
            writer.Write(Environment.NewLine);
        }
    }
}
