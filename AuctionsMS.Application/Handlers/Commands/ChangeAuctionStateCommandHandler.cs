using MediatR;
using AuctionMS.Application.Commands;
using AuctionMS.Core.Repositories;
using AuctionMS.Domain.Exceptions;
using AuctionMS.Infrastructure.Exceptions;

namespace AuctionMS.Application.Handlers.Commands
{
    public class ChangeAuctionStateCommandHandler : IRequestHandler<ChangeAuctionStateCommand, string>
    {
        private readonly IAuctionRepository _auctionRepository;

        public ChangeAuctionStateCommandHandler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<string> Handle(ChangeAuctionStateCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ChangeAuctionStateDto;
            var auction = await _auctionRepository.GetByIdAsync(dto.AuctionId);

            if (auction == null)
            {
                throw new AuctionNotFoundException($"No se encontró la subasta con ID {dto.AuctionId}");
            }

            try
            {
                auction.ChangeState(dto.NewState);
                auction.UpdatedAt = DateTime.UtcNow; 
                await _auctionRepository.UpdateAsync(auction);
                
                return $"Estado de la subasta actualizado correctamente a {dto.NewState}";
            }
            catch (InvalidAuctionStateTransitionException ex)
            {
                throw new InvalidOperationException($"No se puede cambiar el estado de la subasta: {ex.Message}", ex);
            }
            catch (Exception)
            {
                // Log the exception
                throw new Exception("Ocurrió un error al intentar cambiar el estado de la subasta");
            }
        }
    }
}   
