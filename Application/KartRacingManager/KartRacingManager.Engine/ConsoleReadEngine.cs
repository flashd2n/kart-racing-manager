using System;
using System.Collections.Generic;
using System.Text;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Engine;
using KartRacingManager.Interfaces.Logger;
using KartRacingManager.Interfaces.Providers;
using Bytes2you.Validation;

namespace KartRacingManager.Engine
{
    public class ConsoleReadEngine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandFactory commandFactory;
        private readonly ILogger logger;

        public ConsoleReadEngine(IReader reader, IWriter writer, ICommandFactory commandFactory, ILogger logger)
        {
            Guard.WhenArgument(reader, "reader").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            Guard.WhenArgument(commandFactory, "commandFactory").IsNull().Throw();
            Guard.WhenArgument(logger, "logger").IsNull().Throw();

            this.reader = reader;
            this.writer = writer;
            this.commandFactory = commandFactory;
            this.logger = logger;
        }

        public void Run()
        {
            for (;;)
            {
                writer.Write("Command: ");

                string commandLine = this.reader.Read();
                var commandParts = this.getCommandParts(commandLine);

                if (commandParts.Count <= 0)
                {
                    continue;
                }

                string commandName = commandParts[0];

                ICommand command = this.commandFactory.GetCommand(commandName);

                if (command != null)
                {
                    commandParts.RemoveAt(0);
                    try
                    {
                        command.Execute(commandParts.ToArray());
                        this.logger.Log("info", $"Command execution {commandLine}.");
                    }
                    catch (Exception e)
                    {
                        this.writer.Write("Oooops :( Something happened :( Here is the tech-support phone number - 0893387175 - SENIOR Engineer Tonchev");
                        this.logger.Log("error", $"Command execution error. Full command: {commandLine}. Error: {e.Message}");
                        this.writer.Write(Environment.NewLine);
                    }
                }

                if (commandName == "exit")
                {
                    return;
                }

                if (command == null)
                {
                    writer.Write($"Unrecognized command \"{commandName}\"");
                    this.logger.Log("info", $"Unrecognized command \"{commandName}\".");
                    writer.Write(Environment.NewLine);
                }
            }
        }

        // Get command parts. Split by spaces unless the command part is contained in ""
        // \" escapes the "
        private List<string> getCommandParts(string commandLine)
        {
            List<string> result = new List<string>();
            StringBuilder currentCommandPart = null;
            bool insideQuotes = false;
            bool insideEscape = false;

            for (int stringIndex = 0; stringIndex < commandLine.Length; stringIndex++)
            {
                char currentCharacter = commandLine[stringIndex];
                if (currentCommandPart == null)
                {
                    currentCommandPart = new StringBuilder();
                }

                if (insideEscape)
                {
                    currentCommandPart.Append(currentCharacter);
                    insideEscape = false;
                }
                else if (currentCharacter == '\\')
                {
                    insideEscape = true;
                }
                else if (insideQuotes)
                {
                    if (currentCharacter == '"')
                    {
                        if (currentCommandPart.Length > 0)
                        {
                            result.Add(currentCommandPart.ToString());
                        }
                        currentCommandPart = null;

                        insideQuotes = false;
                    }
                    else
                    {
                        currentCommandPart.Append(currentCharacter);
                    }
                }
                else
                {
                    if (currentCharacter == ' ')
                    {
                        if (currentCommandPart.Length > 0)
                        {
                            result.Add(currentCommandPart.ToString());
                        }
                        currentCommandPart = null;
                    }
                    else if (currentCharacter == '"')
                    {
                        insideQuotes = true;
                    }
                    else
                    {
                        currentCommandPart.Append(currentCharacter);
                    }
                }
            }

            if (currentCommandPart != null && currentCommandPart.Length > 0)
            {
                result.Add(currentCommandPart.ToString());
            }

            return result;
        }
    }
}
