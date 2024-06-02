namespace FinalLabProject.Application.Boats.Commands.UpdateBoat;

public class UpdateBoatCommandValidator : AbstractValidator<UpdateBoatCommand>
{
    public UpdateBoatCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
