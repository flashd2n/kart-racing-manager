using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartRacingManager.Interfaces.Commands
{
    public interface IAwesomeCommandFactory
    {
        ICommand GetCommand(string commandType);
    }
}
