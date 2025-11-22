namespace SurveyBasket.Api.Contracts.Results;

public record VotesPerDayRespone (

    DateOnly Date,
    int NumberOfVote
    );
