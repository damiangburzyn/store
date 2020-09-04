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
            var res = await Repository.GetById<Product>(id, includeAction);
            res.Images = images.ToList();
            return res;
        }

        public async Task<ICollection<Category>> MainCategories()
        {
            var cat = await Repository.Find<Category>(predicate: (a) => a.ParentCategoryId == null);
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

            var actualpc = new List<ProductCategory>();
            foreach (var item in entity.ProductCategories)
            {
                actualpc.Add(item);
            }

            var pcFetched = await Repository.GetAll<ProductCategory>(pc => pc.Where(pcc => pcc.ProductId == entity.Id));
            //remove unused
            var pcToDelete = pcFetched.Where(a => !actualpc.Any(n => n.ProductId == a.ProductId && n.CategoryId == a.CategoryId)).ToList();
            if (pcToDelete != null)
            {
                foreach (var item in pcToDelete)
                {
                    //Repository.Attach(item, EntityState.Deleted);
                }
                Repository.RemoveBatch(pcToDelete);
            }
            pcFetched = pcFetched.Where(a => actualpc.Any(n => n.ProductId == a.ProductId && n.CategoryId == a.CategoryId)).ToList();
            foreach (var item in entity.ProductCategories)
            {
                if (!pcFetched.Any(a => a.ProductId == item.ProductId && a.CategoryId == item.CategoryId))
                {
                    pcFetched.Add(item);
                }
            }
            entity.ProductCategories = pcFetched;


            var actualdm = new List<ProductDeliveryMethod>();
            foreach (var item in entity.DeliveryMethods)
            {
                actualdm.Add(item);
            }


            var dmFetched = await Repository.GetAll<ProductDeliveryMethod>(pd => pd.Where(pdm => pdm.ProductId == entity.Id));
            var dmToDelete = dmFetched.Where(a => !actualdm.Any(n => n.ProductId == a.ProductId && n.DeliveryId == a.DeliveryId)).ToList();
            if (dmToDelete != null)
            {
                foreach (var item in dmToDelete)
                {
                  
                  //  Repository.Attach(item, EntityState.Deleted);
                }
              Repository.RemoveBatch(dmToDelete);
            }
            dmFetched = dmFetched.Where(a => actualdm.Any(n => n.ProductId == a.ProductId && n.DeliveryId == a.DeliveryId)).ToList();
            foreach (var item in entity.DeliveryMethods)
            {
                var existing = dmFetched.FirstOrDefault(a => a.ProductId == item.ProductId && a.DeliveryId == item.DeliveryId);

                if (existing != null)
                {
                    existing.MaxCountInPackage = item.MaxCountInPackage;
                    existing.Price = item.Price;
                   // Repository.Attach(existing, EntityState.Modified);
                }
                else
                {
                    dmFetched.Add(item);
                    //Repository.Attach(item, EntityState.Added);
                }
            }

            entity.DeliveryMethods = dmFetched;


            Repository.Update(entity);
            await Repository.SaveChangesAsync();
            return entity;
        }
    }
}

