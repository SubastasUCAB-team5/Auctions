using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Commons.Dtos.Response;

namespace AuctionMS.Application.Queries
{
    public class GetAllAuctionsQuery : IRequest<List<GetAllAuctionsDto>>
    {
        public GetAllAuctionsQuery() { }
    }
}

