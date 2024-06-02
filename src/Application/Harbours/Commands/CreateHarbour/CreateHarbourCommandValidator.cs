using FinalLabProject.Application.Common.Interfaces;

namespace FinalLabProject.Application.Harbours.Commands.CreateHarbour;

public class CreateHarbourCommandValidator : AbstractValidator<CreateHarbourCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateHarbourCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Harbours
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}
