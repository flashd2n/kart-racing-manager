using System;
using System.Linq;
using Bytes2you.Validation;
using KarRacingManager.Models.PostgreSqlModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class ListKartsCommand : ICommand
    {
        private readonly IKartsUnitOfWork kartsUnitOfWork;
        private readonly IWriter writer;

        public ListKartsCommand(IKartsUnitOfWork kartsUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(kartsUnitOfWork, "kartsUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.kartsUnitOfWork = kartsUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            foreach (var kart in this.kartsUnitOfWork.KartsRepo.All.ToList())
            {
                this.writer.Write($"id: {kart.Id}, {kart.HorsePower} hp, top {kart.TopSpeedKph} kph, transmission: {kart.TransmissionType.Name}");
                this.writer.Write(Environment.NewLine);
            }
        }
    }

}
