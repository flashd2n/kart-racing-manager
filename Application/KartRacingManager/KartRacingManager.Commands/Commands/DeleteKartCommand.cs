using Bytes2you.Validation;
using KarRacingManager.Models.PostgreSqlModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;
using System;
using System.Linq;

namespace KartRacingManager.Commands.Commands
{
    public class DeleteKartCommand : ICommand
    {
        private readonly IKartsUnitOfWork kartsUnitOfWork;
        private readonly IWriter writer;

        public DeleteKartCommand(IKartsUnitOfWork kartsUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(kartsUnitOfWork, "kartsUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.kartsUnitOfWork = kartsUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {

            if (commandParameters.Length <= 0)
            {
                this.writer.Write("DeleteKart kart_id");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (commandParameters.Length < 1)
            {
                this.writer.Write("Kart id must be provided.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int kartId;
            try
            {
                kartId = int.Parse(commandParameters[0]);
            }
            catch (FormatException)
            {
                this.writer.Write("Kart id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            var kart = this.kartsUnitOfWork.KartsRepo.FindById(kartId);

            if (kart == null)
            {
                this.writer.Write($"Kart with id of {kartId} does not exist.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            kart.KartStatus = KartStatus.Deleted;

            this.kartsUnitOfWork.Save();
        }
    }
}
