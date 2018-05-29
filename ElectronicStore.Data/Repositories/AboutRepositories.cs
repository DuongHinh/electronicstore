using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IAboutRepositories : IRepositories<About>
    {
    }

    public class AboutRepositories : Repositories<About>, IAboutRepositories
    {
        public AboutRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
