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

        public override async Task<Product> GetById(long id, Func<IQueryable<Product>, IQueryable<Product>> includeAction = null)
        {
            var images = await Repository.Find<GalleryImage>(x => x.ProductId == id);
            var res =   await  Repository.GetById<Product>(id, includeAction);
            res.Images = images.ToList();
            return res;
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
            Repository.Remove<Category>(entity.Id);
            await Repository.SaveChangesAsync();
        }

      

        public override async Task<Product> Update(Product entity)
        {
            //    if (entity.ParentCategory != null)
            //    {
            //        entity.ParentCategory = null;
            //    }
            if (entity.ProductCategories != null)
            {
                foreach (var item in entity.ProductCategories)
                {
                    if (item != null )
                      
                            Repository.Attach(item, EntityState.Modified);
                    }
                      
            }

            if (entity.DeliveryMethods != null)
            {
                foreach (var item in entity.DeliveryMethods)
                {
                    if (item != null)

                        Repository.Attach(item, EntityState.Modified);
                }

            }
            Repository.Update(entity);
            await Repository.SaveChangesAsync();
            return entity;
        }
    }
}

