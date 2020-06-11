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



  




        [HttpGet("find")]
        public virtual async Task<IActionResult> Find(string query, string culture = null, bool withCount = false, int? page = null, int? pageSize = null)
        {
            return await this.WrapExceptionAsync(async () =>
            {
           

                var result = await productService.Find();

                return Ok(Mapper.Map<ICollection<ProductViewModel>>(result));
            });
        }



    }
}