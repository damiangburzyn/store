using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Store.Contracts;
using Microsoft.AspNetCore.Authorization;
using Store.Services;
using Store.Contracts.ViewModel;
using Store.Data.EF.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<Category, CategoryViewModel>
    {
        private readonly ICategoryService categoryService;
        private readonly StorageHelper storageHelper;

        public CategoriesController(
            IOptions<AppSettings> settings, ILocalPageData pageData, IMapper mapper, ICategoryService categoryService, IMediaService mediaService, StorageHelper storageHelper)
             : base(settings, pageData, mapper, categoryService, mediaService)
        {
            this.categoryService = categoryService;
            this.storageHelper = storageHelper;
        }







        // Get: api/categories/tree
        [HttpGet("tree")]
        public virtual async Task<ActionResult<List<CategoryViewModel>>> Tree()
        {
            Func<Task<ActionResult>> func = async () =>
            {
                var result = await _service.GetAll();
                var vm = Mapper.Map<List<CategoryViewModel>>(result);
                for (int i = 0; i < vm.Count; i++)
                {
                    var item = vm[i];
                    item.SubCategories = vm.Where(a => a.ParentCategoryId == item.Id).ToList();
                }
                vm = vm.Where(a => a.ParentCategoryId == null).ToList();
                return Ok(vm);
            };

            return await this.WrapExceptionAsync(async () =>
            {
                return await func();
            });
        }


        [HttpPost]
        public override async Task<ActionResult<CategoryViewModel>> Create(CategoryViewModel viewModel)
        {
            var model = Mapper.Map<Category>(viewModel);
            var result = await _service.Add(model);
            viewModel.Id = result.Id;
            await _mediaService.SaveMedia(viewModel.Logo, storageHelper.CrateContainer<CategoryViewModel>(viewModel));
            var vm = Mapper.Map<CategoryViewModel>(result);
            return Ok(vm);
        }

        [HttpPut]
        public override async Task<ActionResult<CategoryViewModel>> Update(CategoryViewModel viewModel)
        {
            return await this.WrapExceptionAsync(async () =>
            {
              
                var model = await _service.GetById(viewModel.Id);

                var prevLogo = model.Logo;
                Mapper.Map<CategoryViewModel,Category>(viewModel,model);
                var result = await _service.Update(model);
                await _mediaService.SaveMedia(viewModel.Logo, storageHelper.CrateContainer<CategoryViewModel>(viewModel), null, prevLogo);
                var vm = Mapper.Map<CategoryViewModel>(result);
                return Ok(vm);
            });
        }

        [ValidateAntiForgeryToken]
        [HttpGet("main")]
        [AllowAnonymous]
        public async Task<IActionResult> MainCategories()
        {
            return await this.WrapExceptionAsync(async () =>
            {
                var mainCat = await categoryService.MainCategories();
                var res = Mapper.Map<List<CategoryViewModel>>(mainCat);
                return Ok(res);

            });
        }
    }
}