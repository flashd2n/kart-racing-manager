using System;
using System.Linq;
using Bytes2you.Validation;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{

    public class GetRaceInformationCommand : ICommand
    {
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IWriter writer;

        public GetRaceInformationCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length <= 0)
            {
                this.writer.Write("GetRaceInformation race_id");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (commandParameters.Length < 1)
            {
                this.writer.Write("Incorrect number of parameters.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int raceId = 0;
            try
            {
                raceId = int.Parse(commandParameters[0]);
            }
            catch (FormatException)
            {
                this.writer.Write("Race id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            var race = this.mainUnitOfWork.RacesRepo.FindById(raceId);
            if (race == null)
            {
                this.writer.Write($"No race with id {raceId}.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            this.writer.Write($"Race: {race.Name}");
            this.writer.Write(Environment.NewLine);
            this.writer.Write($"State: {race.RaceStatus}");
            this.writer.Write(Environment.NewLine);
            this.writer.Write($"Start time: {race.StartTime}");
            this.writer.Write(Environment.NewLine);
            string endTimeAsString = race.EndTime != null ? race.EndTime.ToString() : "N/A";
            this.writer.Write($"End time: {endTimeAsString}");
            this.writer.Write(Environment.NewLine);
            this.writer.Write($"Laps: {race.LapCount}");
            this.writer.Write(Environment.NewLine);
            this.writer.Write($"Track: {race.Track.Name}");
            this.writer.Write(Environment.NewLine);
            this.writer.Write($"Racers:");
            this.writer.Write(Environment.NewLine);
            foreach (var racer in race.Racers)
            {
                this.writer.Write($"Id: {racer.Id} - {racer.FirstName} {racer.LastName}");
                this.writer.Write(Environment.NewLine);
            }

            this.writer.Write($"Race laps:");
            this.writer.Write(Environment.NewLine);
            foreach (var lap in race.Laps.OrderBy(lap => lap.FinishTime))
            {
                this.writer.Write($"{lap.StartTime} - {lap.FinishTime} {lap.Racer.FirstName} {lap.Racer.LastName}");
                this.writer.Write(Environment.NewLine);
            }

            this.writer.Write(Environment.NewLine);
        }
    }
}
