using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Domain.Entities;

namespace AuctionMS.Core.Service
{
    public interface IEventPublisher
    {
        Task PublishAuctionCreatedAsync(Auction auction);
        Task PublishAuctionUpdatedAsync(Auction auction);
        Task PublishAuctionDeletedAsync(Auction auction);        
        Task PublishAuctionProductsAsync(Guid auctionId, List<string> products);
    }
}
