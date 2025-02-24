using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Application.Tours.Commands.RemovePackage
{
    public record RemovePackageCommand : IRequest<Result>
    {
        public Guid PackageId { get; init; }
    }

    public class RemovePackageCommandHandler : IRequestHandler<RemovePackageCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public RemovePackageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RemovePackageCommand request, CancellationToken cancellationToken)
        {
            var package = await _context.Packages
                .FirstOrDefaultAsync(p => p.Id == request.PackageId, cancellationToken);

            if (package == null)
                return Result.Failure<Unit>(ErrorCodes.ResourceNotFound);

            _context.Packages.Remove(package);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
