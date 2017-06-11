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

namespace KartRacingManager.ConsoleClient
{
    public class KartRacingManagerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            var engine = this.Bind<IEngine>().To<ConsoleReadEngine>().InSingletonScope();
            this.Bind<IMainDbContext>().To<MainDbContext>().InSingletonScope();
            this.Bind<IMainUnitOfWork>().To<MainUnitOfWork>().InSingletonScope();
            this.Bind<IKartDbContext>().To<KartDbContext>().InSingletonScope();
            this.Bind<IKartsUnitOfWork>().To<KartsUnitOfWork>().InSingletonScope();
            this.Bind<IReader>().To<ConsoleReader>();
            this.Bind<IWriter>().To<ConsoleWriter>();
            this.Bind<ICommandFactory>().To<CommandFactory>();
            this.Bind<ILogger>().To<SqliteLogger>();
            this.Bind<IDateTimeProvider>().To<DateTimeProvider>();
            this.Bind<ILogsUnitOfWork>().To<LogsUnitOfWork>();
            this.Bind<ILogsDbContext>().To<LogsDbContext>();
            this.Bind<IJsonImporter>().To<JsonImporter>();
            this.Bind<IXmlImporter>().To<XmlImporter>();
            //this.Bind<IExporter>().To(null);
        }
    }
}
