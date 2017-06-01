using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyNote.Enties;

namespace MyNote.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            NoteUser admin = new NoteUser()
            {
                Name = "Nuh",
                Surname = "Çolakkadıoğlu",
                Email = "n.colakkadioglu@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "nck",
                Password = "1s1s",
                CreatedOn = DateTime.Now,
                Modified = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "nck"
            };

            NoteUser user = new NoteUser()
            {
                Name = "Kullanıcı",
                Surname = "Normal",
                Email = "user@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "kullanici",
                Password = "2s2s",
                CreatedOn = DateTime.Now,
                Modified = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "nck"
            };

            context.NoteUsers.Add(admin);
            context.NoteUsers.Add(user);

            for (int n = 0; n < 8; n++)
            {
                NoteUser fakeUser = new NoteUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username =$"user{n}",
                    Password = "2s2s",
                    CreatedOn = DateTime.Now,
                    Modified = DateTime.Now.AddMinutes(5),
                    ModifiedUsername = "nck"
                };

                context.NoteUsers.Add(fakeUser);
            }

            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    Modified = DateTime.Now,
                    ModifiedUsername = "nck",
                    CreatedOn = DateTime.Now
                };
                context.Categorys.Add(cat);

                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(10, 50),
                        Owner = (k % 2 == 0) ? admin : user,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(),
                        Modified = FakeData.DateTimeData.GetDatetime(),
                        ModifiedUsername= (k % 2 == 0) ? admin.Username : user.Username
                        
                    };
                    cat.Notes.Add(note);

                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                           
                            Owner = (j % 2 == 0) ? admin : user,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(),
                            Modified = FakeData.DateTimeData.GetDatetime(),
                            ModifiedUsername = (j % 2 == 0) ? admin.Username : user.Username
                        };
                        note.Comments.Add(comment);
                    }

                    //for (int d = 0; d < FakeData.NumberData.GetNumber(1, 5); d++)
                    //{
                    //    Liked liked = new Liked()
                    //    {
                            
                    //    };
                    //}
                }
            }
            context.SaveChanges();
        }
    }
}
