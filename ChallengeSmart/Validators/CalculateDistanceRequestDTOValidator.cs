using ChallengeSmart.DTOS;
using FluentValidation;

namespace ChallengeSmart.Validators
{
    public class CalculateDistanceRequestDTOValidator : AbstractValidator<CalculateDistanceRequestDTO>
    {
        public CalculateDistanceRequestDTOValidator()
        {
            RuleFor(_ => _.originIata)
                .NotEmpty()              
                .WithMessage("This field is required");

            RuleFor(_ => _.destinationIata)
                .NotEmpty()
                .WithMessage("This field is required");
        }
    }
}
