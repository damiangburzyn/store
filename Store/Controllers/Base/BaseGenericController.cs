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
        public readonly IBaseService<TEntity> _service;
        public readonly IMediaService _mediaService;

        public BaseController(IOptions<AppSettings> settings, ILocalPageData pageData, IMapper mapper, IBaseService<TEntity> service, IMediaService mediaService)
            : base(settings, pageData, mapper)
        {
            _service = service;
            _mediaService = mediaService;
        }



        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var todoItem = await _service.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            await _service.Remove(id);
            return Ok();
        }

        // Get: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TViewModel>> Get(long id)
        {
            var todoItem = await _service.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            var result = await _service.GetById(id);
            var vm = Mapper.Map<TViewModel>(result);
            return Ok(vm);
        }



        [HttpPost]
        public virtual async Task<ActionResult<TViewModel>> Create(TViewModel viewModel)
        {
            var model = Mapper.Map<TEntity>(viewModel);
            var result = await _service.Add(model);
            var vm = Mapper.Map<TViewModel>(result);
            return Ok(vm);
        }

        [HttpPut]
        public virtual async Task<ActionResult<TViewModel>> Update(TViewModel viewModel)
        {
            var model = Mapper.Map<TEntity>(viewModel);
            var result = await _service.Update(model);
            var vm = Mapper.Map<TViewModel>(result);
            return Ok(vm);
        }
    }
}