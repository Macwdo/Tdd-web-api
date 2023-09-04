using System.Collections.Generic;
using TddWebApi.Models;

namespace TddWebApi.UnitTests.Fixtures;

public static class UsersFixture
{
    public static List<User> GetTestUsers() => 
        new()
        {
            new User
                {
                    Id = 1,
                    Name = "Test User 1",
                    Email = "Test User 1 Email",
                    Address = new Address
                    {
                        City = "Test User 1 City",
                        Number = 10,
                        Street = "Test User 1 Street",
                    }
                },
            
            new User
                {
                    Id = 2,
                    Name = "Test User 2",
                    Email = "Test User 2 Email",
                    Address = new Address
                    {
                        City = "Test User 2 City",
                        Number = 20,
                        Street = "Test User 2 Street",
                    }
                },
            
            new User
                {
                    Id = 3,
                    Name = "Test User 3",
                    Email = "Test User 3 Email",
                    Address = new Address
                    {
                        City = "Test User 3 City",
                        Number = 30,
                        Street = "Test User 3 Street",
                    }
                }
            
        };
}