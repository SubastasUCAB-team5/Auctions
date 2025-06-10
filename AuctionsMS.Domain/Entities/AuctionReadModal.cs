using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionMS.Domain.Entities;

public class AuctionReadModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<string> Images { get; set; } = new List<string>();
    public string BasePrice { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string MinimumIncrement { get; set; }
    public string ReservePrice { get; set; }
    public string AuctionType { get; set; } = default!;
    public AuctionState State { get; set; }
    [BsonRepresentation(BsonType.String)]
    public List<Guid> Products { get; set; } = new List<Guid>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

