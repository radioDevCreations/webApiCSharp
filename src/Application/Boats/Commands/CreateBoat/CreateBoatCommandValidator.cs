using FinalLabProject.Application.Boats.Commands.CreateBoat;

public class CreateBoatCommandValidator : AbstractValidator<CreateBoatCommand>
{
    public CreateBoatCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
