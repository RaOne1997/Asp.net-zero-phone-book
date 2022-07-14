using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyTraining1121AngularDemo.Phonebook.dto;
using System.Threading.Tasks;

namespace Acme.PhoneBookDemo.PhoneBook
{
    public interface IPersonAppService : IApplicationService
    {
        ListResultDto<PersonListDto> GetPeople(GetPeopleInput input);
        Task CreatePerson(CreatePersonInput input);
        Task Edit(EditPersoneInput input);
    }
}