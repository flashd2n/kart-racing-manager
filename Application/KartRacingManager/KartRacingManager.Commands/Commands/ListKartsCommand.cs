using System;
using System.Linq;
using Bytes2you.Validation;
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
            if (this.kartsUnitOfWork.KartsRepo.All.Count() <= 0)
            {
                this.writer.Write("No karts are found");
                this.writer.Write(Environment.NewLine);
                return;
            }

            foreach (var kart in this.kartsUnitOfWork.KartsRepo.All.ToList())
            {
                this.writer.Write($"id: {kart.Id}, {kart.HorsePower} hp, top {kart.TopSpeedKph} kph, transmission: {kart.TransmissionType.Name}");
                this.writer.Write(Environment.NewLine);
            }
        }
    }

}
