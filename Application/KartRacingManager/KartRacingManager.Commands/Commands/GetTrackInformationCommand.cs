using System;
using System.Linq;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class GetTrackInformationCommand : ICommand
    {
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IWriter writer;

        public GetTrackInformationCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length < 1)
            {
                this.writer.Write("Track id is not provided.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int trackId = 0;
            try
            {
                trackId = int.Parse(commandParameters[0]);
            }
            catch (FormatException)
            {
                this.writer.Write("Track id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            var track = this.mainUnitOfWork.TracksRepo.FindById(trackId);
            if (track == null)
            {
                this.writer.Write($"No track with id {trackId}.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            // basic info
            this.writer.Write($"Track name: {track.Name}");
            this.writer.Write(Environment.NewLine);
            // address
            this.writer.Write($"Full address: {track.Address.Location}, {track.Address.City.Name}, {track.Address.City.Country.Name}");
            this.writer.Write(Environment.NewLine);
            // races
            int numberOfCompletedRaces = this.mainUnitOfWork.RacesRepo.All
                .Count(race => race.RaceStatus == RaceStatus.Completed && race.TrackId == trackId);
            this.writer.Write($"Number of completed races: {numberOfCompletedRaces}");
            this.writer.Write(Environment.NewLine);
            int numberOfPendingRaces = this.mainUnitOfWork.RacesRepo.All
                .Count(race => race.RaceStatus == RaceStatus.Pending && race.TrackId == trackId);
            this.writer.Write($"Number of pending races: {numberOfPendingRaces}");
            this.writer.Write(Environment.NewLine);
            int numberOfRacesInProgress = this.mainUnitOfWork.RacesRepo.All
                .Count(race => race.RaceStatus == RaceStatus.InProgress && race.TrackId == trackId);
            this.writer.Write($"Number of races in progress: {numberOfRacesInProgress}");
            this.writer.Write(Environment.NewLine);
            int numberOfCanceledRaces = this.mainUnitOfWork.RacesRepo.All
                .Count(race => race.RaceStatus == RaceStatus.Cancelled && race.TrackId == trackId);
            this.writer.Write($"Number of canceled races: {numberOfCanceledRaces}");
            this.writer.Write(Environment.NewLine);
            // number of laps
            int numberOfLaps = this.mainUnitOfWork.LapsRepo.All.Count(lap => lap.TrackId == trackId);
            this.writer.Write($"Total number of laps on this track: {numberOfLaps}");
            this.writer.Write(Environment.NewLine);
            var bestLap = this.mainUnitOfWork.LapsRepo.All
                .Where(lap => lap.TrackId == trackId)
                .OrderBy(lap => lap.LapTimeInMs)
                .FirstOrDefault();

            if (bestLap == null)
            {
                this.writer.Write($"Best lap: N/A");
            }
            else
            {
                this.writer.Write($"Best lap: {bestLap.LapTimeInMs / 1000 / 60:0}:{bestLap.LapTimeInMs / 1000 % 60:00}.{bestLap.LapTimeInMs % 1000:000} " +
                                  $"({bestLap.Racer.FirstName} {bestLap.Racer.LastName})");
            }
            this.writer.Write(Environment.NewLine);
        }
    }
}
