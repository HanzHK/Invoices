/*  _____ _______         _                      _
 * |_   _|__   __|       | |                    | |
 *   | |    | |_ __   ___| |___      _____  _ __| | __  ___ ____
 *   | |    | | '_ \ / _ \ __\ \ /\ / / _ \| '__| |/ / / __|_  /
 *  _| |_   | | | | |  __/ |_ \ V  V / (_) | |  |   < | (__ / /
 * |_____|  |_|_| |_|\___|\__| \_/\_/ \___/|_|  |_|\_(_)___/___|
 *
 *                      ___ ___ ___
 *                     | . |  _| . |  LICENCE
 *                     |  _|_| |___|
 *                     |_|
 *
 *    REKVALIFIKAČNÍ KURZY  <>  PROGRAMOVÁNÍ  <>  IT KARIÉRA
 *
 * Tento zdrojový kód je součástí profesionálních IT kurzů na
 * WWW.ITNETWORK.CZ
 *
 * Kód spadá pod licenci PRO obsahu a vznikl díky podpoře
 * našich členů. Je určen pouze pro osobní užití a nesmí být šířen.
 * Více informací na http://www.itnetwork.cz/licence
 */
using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Interfaces;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Manager vrstva obsahuje aplikační logiku spojenou s entitou Person.
    /// Odděluje controller od přístupu k datům a umožňuje snadnější testování a údržbu.
    /// </summary>
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;

        public PersonManager(IPersonRepository personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public IEnumerable<PersonDto> GetAllPersons()
        {
            // Vrátíme pouze aktivní (neskryté) osoby
            IEnumerable<Person> people = personRepository.GetByHidden(false);
            return mapper.Map<IEnumerable<PersonDto>>(people);
        }

        public PersonDto AddPerson(PersonDto dto)
        {
            Person person = mapper.Map<Person>(dto);
            Person addedPerson = personRepository.Add(person);
            personRepository.SaveChanges();
            return mapper.Map<PersonDto>(addedPerson);
        }

        public bool DeletePerson(int id)
        {
            // Soft delete: nastavíme příznak Hidden = true místo fyzického smazání
            if (HidePerson(id) is not null)
            {
                personRepository.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Označí osobu jako skrytou (soft delete).
        /// Pokud osoba neexistuje, vrací null.
        /// </summary>
        private Person? HidePerson(int personId)
        {
            Person? person = personRepository.FindById(personId);

            if (person is null)
                return null;

            person.Hidden = true;
            return personRepository.Update(person);
        }
        // TODO: Ořidat dokumentaci
        /// <summary>
        ///     
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PersonDto? GetPersonById(int id)
        {
            Person? person = personRepository.FindById(id);
            if (person is null)
                return null;

            PersonDto personDto = mapper.Map<PersonDto>(person);
            return personDto;
        }
    }
}
