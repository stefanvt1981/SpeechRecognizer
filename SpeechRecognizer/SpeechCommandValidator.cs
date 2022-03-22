using RobotCommands;

namespace SpeechRecognizer
{
    public class SpeechCommandValidator : ICommandValidator
    {
        public AvailableRobotCommands ValidateCommand(string recognizedCommand)
        {
            switch (recognizedCommand)
            {
                case "Vooruit":
                    return AvailableRobotCommands.Vooruit;
                default:
                    return AvailableRobotCommands.Onbekend;
            }

        }
    }
}
