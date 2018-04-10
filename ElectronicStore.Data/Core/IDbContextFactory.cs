using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Core
{
    public interface IDbContextFactory : IDisposable
    {
        ElectronicStoreDbContext Init();
    }

    public class DbContextFactory : Disposable, IDbContextFactory
    {
        private ElectronicStoreDbContext dbContext;

        public ElectronicStoreDbContext Init()
        {
            return dbContext ?? (dbContext = new ElectronicStoreDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
