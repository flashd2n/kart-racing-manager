using System;
using System.Linq;
using System.Reflection;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IWriter writer;

        public CommandFactory(IWriter writer)
        {
            this.writer = writer;
        }

        public ICommand GetCommand(string commandName)
        {
            Assembly currentAssembly = this.GetType().GetTypeInfo().Assembly;
            var commandTypeInfo = currentAssembly.DefinedTypes
                .Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(ICommand)))
                .SingleOrDefault(type => type.Name.ToLower() == (commandName.ToLower() + "command"));

            if (commandTypeInfo == null)
            {
                return null;
            }
            else
            {
                ConstructorInfo constructor = commandTypeInfo.GetConstructor(new Type[] {
                    typeof(IWriter)
                });
                ICommand command = constructor.Invoke(new object[] {this.writer}) as ICommand;
                return command;
            }
        }
    }
}
