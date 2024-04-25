using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.ChangePassword;
public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue)
        {
            throw new ArgumentNullException(nameof(request.UserId));
        }

        var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value) ?? throw new DatabaseValidationExceptions("User not found!");
        var encPassword = PasswordEncryptor.Encrypt(request.OldPassword);

        if (dbUser.Password != encPassword)
        {
            throw new DatabaseValidationExceptions("Old password wrong!");
        }

        dbUser.Password = encPassword;

        await _userRepository.UpdateAsync(dbUser);

        return true;
    }
}
