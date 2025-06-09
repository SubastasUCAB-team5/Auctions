using MediatR;
using AuctionMS.Commons.Dtos.Request;

namespace AuctionMS.Application.Commands
{
    public class ChangeAuctionStateCommand : IRequest<string>
    {
        public ChangeAuctionStateDto ChangeAuctionStateDto { get; }

        public ChangeAuctionStateCommand(ChangeAuctionStateDto changeAuctionStateDto)
        {
            ChangeAuctionStateDto = changeAuctionStateDto;
        }
    }
}   
