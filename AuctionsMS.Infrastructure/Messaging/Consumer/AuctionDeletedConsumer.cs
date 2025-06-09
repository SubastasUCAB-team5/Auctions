using MassTransit;
using AuctionMS.Domain.Entities;
using AuctionMS.Infrastructure.DataBase;
using AuctionMS.Commons.Events;
using MongoDB.Driver;

namespace AuctionMS.Infrastructure.Messaging.Consumers;

public class AuctionDeletedConsumer : IConsumer<AuctionDeletedEvent>
{
    private readonly MongoDbContext _mongo;

    public AuctionDeletedConsumer(MongoDbContext mongo)
    {
        _mongo = mongo;
    }

    public async Task Consume(ConsumeContext<AuctionDeletedEvent> context)
    {
        var message = context.Message;
        var filter = Builders<AuctionReadModel>.Filter.Eq(u => u.Id, message.Id);
        await _mongo.Auctions.DeleteOneAsync(filter);
    }
}
