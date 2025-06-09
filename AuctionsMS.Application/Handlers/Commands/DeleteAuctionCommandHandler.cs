using MediatR;
using AuctionMS.Application.Commands;
using AuctionMS.Core.Repositories;
using AuctionMS.Core.Service;

namespace AuctionMS.Application.Handlers.Commands
{
    public class DeleteAuctionCommandHandler : IRequestHandler<DeleteAuctionCommand, string>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IEventPublisher _eventPublisher;

        public DeleteAuctionCommandHandler(IAuctionRepository auctionRepository, IEventPublisher eventPublisher)
        {
            _auctionRepository = auctionRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<string> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
        {
            var auctionId = request.DeleteAuctionDto.AuctionId;

            var auction = await _auctionRepository.GetByIdAsync(auctionId);
            if (auction == null)
                throw new ApplicationException($"No auction found with ID {auctionId}");

            if (string.IsNullOrEmpty(auction.Name))
                throw new ApplicationException("Auction email cannot be null or empty.");

            await _auctionRepository.DeleteAsync(auctionId);
            await _eventPublisher.PublishAuctionDeletedAsync(auction);
            return "Auction successfully disabled.";
        }
    }
}

