﻿using System;
using System.Linq;
using Bytes2you.Validation;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;
using KarRacingManager.Models.PostgreSqlModels;

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
            var karts = this.kartsUnitOfWork.KartsRepo.All.Where(kart => kart.KartStatus == KartStatus.Working).ToList();

            if (karts.Count <= 0)
            {
                this.writer.Write("No karts are found.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            foreach (var kart in karts)
            {
                this.writer.Write($"id: {kart.Id}, {kart.HorsePower} hp, top {kart.TopSpeedKph} kph, transmission: {kart.TransmissionType.Name}");
                this.writer.Write(Environment.NewLine);
            }
        }
    }

}
