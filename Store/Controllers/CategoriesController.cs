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
using Microsoft.Extensions.Logging;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<Category, CategoryViewModel>
    {
        private readonly ICategoryService categoryService;
        private readonly StorageHelper storageHelper;

        public CategoriesController(
            IOptions<AppSettings> settings, ILocalPageData pageData, IMapper mapper, ICategoryService categoryService, IMediaService mediaService, StorageHelper storageHelper, ILogger<CategoriesController> logger)
             : base(settings, pageData, mapper, categoryService, mediaService, logger)
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
                var result = (await _service.GetAll()).ToList();

                var vm = Mapper.Map<List<CategoryViewModel>>(result);
                for (int i = 0; i < vm.Count; i++)
                {
                    var item = vm[i];
                    item.SubCategories = vm.Where(a => a.ParentCategoryId == item.Id).ToList();
                }
                vm = vm.Where(a => a.ParentCategoryId == null).ToList();
                return Ok(vm);
            };

            return await func();
        }

        [ValidateAntiForgeryToken]
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

        [ValidateAntiForgeryToken]
        [HttpPut]
        public override async Task<ActionResult<CategoryViewModel>> Update(CategoryViewModel viewModel)
        {

            var model = await _service.GetById(viewModel.Id);
            var prevLogo = model.Logo;
            Mapper.Map<CategoryViewModel, Category>(viewModel, model);
            var result = await _service.Update(model);
            await _mediaService.SaveMedia(viewModel.Logo, storageHelper.CrateContainer<CategoryViewModel>(viewModel), null, prevLogo);
            var vm = Mapper.Map<CategoryViewModel>(result);
            return Ok(vm);
        }

        [HttpGet("main")]
        [AllowAnonymous]
        public async Task<IActionResult> MainCategories()
        {
            var mainCat = await categoryService.MainCategories();
            var res = Mapper.Map<List<CategoryViewModel>>(mainCat);
            return Ok(res);
        }

        [HttpGet("{id}/categoryproducts")]
        public async Task<ActionResult> CategoryProducts(long id)
        {
            Func<Task<ActionResult>> func = async () =>
            {
                var childCatIds = await categoryService.ChildCategoryIds(id);
                childCatIds.Add(id);
                var result = await categoryService.CategoryProducts(childCatIds);
                var vm = Mapper.Map<List<ProductViewModel>>(result);
                return Ok(vm);
            };
            return await func();
        }
    }
}