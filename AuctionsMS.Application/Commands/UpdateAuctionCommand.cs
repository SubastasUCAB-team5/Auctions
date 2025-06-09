using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Commons.Dtos.Request;


namespace AuctionMS.Application.Commands
{
    public class UpdateAuctionCommand : IRequest<string>
    {
        public UpdateAuctionDto UpdateAuctionDto { get; set; }

        public UpdateAuctionCommand(UpdateAuctionDto dto)
        {
            UpdateAuctionDto = dto;
        }
    }
}
