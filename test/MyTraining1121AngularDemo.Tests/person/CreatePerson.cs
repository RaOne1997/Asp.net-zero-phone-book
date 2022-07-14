
using Abp.Runtime.Validation;
using Acme.PhoneBookDemo.PhoneBook;
using MyTraining1121AngularDemo.Phonebook.dto;
using MyTraining1121AngularDemo.Test.Base;
using MyTraining1121AngularDemo.Tests;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyTraining1121AngularDemo.Tests.person
{
    public class CreatePerson : AppTestBase
    {
        private readonly IPersonAppService _personAppService;

        public CreatePerson()
        {
            _personAppService = Resolve<IPersonAppService>(); ;
        }

        [Fact]
        public async Task Should_Create_Person_With_Valid_Arguments()
        {
            //Act
            await _personAppService.CreatePerson(
                new CreatePersonInput
                {
                    Name = "John",
                    Surname = "Nash",
                    EmailAddress = "john.nash@abeautifulmind.com"
                });

            //Assert
            UsingDbContext(
                context =>
                {
                    var john = context.Persons.FirstOrDefault(p => p.EmailAddress == "john.nash@abeautifulmind.com");
                    john.ShouldNotBe(null);
                    john.Name.ShouldBe("John");
                });
        }

        [Fact]
        public async Task Should_Not_Create_Person_With_Invalid_Arguments()
        {
            //Act and Assert
            await Assert.ThrowsAsync<AbpValidationException>(
                async () =>
                {
                    await _personAppService.CreatePerson(
                new CreatePersonInput
                                {
                                    Name = "John"
                                });
                });
        }

    }
}
