using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Definuje smlouvu pro správu faktur (Invoice).
    /// Rozhraní zajišťuje metody pro čtení, vytváření, mazání a filtrování faktur.
    /// Implementace obsahuje samotnou byznys logiku.
    /// </summary>
    public interface IInvoiceManager
    {
        /// <summary>
        /// Vrátí seznam všech faktur uložených v systému.
        /// </summary>
        /// <returns>Kolekce <see cref="InvoiceGetDto"/> reprezentující všechny faktury.</returns>
        IEnumerable<InvoiceGetDto> GetAllInvoices();

        /// <summary>
        /// Vrátí detail faktury podle jejího jedinečného identifikátoru.
        /// </summary>
        /// <param name="id">Jedinečné ID faktury, kterou chceme získat.</param>
        /// <returns>
        /// Objekt <see cref="InvoiceGetDto"/> reprezentující fakturu s daným ID,
        /// nebo <see langword="null"/>, pokud faktura s tímto ID neexistuje.
        /// </returns>
        InvoiceGetDto GetInvoiceById(int id);

        /// <summary>
        /// Vytvoří novou fakturu na základě dodaného datového objektu.
        /// </summary>
        /// <param name="dto">Objekt <see cref="InvoicePostDto"/> s daty nové faktury.</param>
        /// <returns>Nově vytvořená faktura ve formě <see cref="InvoicePostDto"/>.</returns>
        InvoiceGetDto AddInvoice(InvoicePostDto dto);

        /// <summary>
        /// Smaže fakturu podle jejího ID.
        /// </summary>
        /// <param name="id">Jedinečné ID faktury, která má být odstraněna.</param>
        /// <returns>
        /// <c>true</c>, pokud byla faktura úspěšně smazána,
        /// nebo <c>false</c>, pokud faktura s daným ID neexistuje.
        /// </returns>
        bool DeleteInvoice(int id);

        /// <summary>
        /// Vyhledá a vrátí faktury podle zadaného filtru.
        /// </summary>
        /// <param name="criteria">Řetězec určující kritéria filtrování (např. číslo faktury, datum, kupující).</param>
        /// <returns>Kolekce <see cref="InvoiceGetDto"/> odpovídající zadanému filtru.</returns>
        IEnumerable<InvoiceGetDto> FilterInvoices(string criteria);

        /// <summary>
        /// Aktualizuje existující fakturu na základě dodaného datového objektu.
        /// </summary>
        /// <param name="dto">Objekt <see cref="InvoiceGetDto"/> s upravenými daty faktury.</param>
        /// <returns>Aktualizovaná faktura ve formě <see cref="InvoiceGetDto"/>.</returns>
        InvoiceGetDto UpdateInvoice(InvoicePostDto dto);

        /// <summary>
        /// Vrátí seznam faktur podle ID dodavatele (Seller).
        /// </summary>
        /// <param name="sellerId">Jedinečné ID dodavatele, jehož faktury chceme získat.</param>
        /// <returns>Kolekce <see cref="InvoiceGetDto"/> odpovídající zadanému dodavateli.</returns>
        IEnumerable<InvoiceGetDto> GetInvoicesBySeller(int sellerId);

        /// <summary>
        /// Vrátí seznam faktur vystavených firmou s daným IČO.
        /// </summary>
        /// <param name="ico">Identifikační číslo organizace (IČO), jejíž vystavené faktury chceme získat.</param>
        /// <returns>Kolekce <see cref="InvoiceGetDto"/> odpovídající vystaveným fakturám.</returns>
        IEnumerable<InvoiceGetDto> GetIssuedInvoicesByIco(string ico);

        /// <summary>
        /// Vrátí seznam faktur přijatých firmou s daným IČO.
        /// </summary>
        /// <param name="ico">Identifikační číslo organizace (IČO), jejíž přijaté faktury chceme získat.</param>
        /// <returns>Kolekce <see cref="InvoiceGetDto"/> odpovídající přijatým fakturám.</returns>
        IEnumerable<InvoiceGetDto> GetReceivedInvoicesByIco(string ico);



    }
}

