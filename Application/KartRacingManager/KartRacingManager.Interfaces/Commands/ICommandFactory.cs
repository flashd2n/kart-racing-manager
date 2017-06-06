namespace KartRacingManager.Interfaces.Commands
{
    public interface ICommandFactory
    {
        ICommand GetCommand(string commandName);
    }
}
