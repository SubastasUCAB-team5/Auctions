using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Commons.Dtos.Response;
using MediatR;

namespace AuctionMS.Application.Queries
{
    public class GetAuctionQuery : IRequest<GetAuctionDto>
    {
        public Guid AuctionId { get; set; }

        public GetAuctionQuery() { }

        public GetAuctionQuery(Guid auctionId)
        {
            AuctionId = auctionId;
        }
    }
}
