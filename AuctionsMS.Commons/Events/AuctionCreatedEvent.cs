
using AuctionMS.Domain.Entities;
using AuctionMS.Domain.ValueObjects;

namespace AuctionMS.Commons.Events;

public class AuctionCreatedEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<string> Images { get; set; } = new List<string>();
    public string BasePrice { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string MinimumIncrement { get; set; }
    public string ReservePrice { get; set; }
    public string AuctionType { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public AuctionState State { get; set; } = AuctionState.Active;
    public List<string> Products { get; set; } = new List<string>();
    public Guid UserId { get; set; }
}
