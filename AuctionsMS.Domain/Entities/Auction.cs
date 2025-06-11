using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Domain.Entities;
using AuctionMS.Domain.Exceptions;
using AuctionMS.Domain.ValueObjects;

namespace AuctionMS.Domain.Entities
{
    public enum AuctionState
    {
        Active,
        Ended,
        Completed,
        Canceled
    }

    public class Auction : Base
    {
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
        public List<string> Products { get; set; } = new List<string>();
        public Guid UserId { get; set; }

        public Auction() { }

        public Auction(string name, string description, List<string> images, string basePrice, DateTime startTime, DateTime endTime, string minimumIncrement, string reservePrice, string auctionType, List<string> products, Guid userId, AuctionState state = AuctionState.Active)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Images = images ?? new List<string>();
            BasePrice = basePrice;
            StartTime = startTime;
            EndTime = endTime;
            MinimumIncrement = minimumIncrement;
            ReservePrice = reservePrice;
            AuctionType = auctionType;
            State = state;
            Products = products ?? new List<string>();
            CreatedAt = DateTime.UtcNow;
            UserId = userId;
        }

        public void Update(string name, string description, string basePrice, string minimumIncrement, string reservePrice, string auctionType, List<string> images, List<string> products, Guid userId)
        {
            if (AuctionStateTransitions.IsImmutable(State))
            {
                throw new InvalidOperationException($"No se puede modificar una subasta en estado {State}");
            }

            Name = name;
            Description = description;
            BasePrice = basePrice;
            MinimumIncrement = minimumIncrement;
            ReservePrice = reservePrice;
            AuctionType = auctionType;
            Products = products ?? new List<string>();
            UserId = userId;
            
            if (images != null && images.Any())
            {
                Images = images;
            }
            
            UpdatedAt = DateTime.UtcNow; 
        }

        public void ChangeState(AuctionState newState)
        {
            if (State == newState)
                return;

            if (!AuctionStateTransitions.IsValidTransition(State, newState))
            {
                throw new InvalidAuctionStateTransitionException(State, newState);
            }

            State = newState;
            UpdatedAt = DateTime.UtcNow; 
        }

        public bool CanTransitionTo(AuctionState newState)
        {
            return AuctionStateTransitions.IsValidTransition(State, newState);
        }

        public bool IsImmutable()
        {
            return AuctionStateTransitions.IsImmutable(State);
        }
    }
}

