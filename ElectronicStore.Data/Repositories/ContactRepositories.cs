using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IContactRepositories : IRepositories<Contact>
    {

    }
    public class ContactRepositories : Repositories<Contact>, IContactRepositories
    {
        public ContactRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
