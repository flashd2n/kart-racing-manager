using System;
using System.Linq;
using KartRacingManager.Data;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Data;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class ListRacersCommand : ICommand
    {
        private readonly IWriter writer;
        private readonly IMainDbContext mainDbContext;

        public ListRacersCommand(IMainDbContext mainDbContext, IWriter writer)
        {
            this.mainDbContext = mainDbContext;
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
            catch (FormatException ex)
            {
                var argException = new ArgumentException("The suplied arguments are not valid. " + ex.Message);
                throw argException;
            }

            var dbContext = new MainDbContext();

            var racers = dbContext.Racers
                .OrderBy(r => r.Id)
                .Skip(startIndex)
                .Take(endIndex - startIndex + 1)
                .ToList();

            if (racers.Count <= 0)
            {
                this.writer.Write($"No racers in this range ({startIndex + 1} to {endIndex + 1}).");
                this.writer.Write(Environment.NewLine);
            }
            else
            {
                this.writer.Write($"Listing racers from {startIndex + 1} to {startIndex + racers.Count}.");
                this.writer.Write(Environment.NewLine);
                int positionIndicator = startIndex + 1;
                for (int i = 0; i < racers.Count; i++)
                {
                    this.writer.Write($"({positionIndicator}) Id: {racers[i].Id} - {racers[i].FirstName} {racers[i].LastName}");
                    this.writer.Write(Environment.NewLine);
                    positionIndicator++;
                }
            }
        }
    }
}
