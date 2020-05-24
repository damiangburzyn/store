using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Contracts.ViewModel;
using Store.Data;
using Store.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        //protected readonly ICategoriesRepository categoriesRepository;
        public ProductService(           
            IRepository repository)
            : base(repository)
        {
          //  categoriesRepository = cRepository;
        }

        public async Task<ICollection<Category>> MainCategories()
        {
            var cat =  await Repository.Find<Category>(predicate: (a) => a.ParentCategoryId == null);
            return cat;
        }

        public override async Task<Product> Add(Product entity)
        {

            await Repository.Add(entity);

            await Repository.SaveChangesAsync();

            return entity;
        }

        public override async Task Remove(long id)
        {
            var entity = await GetById(id);


          

            if (entity == null)
            {
                return;
            }


            //if (entity != null && entity.SubCategories.Count > 0)
            //{
            //    foreach (var category in entity.SubCategories.ToList())
            //    {
            //        await Remove(category.Id);
            //    }
            //}

            Repository.Remove<Category>(entity.Id);

            await Repository.SaveChangesAsync();
        }

      

        public override async Task<Product> Update(Product entity)
        {
        //    if (entity.ParentCategory != null)
        //    {
        //        entity.ParentCategory = null;
        //    }
            //if (entity.Name_Translation != null)
            //{
            //    Repository.Attach(entity.Name_Translation, EntityState.Modified);

            //    if (entity.Name_Translation.Values != null && entity.Name_Translation.Values.Count > 0)
            //    {
            //        foreach (var val in entity.Name_Translation.Values)
            //            Repository.Attach(val, EntityState.Modified);
            //    }
            //}
            Repository.Update(entity);
            await Repository.SaveChangesAsync();
            return entity;
        }
    }
}

