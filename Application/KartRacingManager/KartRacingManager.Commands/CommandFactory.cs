using System;
using System.Linq;
using System.Reflection;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;
using Bytes2you.Validation;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Imports;
using KartRacingManager.Interfaces.Exports;

namespace KartRacingManager.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IKartsUnitOfWork kartsUnitOfWork;
        private readonly IWriter writer;
        private readonly IJsonImporter jsonImporter;
        private readonly IXmlImporter xmlImporter;
        private readonly IExporter exporter;


        public CommandFactory(IMainUnitOfWork mainUnitOfWork, IKartsUnitOfWork kartsUnitOfWork, IWriter writer, IJsonImporter jsonImporter, IXmlImporter xmlImporter/*, IExporter exporter*/)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(kartsUnitOfWork, "kartsUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            Guard.WhenArgument(jsonImporter, "jsonImporter").IsNull().Throw();
            Guard.WhenArgument(xmlImporter, "xmlImporter").IsNull().Throw();
            //Guard.WhenArgument(exporter, "exporter").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
            this.jsonImporter = jsonImporter;
            this.xmlImporter = xmlImporter;
            this.kartsUnitOfWork = kartsUnitOfWork;
            //this.exporter = exporter;
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
                    typeof(IMainUnitOfWork),
                    typeof(IWriter)
                });
                if (constructor != null)
                {
                    ICommand command = constructor.Invoke(new object[]
                    {
                        this.mainUnitOfWork, this.writer
                    }) as ICommand;
                    return command;
                }

                constructor = commandTypeInfo.GetConstructor(new Type[] {
                    typeof(IMainUnitOfWork),
                    typeof(IWriter),
                    typeof(IJsonImporter)
                });
                if (constructor != null)
                {
                    ICommand command = constructor.Invoke(new object[]
                    {
                        this.mainUnitOfWork, this.writer, this.jsonImporter
                    }) as ICommand;
                    return command;
                }

                constructor = commandTypeInfo.GetConstructor(new Type[] {
                    typeof(IMainUnitOfWork),
                    typeof(IWriter),
                    typeof(IXmlImporter)
                });
                if (constructor != null)
                {
                    ICommand command = constructor.Invoke(new object[]
                    {
                        this.mainUnitOfWork, this.writer, this.xmlImporter
                    }) as ICommand;
                    return command;
                }

                constructor = commandTypeInfo.GetConstructor(new Type[] {
                    typeof(IMainUnitOfWork),
                    typeof(IWriter),
                    typeof(IExporter)
                });
                if (constructor != null)
                {
                    ICommand command = constructor.Invoke(new object[]
                    {
                        this.mainUnitOfWork, this.writer, this.exporter
                    }) as ICommand;
                    return command;
                }

                constructor = commandTypeInfo.GetConstructor(new Type[] {
                    typeof(IKartsUnitOfWork),
                    typeof(IWriter)
                });
                if (constructor != null)
                {
                    ICommand command = constructor.Invoke(new object[]
                    {
                        this.kartsUnitOfWork, this.writer
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
