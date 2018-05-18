using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IRoleGroupRepositories : IRepositories<RoleGroup>
    {

    }

    public class RoleGroupRepositories : Repositories<RoleGroup>, IRoleGroupRepositories
    {
        public RoleGroupRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
