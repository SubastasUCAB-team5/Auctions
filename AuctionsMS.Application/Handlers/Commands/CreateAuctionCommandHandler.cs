using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionMS.Application.Commands;
using AuctionMS.Core.Repositories;
using AuctionMS.Core.Service;
using AuctionMS.Domain.Entities;
using AuctionMS.Infrastructure.Service;

namespace AuctionMS.Application.Handlers.Commands
{
    public class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, string>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IEventPublisher _eventPublisher;

        public CreateAuctionCommandHandler(IAuctionRepository auctionRepository, IEventPublisher eventPublisher)
        {
            _auctionRepository = auctionRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<string> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateAuctionDto;

            var auction = new Auction(
                dto.Name!,
                dto.Description!, 
                dto.Images,
                dto.BasePrice,
                dto.StartTime,
                dto.EndTime,
                dto.MinimumIncrement,
                dto.ReservePrice,
                dto.AuctionType,
                dto.Products,
                dto.State
            )
            {
                State = dto.State
            };

            await _auctionRepository.AddAsync(auction);
            await _eventPublisher.PublishAuctionCreatedAsync(auction);

            return "Auction successfully created.";
        }
    }
}
