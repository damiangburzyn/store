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
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        //protected readonly ICategoriesRepository categoriesRepository;
        public CategoryService(
            IRepository repository)
            : base(repository)
        {
            //  categoriesRepository = cRepository;
        }

        public async Task<ICollection<Category>> MainCategories()
        {
            var cat = await Repository.Find<Category>(predicate: (a) => a.ParentCategoryId == null);
            return cat;
        }


        public async Task<ICollection<Product>> CategoryProducts(List<long> categoryIds)
        {
            var cat = await Repository.Find<Product>(predicate: (a) => a.ProductCategories.Any(a => categoryIds.Any(categoryId=> a.CategoryId == categoryId)));
            return cat;
        }

        public override async Task<Category> Add(Category entity)
        {
            if (entity.ParentCategory != null)
            {
                Repository.Attach(entity.ParentCategory);

                //if (entity.ParentCategory.Name_Translation != null)
                //    Repository.Attach(entity.ParentCategory.Name_Translation);
            }

            await Repository.Add(entity);

            await Repository.SaveChangesAsync();

            return entity;
        }

        public override async Task Remove(long id)
        {
            var entity = await GetById(id, q => q
                        //.Include(c => c.Name_Translation)
                        // .ThenInclude(v => v.Values)
                        .Include(c => c.SubCategories)
                        //.ThenInclude(t => t.Name_Translation)
                        //.ThenInclude(t => t.Values)
                        .Include(c => c.ParentCategory));


            await LoadNestedData(entity.SubCategories.ToList());

            if (entity == null)
            {
                return;
            }

            //if (entity.Name_Translation != null)
            //{
            //    if (entity.Name_Translation.Values != null && entity.Name_Translation.Values.Count > 0)
            //    {
            //        foreach (var val in entity.Name_Translation.Values)
            //            Repository.Remove<TranslationValue>(val.Id);
            //    }
            //    Repository.Remove<Translation>(entity.Name_Translation.Id);
            //}

            if (entity.SubCategories != null && entity.SubCategories.Count > 0)
            {
                foreach (var category in entity.SubCategories.ToList())
                {
                    await Remove(category.Id);
                }
            }

            Repository.Remove<Category>(entity.Id);

            await Repository.SaveChangesAsync();
        }

        private async Task<bool> LoadNestedData(List<Category> categories)
        {
            categories = categories.OrderBy(c => c.SortOrder).ToList();
            for (var i = 0; i < categories.Count; i++)
            {

                categories[i] = await GetById(categories[i].Id, q => q
                        //.Include(c => c.Name_Translation)
                        //.ThenInclude(v => v.Values)
                        .Include(c => c.SubCategories)
                        //.ThenInclude(t => t.Name_Translation)
                        //.ThenInclude(t => t.Values)
                        .Include(c => c.ParentCategory));
            }
            return true;
        }

        public override async Task<Category> Update(Category entity)
        {
            if (entity.ParentCategory != null)
            {
                entity.ParentCategory = null;
            }

            Repository.Attach(entity, EntityState.Modified);
            Repository.Update(entity);
            await Repository.SaveChangesAsync();
            return entity;
        }

        public async Task<List<long>> ChildCategoryIds(long categoryId)
        {
            var result = (await GetAll()).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                item.SubCategories = result.Where(a => a.ParentCategoryId == item.Id).ToList();
            }
            var currentCat = result.FirstOrDefault(a => a.Id == categoryId);
            var childCatIds = new List<long>();
            GetChildIds(currentCat,  childCatIds);
            return childCatIds;
        }

        private static void GetChildIds(Category currentCat,  List<long> childCatIds)
        {
            foreach (var item in currentCat.SubCategories)
            {
                childCatIds.Add(item.Id);
                if (item.SubCategories?.Any() == true)
                {
                    GetChildIds(item, childCatIds);
                }
            }
        }
    }
}

