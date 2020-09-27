using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Store.Contracts;
using Store.Contracts.ViewModel;
using Store.Data.EF.Entities;
using Store.Services;

namespace Store.Controllers
{
    // [AutoValidateAntiforgeryToken]
    public abstract class BaseController<TEntity, TViewModel> : BaseController
    where TViewModel : BaseViewModel
    where TEntity : class, IBaseEntity
    {
        protected readonly IBaseService<TEntity> _service;
        protected readonly IMediaService _mediaService;


        public BaseController(IOptions<AppSettings> settings,
            ILocalPageData pageData,
            IMapper mapper, IBaseService<TEntity> service,
            IMediaService mediaService, 
            ILogger logger)
            : base(settings, pageData, mapper, logger)
        {
            _service = service;
            _mediaService = mediaService;
        }


        [HttpGet("search")]
        public virtual async Task<IActionResult> Search(string query, string culture = null, bool withCount = false, int? page = null, int? pageSize = null)
        {


            return await this.WrapExceptionAsync(async () =>
            {

                DataTableSearchViewModel<TEntity> result = null;
                IEnumerable<PropertyInfo> stringProps = null;
                if (!string.IsNullOrWhiteSpace(query))
                {
                    stringProps = typeof(TEntity).GetProperties().Where(prop =>
                         prop.PropertyType == query.GetType());


                    result = await _service.Search( page, pageSize, null,    q => stringProps.Any(p =>
                    (p.GetValue(q, null) as string).Contains(query)));

                }
                else {

                    result = await _service.Search(page, pageSize, null);

                }

                var vm = Mapper.Map<DataTableSearchViewModel<TViewModel>>(result);
                return Ok(vm);
            });

        }



        // DELETE: api/controller/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(long id)
        {
            Func<Task<ActionResult>> func = async () =>
            {
                var todoItem = await _service.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            await _service.Remove(id);
            return Ok();
            };

            return await this.WrapExceptionAsync(async () =>
            {
                return await func();
            });
        }

        // Get: api/controller
        [HttpGet()]
        public virtual async Task<ActionResult<TViewModel>> List()
        {
            Func<Task<ActionResult>> func = async () =>
            {
                var result = await _service.GetAll();              
                var vm = Mapper.Map<List<TViewModel>>(result);
                return Ok(vm);
            };

            return await this.WrapExceptionAsync(async () =>
            {
                return await func();
            });
        }



        // Get: api/categories/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TViewModel>> Get(long id)
        {
            Func<Task<ActionResult>> func = async () =>
            {
                var result = await _service.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
               
                var vm = Mapper.Map<TViewModel>(result);
                return Ok(vm);
            };

            return await this.WrapExceptionAsync(async () =>
            {
                return await func();
            });

        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult<TViewModel>> Create(TViewModel viewModel)
        {
            Func<Task<ActionResult>> func = async () =>
            {
                var model = Mapper.Map<TEntity>(viewModel);
                var result = await _service.Add(model);
                var vm = Mapper.Map<TViewModel>(result);
                return Ok(vm);
            };

            return await this.WrapExceptionAsync(async () =>
            {
                return await func();
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPut]
        public virtual async Task<ActionResult<TViewModel>> Update(TViewModel viewModel)
        {
            Func<Task<ActionResult>> func = async () =>
            {
                var model = Mapper.Map<TEntity>(viewModel);
                var result = await _service.Update(model);
                var vm = Mapper.Map<TViewModel>(result);
                return Ok(vm);
            };
            return await this.WrapExceptionAsync(async () =>
            {
                return await func();
            });
        }
    }
}