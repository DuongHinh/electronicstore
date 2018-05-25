using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;

namespace ElectronicStore.Data.Repositories
{
    public interface INewsCategoryRepositories : IRepositories<NewsCategory>
    {
    }

    public class NewsCategoryRepositories : Repositories<NewsCategory>, INewsCategoryRepositories
    {
        public NewsCategoryRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}