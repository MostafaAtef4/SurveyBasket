using SurveyBasket.Api.Contracts.Results;

namespace SurveyBasket.Api.Services;

public interface IResultService
{
    Task<Result<PollVotesResponse>> GetPollVotesAsync(int pollId, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<VotesPerDayRespone>>> GetVotesPerDayAsync(int pollId, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesQuestionAsync(int pollId, CancellationToken cancellationToken = default);
}
