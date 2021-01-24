using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic
{
    public class CategoryLogic : ICategoryLogic
    {
        private IRepository<Category> CategoryRepository;

        public CategoryLogic(IUnitOfWork unitOfWork)
        {
            CategoryRepository = unitOfWork.GetCategoryRepository();
        }
        
        public ICollection<Category> GetAll()
        {
            return CategoryRepository.GetAll();
        }
    }
}
