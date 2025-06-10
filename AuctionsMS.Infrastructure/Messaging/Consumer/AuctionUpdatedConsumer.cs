using MassTransit;
using AuctionMS.Domain.Entities;
using AuctionMS.Infrastructure.DataBase;
using AuctionMS.Commons.Events;
using MongoDB.Driver;

namespace AuctionMS.Infrastructure.Messaging.Consumers;

public class AuctionUpdatedConsumer : IConsumer<AuctionUpdatedEvent>
{
    private readonly MongoDbContext _mongo;

    public AuctionUpdatedConsumer(MongoDbContext mongo)
    {
        _mongo = mongo;
    }

    public async Task Consume(ConsumeContext<AuctionUpdatedEvent> context)
    {
        var message = context.Message;

        var filter = Builders<AuctionReadModel>.Filter.Eq(u => u.Id, message.Id);
        var update = Builders<AuctionReadModel>.Update
            .Set(u => u.Name, message.Name)
            .Set(u => u.Description, message.Description)
            .Set(u => u.Images, message.Images)
            .Set(u => u.BasePrice, message.BasePrice)
            .Set(u => u.StartTime, message.StartTime)
            .Set(u => u.EndTime, message.EndTime)
            .Set(u => u.MinimumIncrement, message.MinimumIncrement)
            .Set(u => u.ReservePrice, message.ReservePrice)
            .Set(u => u.AuctionType, message.AuctionType)
            .Set(u => u.Products, message.Products)
            .Set(u => u.State, message.State)
            .Set(u => u.CreatedAt, message.CreatedAt)
            .Set(u => u.UserId, message.UserId);


        await _mongo.Auctions.UpdateOneAsync(filter, update);
    }
}
