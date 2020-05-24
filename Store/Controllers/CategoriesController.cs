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

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;
        CategoriesController(
            IOptions<AppSettings> settings, ILocalPageData pageData, IMapper mapper, ICategoryService categoryService)
             : base(settings, pageData, mapper)
        {
            this.categoryService = categoryService;
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