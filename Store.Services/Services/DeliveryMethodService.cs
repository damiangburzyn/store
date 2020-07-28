using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Contracts.ViewModel;
using Store.Data;
using Store.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Store.Services
{
   public class DeliveryMethodService : BaseService<DeliveryMethod>, IDeliveryMethodService
    {
        public DeliveryMethodService(
            IRepository repository)
            : base(repository)
        {
            //  categoriesRepository = cRepository;
        }
    }
}
