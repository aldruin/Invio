using Invio.Domain.Entities;
using Invio.Domain.Repositories;
using Invio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Infrastructure.Repositories
{
    public class ItemRepository : Repository<Item> , IItemRepository
    {
        public ItemRepository(InvioDbContext context) : base(context)
        {
        }
    }
}
