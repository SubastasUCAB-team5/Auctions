using MediatR;
using AuctionMS.Application.Commands;
using AuctionMS.Core.Repositories;
using AuctionMS.Infrastructure.Exceptions;
using AuctionMS.Core.Service;

namespace AuctionMS.Application.Handlers.Commands
{
    public class UpdateAuctionCommandHandler : IRequestHandler<UpdateAuctionCommand, string>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IEventPublisher _eventPublisher;

        public UpdateAuctionCommandHandler(IAuctionRepository auctionRepository, IEventPublisher eventPublisher)
        {
            _auctionRepository = auctionRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<string> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _auctionRepository.GetByIdAsync(request.UpdateAuctionDto.AuctionId!);
            if (auction == null)
                throw new AuctionNotFoundException("Auction not found.");

            if (!string.IsNullOrEmpty(request.UpdateAuctionDto.Name)) auction.Name = request.UpdateAuctionDto.Name;
            if (!string.IsNullOrEmpty(request.UpdateAuctionDto.Description)) auction.Description = request.UpdateAuctionDto.Description;
            if (request.UpdateAuctionDto.Images?.Count > 0) auction.Images = request.UpdateAuctionDto.Images;
            if (!string.IsNullOrEmpty(request.UpdateAuctionDto.BasePrice)) auction.BasePrice = request.UpdateAuctionDto.BasePrice;
            if (request.UpdateAuctionDto.StartTime != null) auction.StartTime = request.UpdateAuctionDto.StartTime;
            if (request.UpdateAuctionDto.EndTime != null) auction.EndTime = request.UpdateAuctionDto.EndTime;
            if (!string.IsNullOrEmpty(request.UpdateAuctionDto.MinimumIncrement)) auction.MinimumIncrement = request.UpdateAuctionDto.MinimumIncrement;
            if (!string.IsNullOrEmpty(request.UpdateAuctionDto.ReservePrice)) auction.ReservePrice = request.UpdateAuctionDto.ReservePrice;
            if (!string.IsNullOrEmpty(request.UpdateAuctionDto.AuctionType)) auction.AuctionType = request.UpdateAuctionDto.AuctionType;
            if (request.UpdateAuctionDto.Products?.Count > 0) auction.Products = request.UpdateAuctionDto.Products;
            auction.UserId = request.UpdateAuctionDto.UserId;

            await _auctionRepository.UpdateAsync(auction);
            await _eventPublisher.PublishAuctionUpdatedAsync(auction);
            return "Auction updated successfully.";
        }
    }
}

