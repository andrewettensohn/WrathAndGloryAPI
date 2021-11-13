using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrathAndGloryModels;

namespace WrathAndGloryAPI.Interfaces
{
    public interface ISyncModelRepository
    {

        SyncModel GetById(Guid id);

        IEnumerable<SyncModel> GetAll();

        IEnumerable<SyncModel> Filter(Func<SyncModel, bool> predicate, bool asNoTracking = false);

        void Add(SyncModel syncModel);

        void Add(List<SyncModel> syncModels);

        void AddOrUpdate(List<SyncModel> syncModels);

        SyncModel Edit(SyncModel syncModel);

        void Delete(SyncModel syncModel);

        void Delete(List<SyncModel> syncModels);

    }
}
