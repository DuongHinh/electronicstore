using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Service
{
    public interface IAboutService
    {
        About GetAboutInfo();

        void Update(About About);

        About GetById(int id);

        void Save();
    }

    public class AboutService : IAboutService
    {
        private IAboutRepositories aboutRepositories;
        private IUnitOfWork unitOfWork;

        public AboutService(IAboutRepositories aboutRepositories, IUnitOfWork unitOfWork)
        {
            this.aboutRepositories = aboutRepositories;
            this.unitOfWork = unitOfWork;
        }
        public About GetAboutInfo()
        {
            return this.aboutRepositories.GetAll().FirstOrDefault();
        }

        public About GetById(int id)
        {
            return this.aboutRepositories.GetSingleById(id);
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }

        public void Update(About About)
        {
            this.aboutRepositories.Update(About);
        }
    }
}
