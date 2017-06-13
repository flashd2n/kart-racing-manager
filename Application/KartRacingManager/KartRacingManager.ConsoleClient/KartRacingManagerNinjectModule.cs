using KartRacingManager.Commands;
using KartRacingManager.Data;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Engine;
using KartRacingManager.Imports;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Engine;
using KartRacingManager.Interfaces.Imports;
using KartRacingManager.Interfaces.Logger;
using KartRacingManager.Interfaces.Providers;
using KartRacingManager.Providers;
using KartRacingManager.Logger;
using Ninject.Modules;
using KartRacingManager.Interfaces.Exports;
using KartRacingManager.Exports;
using KarRacingManager.Models;
using Ninject.Extensions.Factory;
using System.Linq;
using KartRacingManager.Commands.Commands;
using System;
using Ninject;

namespace KartRacingManager.ConsoleClient
{
    public class KartRacingManagerNinjectModule : NinjectModule
    {
        private const string CreateKartCommandName = "addkart";
        private const string CreateLapCommandName = "addlap";
        private const string CreateRaceCommandName = "addrace";
        private const string CreateRacerCommandName = "addracer";
        private const string AddRacerToRaceCommandName = "addracertorace";
        private const string CreateTrackCommandName = "addtrack";
        private const string CancelRaceCommandName = "cancelrace";
        private const string DeleteKartCommandName = "deletekart";
        private const string EchoCommandName = "echo";
        private const string ExitCommandName = "exit";
        private const string ExportRaceCommandName = "exportrace";
        private const string ExportRacerCommandName = "exportracer";
        private const string FinishRaceCommandName = "finishrace";
        private const string GetRaceInformationCommandName = "getraceinformation";
        private const string GetRacerInformationCommandName = "getracerinformation";
        private const string GetTrackInformationCommandName = "gettrackinformation";
        private const string HelpCommandName = "help";
        private const string ImportLapCommandName = "importlap";
        private const string ImportRacerCommandName = "importracer";
        private const string ImportTrackCommandName = "importtrack";

        private const string ListKartsCommandName = "listkarts";
        private const string ListRacersCommandName = "listracers";
        private const string ListRacesCommandName = "listraces";
        private const string ListTracksCommandName = "listtracks";
        private const string SetRacerHeightCommandName = "setracerheight";
        private const string SetRacerWeightCommandName = "setracerweight";
        private const string ShwangShwingCommandName = "shwangshwing";
        private const string StartRaceCommandName = "startrace";

