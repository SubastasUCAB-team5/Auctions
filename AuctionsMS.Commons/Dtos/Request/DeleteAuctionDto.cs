using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionMS.Commons.Dtos.Request
{
    public record DeleteAuctionDto
    {
        public Guid AuctionId { get; set; }
    }
}
