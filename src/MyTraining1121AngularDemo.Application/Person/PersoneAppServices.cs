using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Acme.PhoneBookDemo.PhoneBook;
using MyTraining1121AngularDemo;
using MyTraining1121AngularDemo.Phonebook.dto;
using MyTraining1121AngularDemo.PhoneBook.Acme.PhoneBookDemo.PhoneBook;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PersonAppService : MyTraining1121AngularDemoAppServiceBase, IPersonAppService
{
    private readonly IRepository<Person> _personRepository;

    public PersonAppService(IRepository<Person> personRepository)
    {
        _personRepository = personRepository;
    }

    public ListResultDto<PersonListDto> GetPeople(GetPeopleInput input)
    {
        var people = _personRepository
            .GetAll()
            .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.Name.Contains(input.Filter) ||
                     p.Surname.Contains(input.Filter) ||
                     p.EmailAddress.Contains(input.Filter)
            )
            .OrderBy(p => p.Name)
            .ThenBy(p => p.Surname)
            .ToList();

        return new ListResultDto<PersonListDto>(ObjectMapper.Map<List<PersonListDto>>(people));
    }

    public async Task CreatePerson(CreatePersonInput input)
    {
        var person = ObjectMapper.Map<Person>(input);
        await _personRepository.InsertAsync(person);
    }

    public async Task Edit(EditPersoneInput input)
    {
        var person = ObjectMapper.Map<Person>(input);
         
        await _personRepository.UpdateAsync(person);
        
    }
}
