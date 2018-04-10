using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Core
{
    public interface IUnitOfWork
    {
        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextFactory dbContextFactory;
        private ElectronicStoreDbContext dbContext;

        public UnitOfWork(IDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public ElectronicStoreDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbContextFactory.Init()); }
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}
