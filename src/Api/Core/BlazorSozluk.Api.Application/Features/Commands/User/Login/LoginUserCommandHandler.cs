﻿using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Login;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress) ?? throw new DatabaseValidationExceptions("User not found");

        var pass = PasswordEncryptor.Encrypt(request.Password);

        if (dbUser.Password != pass)
        {
            throw new DatabaseValidationExceptions("Password is wrong!");
        }

        if (!dbUser.EmailConfirmed)
        {
            throw new DatabaseValidationExceptions("EmailAddress is not confirmed yet!");
        }

        var result = mapper.Map<LoginUserViewModel>(dbUser);
        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new(ClaimTypes.Email, dbUser.EmailAddress),
            new(ClaimTypes.Name, dbUser.UserName),
            new(ClaimTypes.GivenName, dbUser.FirstName),
            new(ClaimTypes.Surname, dbUser.LastName)
        };
        result.Token = GenerateToken(claims);

        return result;
    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(10);

        var token = new JwtSecurityToken(claims: claims,
                                         expires: expiry,
                                         signingCredentials: creds,
                                         notBefore: DateTime.Now);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
