using FinalLabProject.Application.Common.Interfaces;

namespace FinalLabProject.Application.Harbours.Commands.UpdateHarbour;

public class UpdateHarbourCommandValidator : AbstractValidator<UpdateHarbourCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateHarbourCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(UpdateHarbourCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.Harbours
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}
