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
    public interface ILogErrorService
    {
        LogError Create(LogError error);

        void Save();
    }
    public class LogErrorService : ILogErrorService
    {
        private ILogErrorRepositories logErrorRepository;
        private IUnitOfWork unitOfWork;

        public LogErrorService(ILogErrorRepositories logErrorRepository, IUnitOfWork unitOfWork)
        {
            this.logErrorRepository = logErrorRepository;
            this.unitOfWork = unitOfWork;
        }

        public LogError Create(LogError error)
        {
            var logError = this.logErrorRepository.Add(error);
            return logError;
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }
    }
}
