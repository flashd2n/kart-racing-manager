using KartRacingManager.Commands;
using KartRacingManager.Data;
using KartRacingManager.Engine;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Data;
using KartRacingManager.Interfaces.Engine;
using KartRacingManager.Interfaces.Providers;
using KartRacingManager.Providers;
using Ninject.Modules;

namespace KartRacingManager.ConsoleClient
{
    public class KartRacingManagerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IEngine>().To<ConsoleReadEngine>().InSingletonScope();
            this.Bind<IMainDbContext>().To<MainDbContext>().InSingletonScope();
            this.Bind<IReader>().To<ConsoleReader>();
            this.Bind<IWriter>().To<ConsoleWriter>();
            this.Bind<ICommandFactory>().To<CommandFactory>();
        }
    }
}
