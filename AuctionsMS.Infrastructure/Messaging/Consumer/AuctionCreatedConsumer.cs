using MassTransit;
using AuctionMS.Domain.Entities;
using AuctionMS.Infrastructure.DataBase;
using AuctionMS.Commons.Events;

namespace AuctionMS.Infrastructure.Messaging.Consumers;

public class AuctionCreatedConsumer : IConsumer<AuctionCreatedEvent>
{
    private readonly MongoDbContext _mongo;

    public AuctionCreatedConsumer(MongoDbContext mongo)
    {
        _mongo = mongo;
    }

    public async Task Consume(ConsumeContext<AuctionCreatedEvent> context)
    {
        var message = context.Message;

        var user = new AuctionReadModel
        {
            Id = message.Id,
            Name = message.Name,
            Description = message.Description,
            Images = message.Images,
            BasePrice = message.BasePrice,
            StartTime = message.StartTime,
            EndTime = message.EndTime,
            MinimumIncrement = message.MinimumIncrement,
            ReservePrice = message.ReservePrice,
            AuctionType = message.AuctionType,
            Products = message.Products,
            State = message.State,
            CreatedAt = message.CreatedAt
        };

        await _mongo.Auctions.InsertOneAsync(user);
    }

}
