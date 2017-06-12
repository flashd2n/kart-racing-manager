using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories
{
    public class TracksRepository
    {
        private IMainDbContext context;

        public TracksRepository(IMainDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public IQueryable<Track> All
        {
            get { return this.context.Tracks; }
        }

        public void Add(Track entity)
        {
            this.context.Tracks.Add(entity);
        }

        public Track FindById(object id)
        {
            return this.context.Tracks.Find(id);
        }

        public void Remove(Track entity)
        {
            this.context.Tracks.Remove(entity);
        }

        public void Update(Track entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(Track entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
