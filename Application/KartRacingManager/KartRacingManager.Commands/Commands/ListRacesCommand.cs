using System;
using System.Linq;
using Bytes2you.Validation;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class ListRacesCommand : ICommand
    {
        private readonly IWriter writer;
        private readonly IMainUnitOfWork mainUnitOfWork;

        public ListRacesCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
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

            var races = this.mainUnitOfWork.RacesRepo.All
                .OrderByDescending(r => r.StartTime)
                .Skip(startIndex)
                .Take(endIndex - startIndex + 1)
                .ToList();

            if (races.Count <= 0)
            {
                this.writer.Write($"No races in range starting from {startIndex + 1}.");
                this.writer.Write(Environment.NewLine);
            }
            else
            {
                this.writer.Write($"Listing races from {startIndex + 1} to {startIndex + races.Count}.");
                this.writer.Write(Environment.NewLine);
                int positionIndicator = startIndex + 1;
                for (int i = 0; i < races.Count; i++)
                {
                    string endTimeAsString = races[i].EndTime != null ? races[i].EndTime.ToString() : "N/A";
                    this.writer.Write($"({positionIndicator}) Id: {races[i].Id} - " +
                                      $"{races[i].Name}, " +
                                      $"{races[i].RaceStatus}, " +
                                      $"Start time: {races[i].StartTime}, " +
                                      $"End Time: {endTimeAsString}," +
                                      $"Laps: {races[i].Laps}; " +
                                      $"Track: {races[i].Track.Name}");
                    this.writer.Write(Environment.NewLine);
                    positionIndicator++;
                }
            }
        }
    }
}
