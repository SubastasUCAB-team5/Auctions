using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Domain.Entities;

namespace AuctionMS.Commons.Dtos.Request
{
    public class ChangeAuctionStateDto
    {
        public Guid AuctionId { get; set; }
        public AuctionState NewState { get; set; }
    }
}
