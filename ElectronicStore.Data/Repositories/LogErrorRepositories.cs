using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface ILogErrorRepositories : IRepositories<LogError>
    {

    }

    public class LogErrorRepositories : Repositories<LogError>, ILogErrorRepositories
    {
        public LogErrorRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
