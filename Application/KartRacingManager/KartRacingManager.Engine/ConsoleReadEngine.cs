using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Engine;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Engine
{
    public class ConsoleReadEngine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandFactory commandFactory;

        public ConsoleReadEngine(IReader reader, IWriter writer, ICommandFactory commandFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandFactory = commandFactory;
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
                    }
                    catch (Exception)
                    {
                        this.writer.Write("Command execution error.");
                        // TODO: log
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
