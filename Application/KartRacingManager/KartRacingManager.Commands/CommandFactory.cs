using System;
using System.Linq;
using System.Reflection;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;
using KartRacingManager.Interfaces.Data;
using Bytes2you.Validation;

namespace KartRacingManager.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IMainDbContext mainDbContext;
        private readonly IWriter writer;
        

        public CommandFactory(IMainDbContext mainDbContext, IWriter writer)
        {
            Guard.WhenArgument(mainDbContext, "mainDbContext").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainDbContext = mainDbContext;
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
                ConstructorInfo constructor;

                constructor = commandTypeInfo.GetConstructor(new Type[] {
                    typeof(IMainDbContext),
                    typeof(IWriter)
                });
                if (constructor != null)
                {
                    ICommand command = constructor.Invoke(new object[]
                    {
                        this.mainDbContext, this.writer
                    }) as ICommand;
                    return command;
                }

                constructor = commandTypeInfo.GetConstructor(new Type[] { typeof(IWriter) });
                if (constructor != null)
                {
                    ICommand command = constructor.Invoke(new object[] { this.writer }) as ICommand;
                    return command;
                }

                throw new InvalidOperationException($"Command {commandName} found but no appropriate constructor found");
            }
        }
    }
}
