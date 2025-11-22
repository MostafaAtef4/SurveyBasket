
using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Api.Errors;

namespace SurveyBasket.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;


    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)=>
         Ok(await _pollService.GetAllAsync(cancellationToken));


    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken) =>
        Ok(await _pollService.GetCurrentAsync(cancellationToken));


    [HttpGet("{id}")]

    async public Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {

        var result = await _pollService.GetAsync(id, cancellationToken);



        return result.IsSuccess ? Ok(result.Value) : Problem(statusCode:StatusCodes.Status404NotFound, title: result.Error.Code, detail: result.Error.Description);

    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] PollRequest request,
        CancellationToken cancellationToken)
    {

        var result = await _pollService.AddAsync(request, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
            : result.ToProblem();

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
    {
        var result = await _pollService.UpdateAsync(id, request, cancellationToken);

        if (result.IsSuccess)
            return NoContent();

        return result.Error.Equals(PollErrors.DuplicatedPollTitle)
                ? result.ToProblem()
                : result.ToProblem();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _pollService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem( );

    }
    [HttpPut("{id}/togglePublish")]
    public async Task<IActionResult> TogglePublishStatus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _pollService.TogglePublishStatusAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }


}
