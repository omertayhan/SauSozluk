﻿using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using BlazorSozluk.Infrastructure.Persistence.Context.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations;

public class EmailConfirmationEntityConfiguration : BaseEntityConfiguration<EmailConfirmation>
{
    public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        base.Configure(builder);
        builder.ToTable("emailconfirmation", BlazorSozlukContext.DEFAULT_SCHEMA);
    }
}
