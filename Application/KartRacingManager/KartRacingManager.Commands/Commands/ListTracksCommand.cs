using System;
using System.Linq;
using Bytes2you.Validation;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class ListTracksCommand : ICommand
    {
        private readonly IWriter writer;
        private readonly IMainUnitOfWork mainUnitOfWork;

        public ListTracksCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            int startIndex = 0;
            int endIndex = int.MaxValue - 1;

            try
            {
                if (commandParameters.Length > 0)
                {
                    startIndex = int.Parse(commandParameters[0]) - 1;

                }

                if (commandParameters.Length > 1)
                {
                    endIndex = int.Parse(commandParameters[1]) - 1;
                }

                if (startIndex < 0)
                {
                    startIndex = 0;
                }

                if (endIndex < startIndex)
                {
                    endIndex = startIndex;
                }
            }
            catch (FormatException)
            {
                this.writer.Write("To and from parameters should be integers.");
                writer.Write(Environment.NewLine);
                return;
            }

            var tracks = this.mainUnitOfWork.TracksRepo.All
                .OrderBy(t => t.Id)
                .Skip(startIndex)
                .Take(endIndex - startIndex + 1)
                .ToList();

            if (tracks.Count <= 0)
            {
                this.writer.Write($"No tracks in range starting from {startIndex + 1}.");
                this.writer.Write(Environment.NewLine);
            }
            else
            {
                this.writer.Write($"Listing tracks from {startIndex + 1} to {startIndex + tracks.Count}.");
                this.writer.Write(Environment.NewLine);
                int positionIndicator = startIndex + 1;
                for (int i = 0; i < tracks.Count; i++)
                {
                    this.writer.Write($"({positionIndicator}) Id: {tracks[i].Id} - " +
                                      $"{tracks[i].Name} " +
                                      $"({tracks[i].Address.Location}, {tracks[i].Address.City.Name}, {tracks[i].Address.City.Country.Name})");
                    this.writer.Write(Environment.NewLine);
                    positionIndicator++;
                }
            }
        }
    }
}
