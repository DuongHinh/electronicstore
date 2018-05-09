using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;

namespace ElectronicStore.Service
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);

        void Save();
    }

    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepositories feedbackRepositories;
        private IUnitOfWork unitOfWork;

        public FeedbackService(IFeedbackRepositories feedbackRepositories, IUnitOfWork unitOfWork)
        {
            this.feedbackRepositories = feedbackRepositories;
            this.unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            return this.feedbackRepositories.Add(feedback);
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }
    }
}