        public override void Load()
        {
            var engine = this.Bind<IEngine>().To<ConsoleReadEngine>().InSingletonScope();
            this.Bind<IMainDbContext>().To<MainDbContext>().InSingletonScope();
            this.Bind<IMainUnitOfWork>().To<MainUnitOfWork>().InSingletonScope();
            this.Bind<IKartDbContext>().To<KartDbContext>().InSingletonScope();
            this.Bind<IKartsUnitOfWork>().To<KartsUnitOfWork>().InSingletonScope();
            this.Bind<IReader>().To<ConsoleReader>();
            this.Bind<IWriter>().To<ConsoleWriter>();
            this.Bind<ILogger>().To<SqliteLogger>();
            this.Bind<IDateTimeProvider>().To<DateTimeProvider>();
            this.Bind<ILogsUnitOfWork>().To<LogsUnitOfWork>();
            this.Bind<ILogsDbContext>().To<LogsDbContext>();
            this.Bind<IJsonImporter>().To<JsonImporter>();
            this.Bind<IXmlImporter>().To<XmlImporter>();
            this.Bind<IExporter>().To<PdfExporter>();

            this.Bind<IModelFactory>().ToFactory();
            this.Bind<IAwesomeCommandFactory>().ToFactory();

            this.Bind<ICommand>().To<AddKartCommand>().Named(CreateKartCommandName);
            this.Bind<ICommand>().To<AddLapCommand>().Named(CreateLapCommandName);
            this.Bind<ICommand>().To<AddRaceCommand>().Named(CreateRaceCommandName);
            this.Bind<ICommand>().To<AddRacerCommand>().Named(CreateRacerCommandName);
            this.Bind<ICommand>().To<AddRacerToRaceCommand>().Named(AddRacerToRaceCommandName);
            this.Bind<ICommand>().To<AddTrackCommand>().Named(CreateTrackCommandName);
            this.Bind<ICommand>().To<CancelRaceCommand>().Named(CancelRaceCommandName);
            this.Bind<ICommand>().To<DeleteKartCommand>().Named(DeleteKartCommandName);
            this.Bind<ICommand>().To<EchoCommand>().Named(EchoCommandName);
            this.Bind<ICommand>().To<ExitCommand>().Named(ExitCommandName);
            this.Bind<ICommand>().To<ExportRaceCommand>().Named(ExportRaceCommandName);
            this.Bind<ICommand>().To<ExportRacerCommand>().Named(ExportRacerCommandName);
            this.Bind<ICommand>().To<FinishRaceCommand>().Named(FinishRaceCommandName);
            this.Bind<ICommand>().To<GetRaceInformationCommand>().Named(GetRaceInformationCommandName);
            this.Bind<ICommand>().To<GetRacerInformationCommand>().Named(GetRacerInformationCommandName);
            this.Bind<ICommand>().To<GetTrackInformationCommand>().Named(GetTrackInformationCommandName);
            this.Bind<ICommand>().To<HelpCommand>().Named(HelpCommandName);
            this.Bind<ICommand>().To<ImportLapCommand>().Named(ImportLapCommandName);
            this.Bind<ICommand>().To<ImportRacerCommand>().Named(ImportRacerCommandName);
            this.Bind<ICommand>().To<ImportTrackCommand>().Named(ImportTrackCommandName);
            this.Bind<ICommand>().To<ListKartsCommand>().Named(ListKartsCommandName);
            this.Bind<ICommand>().To<ListRacersCommand>().Named(ListRacersCommandName);
            this.Bind<ICommand>().To<ListRacesCommand>().Named(ListRacesCommandName);
            this.Bind<ICommand>().To<ListTracksCommand>().Named(ListTracksCommandName);
            this.Bind<ICommand>().To<SetRacerHeightCommand>().Named(SetRacerHeightCommandName);
            this.Bind<ICommand>().To<SetRacerWeightCommand>().Named(SetRacerWeightCommandName);
            this.Bind<ICommand>().To<ShwangShwingCommand>().Named(ShwangShwingCommandName);
            this.Bind<ICommand>().To<StartRaceCommand>().Named(StartRaceCommandName);

            this.Bind<ICommand>().ToMethod(c =>
            {
                string commandName = (string)c.Parameters.Single().GetValue(c, null);

                commandName = commandName.ToLower();

                ICommand command = null;

                switch (commandName)
                {
                    case CreateKartCommandName:
                        command = c.Kernel.Get<ICommand>(CreateKartCommandName);
                        break;
                    case CreateLapCommandName:
                        command = c.Kernel.Get<ICommand>(CreateLapCommandName);
                        break;
                    case CreateRaceCommandName:
                        command = c.Kernel.Get<ICommand>(CreateRaceCommandName);
                        break;
                    case CreateRacerCommandName:
                        command = c.Kernel.Get<ICommand>(CreateRacerCommandName);
                        break;
                    case AddRacerToRaceCommandName:
                        command = c.Kernel.Get<ICommand>(AddRacerToRaceCommandName);
                        break;
                    case CreateTrackCommandName:
                        command = c.Kernel.Get<ICommand>(CreateTrackCommandName);
                        break;
                    case CancelRaceCommandName:
                        command = c.Kernel.Get<ICommand>(CancelRaceCommandName);
                        break;
                    case DeleteKartCommandName:
                        command = c.Kernel.Get<ICommand>(DeleteKartCommandName);
                        break;
                    case EchoCommandName:
                        command = c.Kernel.Get<ICommand>(EchoCommandName);
                        break;
                    case ExitCommandName:
                        command = c.Kernel.Get<ICommand>(ExitCommandName);
                        break;
                    case ExportRaceCommandName:
                        command = c.Kernel.Get<ICommand>(ExportRaceCommandName);
                        break;
                    case ExportRacerCommandName:
                        command = c.Kernel.Get<ICommand>(ExportRacerCommandName);
                        break;
                    case FinishRaceCommandName:
                        command = c.Kernel.Get<ICommand>(FinishRaceCommandName);
                        break;
                    case GetRaceInformationCommandName:
                        command = c.Kernel.Get<ICommand>(GetRaceInformationCommandName);
                        break;
                    case GetRacerInformationCommandName:
                        command = c.Kernel.Get<ICommand>(GetRacerInformationCommandName);
                        break;
                    case GetTrackInformationCommandName:
                        command = c.Kernel.Get<ICommand>(GetTrackInformationCommandName);
                        break;
                    case HelpCommandName:
                        command = c.Kernel.Get<ICommand>(HelpCommandName);
                        break;
                    case ImportLapCommandName:
                        command = c.Kernel.Get<ICommand>(ImportLapCommandName);
                        break;
                    case ImportRacerCommandName:
                        command = c.Kernel.Get<ICommand>(ImportRacerCommandName);
                        break;
                    case ImportTrackCommandName:
                        command = c.Kernel.Get<ICommand>(ImportTrackCommandName);
                        break;
                    case ListKartsCommandName:
                        command = c.Kernel.Get<ICommand>(ListKartsCommandName);
                        break;
                    case ListRacersCommandName:
                        command = c.Kernel.Get<ICommand>(ListRacersCommandName);
                        break;
                    case ListRacesCommandName:
                        command = c.Kernel.Get<ICommand>(ListRacesCommandName);
                        break;
                    case ListTracksCommandName:
                        command = c.Kernel.Get<ICommand>(ListTracksCommandName);
                        break;
                    case SetRacerHeightCommandName:
                        command = c.Kernel.Get<ICommand>(SetRacerHeightCommandName);
                        break;
                    case SetRacerWeightCommandName:
                        command = c.Kernel.Get<ICommand>(SetRacerWeightCommandName);
                        break;
                    case ShwangShwingCommandName:
                        command = c.Kernel.Get<ICommand>(ShwangShwingCommandName);
                        break;
                    case StartRaceCommandName:
                        command = c.Kernel.Get<ICommand>(StartRaceCommandName);
                        break;
                    default:
                        throw new InvalidOperationException($"Command {commandName} not found in the awesome factory");
                }

                return command;

            }).NamedLikeFactoryMethod((IAwesomeCommandFactory fac) => fac.GetCommand("flash"));

        }
    }
}
