using Invio.Domain.Entities;
using Invio.Domain.Repositories;
using Invio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Item>> ObterItemsPorEquipeIdAsync(Guid equipeId)
        {
            try
            {
                var items = await Query.Where(item => item.EquipeId == equipeId).ToListAsync();
                return items;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Erro ao buscar items por ID da equipe.", ex);
            }
        }
    }
}
