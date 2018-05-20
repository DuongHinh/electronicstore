using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using ElectronicStore.Fulcrum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Service
{
    public interface IGroupService
    {
        Group GetById(int id);

        IEnumerable<Group> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<Group> GetAll();

        Group Add(Group group);

        void Update(Group group);

        Group Delete(int id);

        bool AddUserToGroups(IEnumerable<UserGroup> userGroups, string userId);

        IEnumerable<Group> GetListGroupByUserId(string userId);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);

        void Save();
    }
    public class GroupService : IGroupService
    {
        private IGroupRepositories groupRepositories;
        private IUserGroupRepositories  userGroupRepositories;
        private IUnitOfWork unitOfWork;
        public GroupService(IGroupRepositories groupRepositories, IUserGroupRepositories userGroupRepositories, IUnitOfWork unitOfWork) {
            this.groupRepositories = groupRepositories;
            this.userGroupRepositories = userGroupRepositories;
            this.unitOfWork = unitOfWork;
        }
        public Group Add(Group group)
        {
            if (this.groupRepositories.Any(x => x.Name == group.Name))
                throw new DuplicatedException("Group name is exist");
            return groupRepositories.Add(group);
        }

        public IEnumerable<Group> GetAll()
        {
            return this.groupRepositories.GetAll();
        }

        public IEnumerable<Group> GetAll(int page, int pageSize, out int totalRow, string search)
        {
            var query = groupRepositories.GetAll();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Name.Contains(search));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public Group GetById(int id)
        {
            return this.groupRepositories.GetSingleById(id);
        }

        public IEnumerable<Group> GetListGroupByUserId(string userId)
        {
            return this.groupRepositories.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return this.groupRepositories.GetListUserByGroupId(groupId);
        }

        public void Update(Group group)
        {
            if (this.groupRepositories.Any(x => x.Name == group.Name && x.Id != group.Id))
                throw new DuplicatedException("Group name is exist");
            this.groupRepositories.Update(group);
        }

        public Group Delete(int id)
        {
            var group = this.groupRepositories.GetSingleById(id);
            return this.groupRepositories.Delete(group);
        }

        public bool AddUserToGroups(IEnumerable<UserGroup> userGroups, string userId)
        {
            this.userGroupRepositories.DeleteMulti(x => x.UserId == userId);
            foreach (var userGroup in userGroups)
            {
                this.userGroupRepositories.Add(userGroup);
            }
            return true;
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }
    }
}
