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
    public interface IBrandService
    {
        Brand Add(Brand brand);

        void Update(Brand brand);

        Brand Delete(int id);

        IEnumerable<Brand> GetAll();

        IEnumerable<Brand> GetAll(string keyword);

        Brand GetById(int id);

        void Save();
    }
    public class BrandService : IBrandService
    {
        private IBrandRepositories brandRepositories;
        private IUnitOfWork unitOfWork;

        public BrandService(IBrandRepositories brandRepositories, IUnitOfWork unitOfWork)
        {
            this.brandRepositories = brandRepositories;
            this.unitOfWork = unitOfWork;
        }


        public Brand Add(Brand Brand)
        {
            var brand = this.brandRepositories.Add(Brand);
            return brand;
        }

        public Brand Delete(int id)
        {
            return this.brandRepositories.Delete(id);
        }

        public IEnumerable<Brand> GetAll()
        {
            return this.brandRepositories.GetAll();
        }

        public IEnumerable<Brand> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return this.brandRepositories.GetMulti(x => x.Name.ToLower().Contains(keyword.ToLower()));
            else
                return this.brandRepositories.GetAll();
        }

        public Brand GetById(int id)
        {
            return this.brandRepositories.GetSingleById(id);
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }

        public void Update(Brand brand)
        {
            this.brandRepositories.Update(brand);
        }
    }
}
