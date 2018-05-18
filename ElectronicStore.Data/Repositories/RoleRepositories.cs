using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IRoleRepositories : IRepositories<Role>
    {
        IEnumerable<Role> GetListRoleByGroupId(int groupId);
    }
    public class RoleRepositories : Repositories<Role>, IRoleRepositories
    {
        public RoleRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public IEnumerable<Role> GetListRoleByGroupId(int groupId)
        {
            var query = from r in DbContext.Role
                        join rg in DbContext.RoleGroup
                        on r.Id equals rg.RoleId
                        where rg.GroupId == groupId
                        select r;
            return query;
        }
    }
}
