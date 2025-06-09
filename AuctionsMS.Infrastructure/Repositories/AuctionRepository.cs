using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Core.Repositories;
using AuctionMS.Domain.Entities;
using AuctionMS.Infrastructure.DataBase;
using AuctionMS.Infrastructure.Exceptions;


namespace AuctionMS.Infrastructure.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionsDbContext _dbContext;

        public AuctionRepository(AuctionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Auction auction)
        {
            await _dbContext.Auctions.AddAsync(auction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid auctionId)
        {
            var auctionEntity = await _dbContext.Auctions.FindAsync(auctionId);
            if (auctionEntity == null)
            {
                throw new Exception("Auction not found.");
            }
            _dbContext.Auctions.Remove(auctionEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Auction auction)
        {
            _dbContext.Auctions.Update(auction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Auction?> GetByIdAsync(Guid auctionId)
        {
            return await _dbContext.Auctions.FirstOrDefaultAsync(u => u.Id == auctionId);
        }

        public async Task<List<Auction>> GetAllAsync()
        {
            return await _dbContext.Auctions.ToListAsync();
        }
    }

}
