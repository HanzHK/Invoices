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
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    /// <summary>
    /// API kontroler pro práci s osobami (Person).
    /// Přijímá HTTP požadavky a deleguje logiku do <see cref="IPersonManager"/>.
    /// Controller neřeší byznys logiku, pouze zpracovává vstup a vrací odpovědi.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonManager personManager;

        /// <summary>
        /// Vytváří novou instanci kontroleru a nastavuje závislost na <see cref="IPersonManager"/>.
        /// </summary>
        /// <param name="personManager">Manager, který zajišťuje veškerou logiku práce s osobami.</param>
        public PersonsController(IPersonManager personManager)
        {
            this.personManager = personManager;
        }

        /// <summary>
        /// Vrátí seznam všech osob v systému.
        /// </summary>
        /// <returns>
        /// HTTP 200 (OK) s kolekcí <see cref="PersonDto"/> reprezentující všechny osoby.
        /// </returns>
        [HttpGet]
        public ActionResult<IEnumerable<PersonDto>> GetAllPersons()
        {
            IEnumerable<PersonDto> people = personManager.GetAllPersons();
            return Ok(people);
        }

        /// <summary>
        /// Vytvoří novou osobu podle dodaného DTO.
        /// </summary>
        /// <param name="dto">Data nové osoby.</param>
        /// <returns>
        /// HTTP 201 (Created) s reprezentací nově vytvořené osoby.
        /// </returns>
        [HttpPost]
        public ActionResult<PersonDto> AddPerson([FromBody] PersonDto dto)
        {
            PersonDto createdPerson = personManager.AddPerson(dto);

            // V budoucnu: až bude implementován detail (GET /api/persons/{id}),
            // lze vracet odkaz na nově vytvořený záznam v location hlavičce:
            // return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

            return Created(string.Empty, createdPerson);
        }
        /// <summary>
        /// Nahradí osobu: původní skryje (Hidden = true) a vytvoří novou s upravenými hodnotami.
        /// </summary>
        /// <param name="id">ID původní osoby, která má být nahrazena.</param>
        /// <param name="dto">Nová data osoby zaslaná klientem v JSONu.</param>
        /// <returns>Nově vytvořená osoba s novým ID.</returns>
        [HttpPut("{id}")]
        public ActionResult<PersonDto> ReplacePerson(int id, [FromBody] PersonDto dto)
        {
            var replaced = personManager.ReplacePerson(id, dto);
            return Ok(replaced);
        }


        /// <summary>
        /// Smaže osobu podle jejího ID.
        /// </summary>
        /// <param name="id">ID osoby ke smazání.</param>
        /// <returns>
        /// HTTP 204 (NoContent), pokud byla osoba úspěšně odstraněna,
        /// nebo HTTP 404 (NotFound), pokud osoba neexistuje.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            bool success = personManager.DeletePerson(id);

            if (!success)
                return NotFound(); // HTTP 404 – pokud osoba neexistuje

            return NoContent(); // HTTP 204 – smazáno, server nic dále nevrací
        }
        /// <summary>
        /// Vrátí detail osoby podle jejího ID.
        /// </summary>
        /// <param name="id">
        /// ID osoby, kterou chceme zobrazit (detail).
        /// </param>
        /// <returns>
        /// HTTP 200 (OK) s objektem <see cref="PersonDto"/>, pokud osoba s daným ID existuje.  
        /// HTTP 404 (NotFound), pokud osoba s daným ID nebyla nalezena.
        /// </returns>
        [HttpGet("{id}")]
        public ActionResult<PersonDto> GetPersonById(int id)
        {
            PersonDto? personDto = personManager.GetPersonById(id);
            if(personDto == null)
                return NotFound();
            return Ok(personDto);
        }
        /// <summary>
        /// Vrátí statistiky fakturovaných příjmů pro jednotlivé osoby/společnosti.
        /// </summary>
        /// <param name="statisticsManager">
        /// Služba, která zajišťuje výpočet statistik (vložená přes dependency injection).
        /// </param>
        /// <param name="personId">
        /// Nepovinný query parametr – pokud je zadán, vrátí se jen statistika pro danou osobu.
        /// </param>
        /// <returns>
        /// HTTP 200 (OK) s kolekcí <see cref="PersonStatisticsDto"/>, kde každý záznam obsahuje:
        /// - identifikátor osoby v databázi (PersonId),
        /// - název nebo jméno osoby/společnosti (PersonName),
        /// - celkový fakturovaný příjem (Revenue).
        /// </returns>
        [HttpGet("statistics")]
        public ActionResult<IEnumerable<PersonStatisticsDto>> GetPersonStatistics(
            [FromServices] IStatisticsManager statisticsManager,
            [FromQuery] int? personId
        )
        {
            var stats = statisticsManager.GetPersonStatistics();

            if (personId.HasValue)
            {
                stats = stats.Where(s => s.PersonId == personId.Value);
            }

            return Ok(stats);
        }



    }
}
