using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Infrastructure;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlazorSozluk.Infrastructure.Persistence.Context;

internal class SeedData //db tablolarımızı bogus ile sahte data ile beslediğimiz alan 
{
    private static List<User> GetUsers() //kullanıcılar için olan fake data
    {
        var result = new Faker<User>("tr")
            .RuleFor(i => i.Id, i=> Guid.NewGuid())
            .RuleFor(i => i.CreatedDate, 
                i => i.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now))
            .RuleFor(i => i.FirstName, i=>i.Person.FirstName)
            .RuleFor(i => i.LastName, i=>i.Person.LastName)
            .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
            .RuleFor(i => i.UserName, i=>i.Internet.UserName())
            .RuleFor(i => i.Password, i=> PasswordEncryptor.Encrypt(i.Internet.UserName()))
            .RuleFor(i => i.EmailConfirmed, i=>i.PickRandom(true,false))
            .Generate(500);

        return result;
    }
    public async Task SeedAsync(IConfiguration config)
    {
        var builder = new DbContextOptionsBuilder();
        builder.UseSqlServer(config["BlazorSozlukDbConnectionString"]);

        var context = new BlazorSozlukContext(builder.Options);

        if (context.Users.Any()) // registrationda eklediğimiz dataseed isteğine koşul koyfuk tekrar tekrar çalışmasın diye
        {
            await Task.CompletedTask;
            return;
        }


        var users = GetUsers();
        var userIds = users.Select(i => i.Id); // users tablosundaki idleri entrieslerde kullanmak için tanımladık

        await context.Users.AddRangeAsync(users); //contextimize ekledik kullanıcıları


        var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList(); // aşağıda içeride tanımladığımızda erişim zor olacagı için buraya ekledik id parametresini
        int counter = 0; //entries sırayla alabilmek için yukardaki listeden değişken oluşturduk

        var entries = new Faker<Entry>("tr")
            .RuleFor(i => i.Id, i => guids[counter++])
            .RuleFor(i => i.CreatedDate,
                i => i.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now))
            .RuleFor(i => i.Subject, i => i.Lorem.Sentence(5, 5))
            .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
            .RuleFor(i => i.CreateById, i => i.PickRandom(userIds))
            .Generate(150);

        await context.entries.AddRangeAsync(entries);

        var comments = new Faker<EntryComment>("tr")
            .RuleFor(i => i.Id, i=> Guid.NewGuid())
            .RuleFor(i => i.CreatedDate,
                i => i.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now))
            .RuleFor(i =>i.Content, i => i.Lorem.Paragraph(2))
            .RuleFor(i => i.CreateById, i=>i.PickRandom(userIds)) //kim tarafından oluştudugumuzu userIds den çektik
            .RuleFor(i => i.EntryId, i =>i.PickRandom(guids)) //bağlı oldukları entry idleri yukarıdan çektik 
            .Generate(1000);

        await context.entryComments.AddRangeAsync(comments);

        await context.SaveChangesAsync(); // yukarıdan sırayla işlemlerimizi gerçekleştirecek

    }
}
