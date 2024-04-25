using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail;
public class ConfirmEmailCommand : IRequest<bool>
{
    public Guid ConfirmationId { get; set; }
}

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailConfirmationRepository _emailConfirmationRepository;

    public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
    {
        _userRepository = userRepository;
        _emailConfirmationRepository = emailConfirmationRepository;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var confirmation = await _emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

        if (confirmation == null)
        {
            throw new DatabaseValidationExceptions("Confirmation Not Found!");
        }

        var dbUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == confirmation.NewEmailAddress);

        if (dbUser == null)
        {
            throw new DatabaseValidationExceptions("User not found with this!");
        }

        if (dbUser.EmailConfirmed)
        {
            throw new DatabaseValidationExceptions("Email Address already confirmed!");
        }
        dbUser.EmailConfirmed = true;
        await _userRepository.UpdateAsync(dbUser);

        return true;
    }
}
