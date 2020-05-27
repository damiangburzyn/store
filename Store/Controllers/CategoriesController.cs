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

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<Category, CategoryViewModel>
    {
        private readonly ICategoryService categoryService;
      public  CategoriesController(
            IOptions<AppSettings> settings, ILocalPageData pageData, IMapper mapper, ICategoryService categoryService, IMediaService mediaService)
             : base(settings, pageData, mapper, categoryService, mediaService)
        {
            this.categoryService = categoryService;
        }
        const string storage = "Categories";






        [HttpPost]
        public override async Task<ActionResult<CategoryViewModel>> Create(CategoryViewModel viewModel)
        {
            var model = Mapper.Map<Category>(viewModel);
            _mediaService.SaveMedia(viewModel, storage);
            var result = await _service.Add(model);
            var vm = Mapper.Map<CategoryViewModel>(result);
            return Ok(vm);
        }

        [HttpPut]
        public override  async Task<ActionResult<CategoryViewModel>> Update(CategoryViewModel viewModel)
        {
            var model = Mapper.Map<Category>(viewModel);
            var result = await _service.Update(model);
            var vm = Mapper.Map<CategoryViewModel>(result);
            return Ok(vm);
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