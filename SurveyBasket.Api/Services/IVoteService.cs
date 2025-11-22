using SurveyBasket.Api.Contracts.Votes;

namespace SurveyBasket.Api.Services;

public interface IVoteService
{
    Task<Result> AddAsync(int pollId, string UserId, VoteRequest request, CancellationToken cancellationToken=default);
}
