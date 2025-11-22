
namespace SurveyBasket.Api.Errors;

public class VoteErrors
{
    public static readonly Error InvalidQuestions = new("Vote.InvalidQuestions", "Invalid Questions ", StatusCodes.Status400BadRequest);
    public static readonly Error DuplicatedVote = new("Vote.DuplicatedTitle", " This user already Voted", StatusCodes.Status409Conflict);
}
