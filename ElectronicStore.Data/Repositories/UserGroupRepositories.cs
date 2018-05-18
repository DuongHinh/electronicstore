using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IUserGroupRepositories : IRepositories<UserGroup>
    {
    }
    public class UserGroupRepositories : Repositories<UserGroup>, IUserGroupRepositories
    {
        public UserGroupRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
