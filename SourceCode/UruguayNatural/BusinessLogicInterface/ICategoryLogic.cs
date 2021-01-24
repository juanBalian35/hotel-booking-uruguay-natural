using System.Collections.Generic;
using Domain;
using Model.In;
using Model.Out;

namespace BusinessLogicInterface
{
    public interface ICategoryLogic
    {
        ICollection<Category> GetAll();
    }
}