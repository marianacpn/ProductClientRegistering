using Domain.Core;
using System;

namespace Domain.Repository.Interface.Core
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Base
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);

        void SaveChanges(int userId = 1, string userName = "System", bool saveLog = true);
    }
}
