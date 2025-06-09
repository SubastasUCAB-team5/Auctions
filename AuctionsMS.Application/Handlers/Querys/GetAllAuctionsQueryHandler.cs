using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Application.Queries;
using AuctionMS.Commons.Dtos.Response;
using AuctionMS.Core.Repositories;
using AuctionMS.Infrastructure.Exceptions;

namespace AuctionMS.Application.Handlers.Queries
{
    public class GetAllAuctionsQueryHandler : IRequestHandler<GetAllAuctionsQuery, List<GetAllAuctionsDto>>
    {
        private readonly IAuctionRepository _auctionRepository;

        public GetAllAuctionsQueryHandler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<List<GetAllAuctionsDto>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken)
        {
            var auctions = await _auctionRepository.GetAllAsync();
            if (auctions == null) throw new AuctionNotFoundException("Auctions not found.");

            return auctions.Select(auction => new GetAllAuctionsDto
            {
                AuctionId = auction.Id!,
                Name = auction.Name!,
                Description = auction.Description!,
                Images = auction.Images,
                BasePrice = auction.BasePrice,
                StartTime = auction.StartTime,
                EndTime = auction.EndTime,
                MinimumIncrement = auction.MinimumIncrement,
                ReservePrice = auction.ReservePrice,
                AuctionType = auction.AuctionType,
                CreatedAt = auction.CreatedAt,
                CreatedBy = "system",
                State = auction.State,
                Products = auction.Products
            }).ToList();
        }
    }
}

