using System;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands
{
    public class AddLapCommand : ICommand
    {
        private readonly IWriter writer;
        private readonly IMainUnitOfWork mainUnitOfWork;

        public AddLapCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
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
                this.writer.Write("AddLap lap_start_time lap_finish_time|lap_duration_in_ms racer_id track_id [race_id]");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (commandParameters.Length < 4)
            {
                this.writer.Write("Incorrect number of parameters.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            DateTime lapStartTime;
            try
            {
                lapStartTime = DateTime.Parse(commandParameters[0]);
            }
            catch (FormatException)
            {
                this.writer.Write("Incorrect start time.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            //lap end time OR lap duration in miliseconds is accepted as a second argument
            int lapTimeInMiliseconds = 0;
            DateTime lapEndTime;
            try
            {
                lapTimeInMiliseconds = int.Parse(commandParameters[1]);
                lapEndTime = lapStartTime + TimeSpan.FromMilliseconds(lapTimeInMiliseconds);
            }
            catch (FormatException)
            {
                try
                {
                    lapEndTime = DateTime.Parse(commandParameters[1]);
                    lapTimeInMiliseconds = (int)(lapEndTime - lapStartTime).TotalMilliseconds;
                }
                catch (FormatException)
                {
                    this.writer.Write("Incorrect lap time in miliseconds or lap end time.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }
            }

            int racerId = 0;
            try
            {
                racerId = int.Parse(commandParameters[2]);
                if (this.mainUnitOfWork.RacersRepo.FindById(racerId) == null)
                {
                    this.writer.Write($"Racer with id  {racerId} does not exist.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }
            }
            catch (FormatException)
            {
                this.writer.Write("Racer id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int trackId = 0;
            try
            {
                trackId = int.Parse(commandParameters[3]);
                if (this.mainUnitOfWork.TracksRepo.FindById(trackId) == null)
                {
                    this.writer.Write($"Track with id  {trackId} does not exist.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }
            }
            catch (FormatException)
            {
                this.writer.Write("Track id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int? raceId = null;
            if (commandParameters.Length >= 5)
            {
                try
                {
                    raceId = int.Parse(commandParameters[4]);
                }
                catch (FormatException)
                {
                    this.writer.Write("Race id must be integer.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }
            }

            if (raceId != null)
            {
                var race = this.mainUnitOfWork.RacesRepo.FindById(raceId);
                if (race == null)
                {
                    this.writer.Write($"Race with id of {raceId} does not exist.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }

                if (race.RaceStatus != RaceStatus.InProgress)
                {
                    this.writer.Write($"Race with id of {raceId} is in state {race.RaceStatus}. " +
                                      $"Only races in state {RaceStatus.InProgress} can be added laps.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }
            }

            var lap = new Lap
            {
                StartTime = lapStartTime,
                FinishTime = lapEndTime,
                LapTimeInMs = lapTimeInMiliseconds,
                RacerId = racerId,
                TrackId = trackId,
                RaceId = raceId
            };

            this.mainUnitOfWork.LapsRepo.Add(lap);

            this.mainUnitOfWork.Save();
        }
    }

}
