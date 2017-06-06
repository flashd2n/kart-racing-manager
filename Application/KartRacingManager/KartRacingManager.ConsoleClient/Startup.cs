using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Engine;
using KartRacingManager.Interfaces.Providers;
using KartRacingManager.Providers;
using KartRacingManager.Commands;
using KartRacingManager.Engine;

namespace KartRacingManager.ConsoleClient
{
    public class Startup
    {
        static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            ICommandFactory commandFactory = new CommandFactory(writer);
            IEngine engine = new DefaultEngine(reader, writer, commandFactory);

            engine.Run();
        }
    }
}
