using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;

namespace ElectronicStore.Data.Repositories
{
    public interface ITagRepositories : IRepositories<Tag>
    {
    }

    public class TagRepositories : Repositories<Tag>, ITagRepositories
    {
        public TagRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
