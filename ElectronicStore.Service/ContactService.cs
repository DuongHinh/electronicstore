using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;

namespace ElectronicStore.Service
{
    public interface IContactService
    {
        Contact GetInforContact();
    }

    public class ContactDetailService : IContactService
    {
        private IContactRepositories contactDetailRepositories;
        private IUnitOfWork unitOfWork;

        public ContactDetailService(IContactRepositories contactDetailRepositories, IUnitOfWork unitOfWork)
        {
            this.contactDetailRepositories = contactDetailRepositories;
            this.unitOfWork = unitOfWork;
        }

        public Contact GetInforContact()
        {
            return contactDetailRepositories.GetSingleByCondition(x => x.Status);
        }
    }
}