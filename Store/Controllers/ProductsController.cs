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
    public class ProductsController : BaseController<Product, ProductViewModel>
    {
        private readonly IProductService productService;
        private readonly StorageHelper storageHelper;

        public ProductsController(
            IOptions<AppSettings> settings, ILocalPageData pageData, IMapper mapper, IProductService productService, IMediaService mediaService, StorageHelper storageHelper)
             : base(settings, pageData, mapper, productService, mediaService)
        {
            this.productService = productService;
            this.storageHelper = storageHelper;
        }



  




        [HttpGet("search")]
        public override async Task<IActionResult> Search(string query, string culture = null, bool withCount = false, int? page = null, int? pageSize = null)
        {
            return await this.WrapExceptionAsync(async () =>
            {
          
                var result = await productService.Search();
                var vm = Mapper.Map<DataTableSearchViewModel<ProductViewModel>>(result);
                return Ok(vm);
            });
        }




        //[HttpPost]
        //public override async Task<ActionResult<CategoryViewModel>> Create(CategoryViewModel viewModel)
        //{
        //    var model = Mapper.Map<Category>(viewModel);
        //    var result = await _service.Add(model);
        //    await _mediaService.SaveMedia(viewModel.Logo, storageHelper.CrateContainer<CategoryViewModel>(viewModel));
        //    var vm = Mapper.Map<CategoryViewModel>(result);
        //    return Ok(vm);
        //}

        //[HttpPut]
        //public override async Task<ActionResult<CategoryViewModel>> Update(CategoryViewModel viewModel)
        //{
        //    return await this.WrapExceptionAsync(async () =>
        //    {

        //        var model = await _service.GetById(viewModel.Id);

        //        var prevLogo = model.Logo;
        //        Mapper.Map<CategoryViewModel, Category>(viewModel, model);
        //        var result = await _service.Update(model);
        //        await _mediaService.SaveMedia(viewModel.Logo, storageHelper.CrateContainer<CategoryViewModel>(viewModel), null, prevLogo);
        //        var vm = Mapper.Map<CategoryViewModel>(result);
        //        return Ok(vm);
        //    });
        //}


    }
}