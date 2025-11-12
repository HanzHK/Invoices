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
using Invoices.Data.Entities;

namespace Invoices.Data.Interfaces
{
    /// <summary>
    /// Specializované rozhraní pro repozitář pracující s entitou <see cref="Person"/>.
    /// Rozšiřuje obecné metody z <see cref="IRepository{T}"/> o specifické funkce pro osoby.
    /// </summary>
    public interface IPersonRepository : IBaseRepository<Person>
    {
        /// <summary>
        /// Vrátí všechny osoby podle hodnoty příznaku <see cref="Person.Hidden"/>.
        /// Slouží pro práci se skrytými (soft-deleted) záznamy.
        /// </summary>
        /// <param name="hidden">Hodnota příznaku Hidden (true = skryté osoby, false = viditelné).</param>
        /// <returns>Kolekce osob odpovídajících podmínce.</returns>
        IEnumerable<Person> GetByHidden(bool hidden);
    }
}
