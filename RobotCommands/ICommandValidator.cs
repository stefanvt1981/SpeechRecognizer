using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCommands
{
    public interface ICommandValidator
    {
        AvailableRobotCommands ValidateCommand(string recognizedCommand);
    }
}
