using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicStore.Service
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);

        IEnumerable<Feedback> GetAll();

        IEnumerable<Feedback> GetAll(string keyword);

        Feedback GetById(int id);

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

        public IEnumerable<Feedback> GetAll()
        {
            return this.feedbackRepositories.GetAll();
        }

        public IEnumerable<Feedback> GetAll(string keyword)
        {
            var model = this.feedbackRepositories.GetAll();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                model = model.Where(x => x.Name.Contains(keyword) || x.Email.Contains(keyword));
            }
            return model;
        }

        public Feedback GetById(int id)
        {
            return this.feedbackRepositories.GetSingleById(id);
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }
    }
}