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
    public class GetAuctionQueryHandler : IRequestHandler<GetAuctionQuery, GetAuctionDto>
    {
        private readonly IAuctionRepository _auctionRepository;

        public GetAuctionQueryHandler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<GetAuctionDto> Handle(GetAuctionQuery request, CancellationToken cancellationToken)
        {
            var auction = await _auctionRepository.GetByIdAsync(request.AuctionId);

            if (auction == null)
                throw new AuctionNotFoundException("Auction not found.");

            return new GetAuctionDto
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
            };
        }
    }
}

