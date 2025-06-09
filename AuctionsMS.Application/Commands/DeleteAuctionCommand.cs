using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Commons.Dtos.Request;

namespace AuctionMS.Application.Commands
{
    public class DeleteAuctionCommand : IRequest<string>
    {
        public DeleteAuctionDto DeleteAuctionDto { get; set; }

        public DeleteAuctionCommand(DeleteAuctionDto dto)
        {
            DeleteAuctionDto = dto;
        }
    }
}
