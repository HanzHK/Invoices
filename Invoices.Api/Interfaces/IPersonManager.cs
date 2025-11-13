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
using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Definuje smlouvu pro správu osob (Person).
    /// Rozhraní zajišťuje metody pro čtení, vytváření a mazání osob.
    /// Implementace obsahuje samotnou byznys logiku.
    /// </summary>
    public interface IPersonManager
    {
        /// <summary>
        /// Vrátí seznam všech osob uložených v systému.
        /// </summary>
        /// <returns>Kolekce <see cref="PersonDto"/> reprezentující všechny osoby.</returns>
        IEnumerable<PersonDto> GetAllPersons();

        /// <summary>
        /// Vytvoří novou osobu na základě dodaného datového objektu.
        /// </summary>
        /// <param name="dto">Objekt <see cref="PersonDto"/> s daty nové osoby.</param>
        /// <returns>Nově vytvořená osoba ve formě <see cref="PersonDto"/>.</returns>
        PersonDto AddPerson(PersonDto dto);

        /// <summary>
        /// Vrátí osobu podle jejího jedinečného identifikátoru.
        /// </summary>
        /// <param name="id">Jedinečné ID osoby, kterou chceme získat.</param>
        /// <returns>
        /// Objekt <see cref="PersonDto"/> reprezentující osobu s daným ID,
        /// nebo <see langword="null"/>, pokud osoba s tímto ID neexistuje.
        /// </returns>
        PersonDto GetPersonById(int id);

        /// <summary>
        /// Smaže osobu podle jejího ID.
        /// </summary>
        /// <param name="id">Jedinečné ID osoby, která má být odstraněna.</param>
        /// <returns>
        /// <c>true</c>, pokud byla osoba úspěšně smazána, 
        /// nebo <c>false</c>, pokud osoba s daným ID neexistuje.
        /// </returns>
        bool DeletePerson(int id);
    }
}
