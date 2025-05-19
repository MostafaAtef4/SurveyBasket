
namespace SurveyBasket.Api.Errors;

public class VoteErrors
{
    //public static readonly Error PollNotFound = new("Poll.NotFound", "No Poll  was found with the given ID  ");
    public static readonly Error DuplicatedVote = new("Vote.DuplicatedTitle", " This user already Voted");
}
