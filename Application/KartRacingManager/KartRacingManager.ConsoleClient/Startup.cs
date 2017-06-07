using KartRacingManager.Interfaces.Engine;
using Ninject;

namespace KartRacingManager.ConsoleClient
{
    public class Startup
    {
        static void Main()
        {
            IKernel kernel = new StandardKernel(new KartRacingManagerNinjectModule());

            IEngine engine = kernel.Get<IEngine>();

            engine.Run();
        }
    }
}
