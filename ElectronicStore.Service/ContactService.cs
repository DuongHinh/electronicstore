using System;
using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using System.Linq;

namespace ElectronicStore.Service
{
    public interface IContactService
    {
        Contact GetInforContact();

        void Update(Contact Contact);

        Contact GetById(int id);

        void Save();
    }

    public class ContactDetailService : IContactService
    {
        private IContactRepositories contactRepositories;
        private IUnitOfWork unitOfWork;

        public ContactDetailService(IContactRepositories contactDetailRepositories, IUnitOfWork unitOfWork)
        {
            this.contactRepositories = contactDetailRepositories;
            this.unitOfWork = unitOfWork;
        }

        public Contact GetById(int id)
        {
            return this.contactRepositories.GetSingleById(id);
        }

        public Contact GetInforContact()
        {
            return contactRepositories.GetAll().FirstOrDefault();
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }

        public void Update(Contact Contact)
        {
            this.contactRepositories.Update(Contact);
        }
    }
}