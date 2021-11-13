using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrathAndGloryAPI.Interfaces;
using WrathAndGloryModels;

namespace WrathAndGloryAPI.Data
{
    public class SyncModelRepository : ISyncModelRepository
    {
        private readonly WrathAndGloryContext _context;

        public SyncModelRepository(WrathAndGloryContext context)
        {
            _context = context;
        }

        public SyncModel GetById(Guid id)
        {
            return _context.SyncModels.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<SyncModel> GetAll()
        {
            return _context.SyncModels.AsEnumerable();
        }

        public IEnumerable<SyncModel> Filter(Func<SyncModel, bool> predicate, bool asNoTracking = false)
        {
            if(asNoTracking)
            {
                return _context.SyncModels.AsNoTracking().Where(predicate).AsEnumerable();
            }

            return _context.SyncModels.Where(predicate).AsEnumerable();
        }

        public void Add(SyncModel syncModel)
        {
            _context.Add(syncModel);
            _context.SaveChanges();
        }

        public void Add(List<SyncModel> syncModels)
        {
            _context.AddRange(syncModels);
            _context.SaveChanges();
        }

        public void AddOrUpdate(List<SyncModel> syncModels)
        {
            syncModels.ForEach(x =>
            {
                if (x.Id == Guid.Empty)
                {
                    x.Id = Guid.NewGuid();
                }
            });

            List<Guid> apiIds = _context.SyncModels.AsNoTracking().Select(x => x.Id).ToList();

            List<SyncModel> updatedModels = syncModels.Where(x => apiIds.Any(o => o == x.Id)).ToList();
            List<SyncModel> newModels = syncModels.Where(x => !apiIds.Any(o => o == x.Id)).ToList();

            //if(syncModels.Any(x => x.ModelType != ModelType.Character))
            //{

            //}

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);

            _context.SaveChanges();
        }

        public SyncModel Edit(SyncModel syncModel)
        {
 
            _context.Update(syncModel);
            _context.SaveChanges();

            return syncModel;
        }

        public void Delete(SyncModel syncModel)
        {
            _context.SyncModels.Remove(syncModel);
            _context.SaveChanges();
        }

        public void Delete(List<SyncModel> syncModels)
        {
            _context.SyncModels.RemoveRange(syncModels);
            _context.SaveChanges();
        }
    }
}
