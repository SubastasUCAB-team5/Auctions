using Microsoft.AspNetCore.Mvc;
using MediatR;
using AuctionMS.Commons.Dtos.Request;
using AuctionMS.Application.Commands;
using Microsoft.AspNetCore.Authorization;

namespace AuctionMS.Controllers
{
    [ApiController]
    [Route("auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly ILogger<AuctionsController> _logger;
        private readonly IMediator _mediator;

        public AuctionsController(ILogger<AuctionsController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger.LogInformation("AuctionsController instantiated");
        }

        [HttpPost("create-auction")]
        public async Task<IActionResult> CreateAuction(CreateAuctionDto createAuctionDto)
        {
            _logger.LogInformation("Received request to create a Auction");
            try
            {
                var command = new CreateAuctionCommand(createAuctionDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating auction: {Message}", e.Message);
                return StatusCode(500, "Error while creating auction.");
            }
        }

        [HttpPut("update-auction")]
        public async Task<IActionResult> UpdateAuction(UpdateAuctionDto updateAuctionDto)
        {
            try
            {
                var command = new UpdateAuctionCommand(updateAuctionDto);
                var msg = await _mediator.Send(command);
                return Ok(msg);
            }
            catch (Exception e)
            {
                _logger.LogError("Error updating auction: {Message}", e.Message);
                return StatusCode(500, "Error while updating auction.");
            }
        }

        [HttpDelete("delete-{id}")]
        public async Task<IActionResult> DeleteAuctionById(Guid id)
        {
            try
            {
                var command = new DeleteAuctionCommand(new DeleteAuctionDto { AuctionId = id });
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("Error deleting auction: {Message}", e.Message);
                return StatusCode(500, "Error while deleting auction.");
            }
        }

        [HttpPut("change-state")]
        public async Task<IActionResult> ChangeState(ChangeAuctionStateDto dto)
        {
            try
            {
                var command = new ChangeAuctionStateCommand(dto);
                var result = await _mediator.Send(command);
                return Ok(new { Message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Ocurrió un error interno al procesar la solicitud" });
            }
        }
    }
}


