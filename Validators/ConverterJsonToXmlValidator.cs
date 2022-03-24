using FastEndpoints.Validation;
using WebBotFastEndpoints.Requests;

namespace WebBotFastEndpoints.Validators
{
    public class ConverterJsonToXmlValidator: Validator<ConverterJsonToXmlRequest>
    {
        public ConverterJsonToXmlValidator()
        {
            RuleFor(c => c.JsonEndPoint)
                .Matches("^https*.+$")
                .WithMessage("JsonEndPont should start with https or http");
        }   
    }
}
