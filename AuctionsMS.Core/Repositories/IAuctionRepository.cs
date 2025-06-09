using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Domain.Entities;

namespace AuctionMS.Core.Repositories
{
    public interface IAuctionRepository
    {
        Task AddAsync(Auction user);
        Task UpdateAsync(Auction user);
        Task DeleteAsync(Guid userId);
        Task<Auction?> GetByIdAsync(Guid userId);
        Task<List<Auction>> GetAllAsync();
    }
}
