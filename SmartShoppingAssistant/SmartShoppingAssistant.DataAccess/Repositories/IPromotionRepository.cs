using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
       Task<List<Promotion>> GetForProductAsync(int productId);
    }
}
