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
using System.Linq.Expressions;


namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryMethodsController : BaseController<DeliveryMethod, DeliveryMethodViewModel>
    {
        private IBaseService<DeliveryMethod> deliverMethodService;

        public DeliveryMethodsController(
        IOptions<AppSettings> settings, ILocalPageData pageData, IMapper mapper, IDeliveryMethodService deliverMethodService, IMediaService mediaService)
         : base(settings, pageData, mapper, deliverMethodService, mediaService)
        {
            this.deliverMethodService = deliverMethodService;
       
        }
    }
}
