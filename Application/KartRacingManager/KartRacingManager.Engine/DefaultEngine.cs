﻿using System;
using System.Linq;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Engine;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Engine
{
    public class DefaultEngine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandFactory commandFactory;

        public DefaultEngine(IReader reader, IWriter writer, ICommandFactory commandFactory)
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
                var commandParts = commandLine.Split(' ').ToList();
                string commandName = commandParts[0];

                ICommand command = this.commandFactory.GetCommand(commandName);

                if (command != null)
                {
                    commandParts.RemoveAt(0);
                    command.Execute(commandParts.ToArray());
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
    }
}