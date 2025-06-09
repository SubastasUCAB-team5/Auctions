using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Commons.Dtos.Request;

namespace AuctionMS.Application.Commands
{
    public class CreateAuctionCommand : IRequest<string>
    {
        public CreateAuctionDto CreateAuctionDto { get; set; }

        public CreateAuctionCommand(CreateAuctionDto dto)
        {
            CreateAuctionDto = dto;
        }
    }
}
