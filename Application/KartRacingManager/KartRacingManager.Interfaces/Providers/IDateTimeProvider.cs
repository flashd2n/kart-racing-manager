using System;

namespace KartRacingManager.Interfaces.Providers
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get;  } 
    }
}
