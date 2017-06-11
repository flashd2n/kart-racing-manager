using Bytes2you.Validation;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;
using System;
using System.Linq;
using System.Reflection;

namespace KartRacingManager.Commands.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly IWriter writer;

        public HelpCommand(IWriter writer)
        {
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            writer.Write("Command list:");
            writer.Write(Environment.NewLine);

            Assembly currentAssembly = this.GetType().GetTypeInfo().Assembly;
            var commandTypeInfos = currentAssembly.DefinedTypes
                .Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(ICommand)) && type.Name.ToLower().EndsWith("command"))
                .OrderBy(type => type.Name);

            foreach (var commandTypeInfo in commandTypeInfos)
            {
                // remove the "Command" at the end
                string commandName = commandTypeInfo.Name.Remove(commandTypeInfo.Name.Length - 7);
                writer.Write(commandName);
                writer.Write(Environment.NewLine);
            }
        }
    }
}
