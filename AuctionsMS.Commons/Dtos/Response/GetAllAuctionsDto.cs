using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Domain.Entities;

namespace AuctionMS.Commons.Dtos.Response
{
    public record GetAllAuctionsDto
    {
        public Guid AuctionId { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public Guid UserId { get; set; }
    }
}

