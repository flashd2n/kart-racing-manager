using KartRacingManager.Interfaces.Providers;
using System;

namespace KartRacingManager.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
