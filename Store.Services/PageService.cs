//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Microsoft.EntityFrameworkCore;

//namespace GC5.Core.Services
//{
//    public class PageService : BaseService<Page>, IPageService
//    {
//        public PageService(
//            IRepository repository)
//            : base(repository)
//        {
//        }

//        public override async Task Update(Page entity)
//        {
//            if (entity.Description_Translation != null)
//            {
//                Repository.Attach(entity.Description_Translation, EntityState.Modified);

//                if (entity.Description_Translation.Values != null && entity.Description_Translation.Values.Count > 0)
//                {
//                    foreach (var val in entity.Description_Translation.Values)
//                        Repository.Attach(val, EntityState.Modified);
//                }
//            }

//            if (entity.Keys_Translation != null)
//            {
//                Repository.Attach(entity.Keys_Translation, EntityState.Modified);

//                if (entity.Keys_Translation.Values != null && entity.Keys_Translation.Values.Count > 0)
//                {
//                    foreach (var val in entity.Keys_Translation.Values)
//                        Repository.Attach(val, EntityState.Modified);
//                }
//            }

//            if (entity.Title_Translation != null)
//            {
//                Repository.Attach(entity.Title_Translation, EntityState.Modified);

//                if (entity.Title_Translation.Values != null && entity.Title_Translation.Values.Count > 0)
//                {
//                    foreach (var val in entity.Title_Translation.Values)
//                        Repository.Attach(val, EntityState.Modified);
//                }
//            }

//            Repository.Update(entity);

//            await Repository.SaveChangesAsync();
//        }

//        public override async Task Remove(long id)
//        {
//            var entity = await GetById(id, q => q
//                .Include(p => p.Description_Translation)
//                .ThenInclude(v => v.Values)
//                .Include(p => p.Keys_Translation)
//                .ThenInclude(v => v.Values)
//                .Include(p => p.Title_Translation)
//                .ThenInclude(p => p.Values));

//            if (entity.Description_Translation != null)
//            {
//                Repository.Attach(entity.Description_Translation, EntityState.Deleted);

//                if (entity.Description_Translation.Values != null)
//                {
//                    foreach (var v in entity.Description_Translation.Values)
//                        Repository.Attach(v, EntityState.Deleted);
//                }
//            }

//            if (entity.Keys_Translation != null)
//            {
//                Repository.Attach(entity.Keys_Translation, EntityState.Deleted);

//                if (entity.Keys_Translation.Values != null)
//                {
//                    foreach (var v in entity.Keys_Translation.Values)
//                        Repository.Attach(v, EntityState.Deleted);
//                }
//            }

//            if (entity.Title_Translation != null)
//            {
//                Repository.Attach(entity.Title_Translation, EntityState.Deleted);

//                if (entity.Title_Translation.Values != null && entity.Title_Translation.Values.Count > 0)
//                {
//                    foreach (var val in entity.Title_Translation.Values)
//                        Repository.Attach(val, EntityState.Deleted);
//                }
//            }

//            Repository.Remove<Page>(entity.Id);

//            await Repository.SaveChangesAsync();
//        }
//    }
//}
