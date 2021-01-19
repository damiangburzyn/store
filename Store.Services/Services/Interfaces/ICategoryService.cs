using Store.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<ICollection<Category>> MainCategories();

        Task<ICollection<Product>> CategoryProducts(List<long> categoryIds);

        Task<List<long>> ChildCategoryIds(long categoryId);

    }
}
