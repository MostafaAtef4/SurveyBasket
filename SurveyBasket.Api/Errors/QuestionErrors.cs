namespace SurveyBasket.Api.Errors;

public class QuestionErrors
{
    public static readonly Error QuestionNotFound = new("Question.NotFound", "No Question  was found with the given ID  ");
    public static readonly Error DuplicatedQuestionTitle = new("Question.DuplicatedTitle", " Another Question with the same content is already exists  ");
}
