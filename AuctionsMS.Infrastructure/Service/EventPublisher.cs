using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using AuctionMS.Domain.Entities;
using AuctionMS.Commons.Events;
using AuctionMS.Core.Service;

namespace AuctionMS.Infrastructure.Service
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAuctionCreatedAsync(Auction auction)
        {
            var @event = new AuctionCreatedEvent
            {
                Id = auction.Id,
                Name = auction.Name,
                Description = auction.Description,
                StartTime = auction.StartTime,
                EndTime = auction.EndTime,
                MinimumIncrement = auction.MinimumIncrement,
                ReservePrice = auction.ReservePrice,
                AuctionType = auction.AuctionType,
                BasePrice = auction.BasePrice,
                Images = auction.Images,
                State = auction.State,
                Products = auction.Products,
                CreatedAt = DateTime.UtcNow

            };

            await _publishEndpoint.Publish(@event);
        }

        public async Task PublishAuctionUpdatedAsync(Auction auction)
        {
            var @event = new AuctionUpdatedEvent
            {
                Id = auction.Id,
                Name = auction.Name,
                Description = auction.Description,
                BasePrice = auction.BasePrice,
                StartTime = auction.StartTime,
                EndTime = auction.EndTime,
                MinimumIncrement = auction.MinimumIncrement,
                ReservePrice = auction.ReservePrice,
                AuctionType = auction.AuctionType,
                Images = auction.Images,
                State = auction.State,
                Products = auction.Products,
                CreatedAt = DateTime.UtcNow
            };

            await _publishEndpoint.Publish(@event);
        }

        public async Task PublishAuctionDeletedAsync(Auction auction)
        {
            var @event = new AuctionDeletedEvent
            {
                Id = auction.Id,
                Name = auction.Name,
                Description = auction.Description,
                BasePrice = auction.BasePrice,
                StartTime = auction.StartTime,
                EndTime = auction.EndTime,
                MinimumIncrement = auction.MinimumIncrement,
                ReservePrice = auction.ReservePrice,
                AuctionType = auction.AuctionType,
                Images = auction.Images,
                State = auction.State,
                Products = auction.Products,
                CreatedAt = DateTime.UtcNow
            };

            await _publishEndpoint.Publish(@event);
        }
    }
}

