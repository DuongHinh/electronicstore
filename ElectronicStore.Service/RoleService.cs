using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using ElectronicStore.Fulcrum.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicStore.Service
{
    public interface IRoleService
    {
        Role Add(Role role);

        //Add roles to a sepcify group
        bool AddRolesToGroup(IEnumerable<RoleGroup> roleGroups, int groupId);

        Role GetById(string id);

        IEnumerable<Role> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<Role> GetAll();

        //Get list role by group id
        IEnumerable<Role> GetListRoleByGroupId(int groupId);

        void Update(Role role);

        void Delete(string id);

        void Save();
    }

    public class RoleService : IRoleService
    {
        private IRoleRepositories roleRepositories;
        private IRoleGroupRepositories roleGroupRepositories;
        private IUnitOfWork unitOfWork;

        public RoleService(IRoleRepositories roleRepositories, IRoleGroupRepositories roleGroupRepositories, IUnitOfWork unitOfWork)
        {
            this.roleRepositories = roleRepositories;
            this.roleGroupRepositories = roleGroupRepositories;
            this.unitOfWork = unitOfWork;
        }

        public Role Add(Role role)
        {
            if (this.roleRepositories.Any(x => x.Name == role.Name))
            {
                throw new DuplicatedException("Role name is exits");
            }
            return this.roleRepositories.Add(role);
        }

        public bool AddRolesToGroup(IEnumerable<RoleGroup> roleGroups, int groupId)
        {
            this.roleGroupRepositories.DeleteMulti(x => x.GroupId == groupId);
            foreach (var roleGroup in roleGroups)
            {
                this.roleGroupRepositories.Add(roleGroup);
            }
            return true;
        }

        public IEnumerable<Role> GetAll()
        {
            return this.roleRepositories.GetAll();
        }

        public IEnumerable<Role> GetAll(int page, int pageSize, out int totalRow, string search)
        {
            var query = this.roleRepositories.GetAll();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Description.Contains(search));

            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public Role GetById(string id)
        {
            return this.roleRepositories.GetSingleById(id);
        }

        public IEnumerable<Role> GetListRoleByGroupId(int groupId)
        {
            return this.roleRepositories.GetListRoleByGroupId(groupId);
        }

        public void Update(Role role)
        {
            if (this.roleRepositories.Any(x => x.Name == role.Name && x.Id != role.Id))
                throw new DuplicatedException("Role name is exist");
            this.roleRepositories.Update(role);
        }

        public void Delete(string id)
        {
            this.roleRepositories.DeleteMulti(x => x.Id == id);
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }
    }
}