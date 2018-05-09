using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IFeedbackRepositories : IRepositories<Feedback>
    {

    }
    public class FeedbackRepositories : Repositories<Feedback>, IFeedbackRepositories
    {
        public FeedbackRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
