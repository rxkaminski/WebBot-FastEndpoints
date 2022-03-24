using FastEndpoints.Validation;
using WebBotFastEndpoints.Requests;

namespace WebBotFastEndpoints.Validators
{
    public class SearchRequestValidator : Validator<SearchRequest>
    {
        public SearchRequestValidator()
        {
            RuleFor(s => s.Title.Length)
                .GreaterThanOrEqualTo(3)
                .WithMessage("Title length more or equal than 3");
        }
    }
}
