using RobotCommands;

namespace ConsoleRecognizer
{
    public class ConsoleCommandValidator : ICommandValidator
    {
        public RobotCommands.AvailableRobotCommands ValidateCommand(string recognizedCommand)
        {
            switch (recognizedCommand)
            {
                case "Up":
                    return AvailableRobotCommands.Vooruit;
                default:
                    return AvailableRobotCommands.Onbekend;
            }
        }
    }
}
