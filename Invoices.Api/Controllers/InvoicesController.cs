using Azure.Identity;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    /// <summary>
    /// API kontroler pro práci s fakturami (Invoice).
    /// Přijímá HTTP požadavky a deleguje logiku do <see cref="IInvoiceManager"/>.
    /// Controller neřeší byznys logiku, pouze zpracovává vstup a vrací odpovědi.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceManager invoiceManager;

        /// <summary>
        /// Vytváří novou instanci kontroleru a nastavuje závislost na <see cref="IInvoiceManager"/>.
        /// </summary>
        /// <param name="invoiceManager">Manager, který zajišťuje veškerou logiku práce s fakturami.</param>
        public InvoicesController(IInvoiceManager invoiceManager)
        {
            this.invoiceManager = invoiceManager;
        }
        /// <summary>
        /// Vrátí detail faktury podle ID.
        /// </summary>
        /// <param name="id">ID faktury kterou chceme zobrazit (detail)</param>
        [HttpGet("{id}")]
        public ActionResult<InvoiceGetDto> GetInvoiceById(int id)
        {
            InvoiceGetDto? invoiceDto = invoiceManager.GetInvoiceById(id);
            if (invoiceDto == null)
                return NotFound();
            return Ok(invoiceDto);
        }

        /// <summary>
        /// Vytvoří novou fakturu.
        /// </summary>
        /// <param name="dto">Data nové faktury</param>
        /// <returns> Vrátí 201 (Created) s nově vytvořenou fakturou.</returns>
        [HttpPost]
        public ActionResult<InvoiceGetDto> AddInvoice([FromBody] InvoicePostDto dto)
        {
            InvoiceGetDto createdInvoice = invoiceManager.AddInvoice(dto);
            return Created(string.Empty, createdInvoice);
        }

        /// <summary>
        /// Aktualizuje existující fakturu.
        /// </summary>
        /// <param name="dto">Nová aktualizovaná data faktury</param>
        /// <returns>Vrátí 200 (OK) s aktualizovanou fakturou nebo 404 pokud neexistuje.</returns>
        [HttpPut]
        public ActionResult<InvoiceGetDto> UpdateInvoice([FromBody] InvoicePostDto dto)
        {
            var updatedInvoice = invoiceManager.UpdateInvoice(dto);
            if (updatedInvoice == null)
                return NotFound();

            return Ok(updatedInvoice);
        }

        /// <summary>
        /// Smaže fakturu podle ID (hard delete).
        /// </summary>
        /// <param name="id">ID gaktury ke smazání</param>
        /// <returns>
        /// HTTP 204 (No Content), pokud byla faktura úspěšně smazána,  
        /// nebo HTTP 404 (Not Found), pokud faktura s daným ID neexistuje.
        /// </returns> 
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            bool success = invoiceManager.DeleteInvoice(id);

            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Vrátí seznam faktur podle zadaných query parametrů.
        /// </summary>
        /// <param name="filter">
        /// Query parametry jako buyerId, sellerId, product, minPrice, maxPrice, limit.
        /// </param>
        /// <returns>
        /// Vrátí 200 (OK) s kolekcí <see cref="InvoiceGetDto"/> odpovídající zadanému filtru
        /// </returns>
        [HttpGet]
        public ActionResult<IEnumerable<InvoiceGetDto>> GetInvoices([FromQuery] InvoiceFilterDto filter)
        {
            var invoices = invoiceManager.FilterInvoices(filter);
            return Ok(invoices);
        }

        /// <summary>
        /// Vrátí seznam faktur, které byly vystaveny firmou s daným IČO.
        /// </summary>
        /// <param name="ico">
        /// Identifikační číslo organizace (IČO), podle kterého se vyhledají vystavené faktury.
        /// </param>
        /// <returns>
        /// Vrátí 200 (OK) s kolekcí <see cref="InvoiceGetDto"/> odpovídající vystaveným fakturám.
        /// </returns>
        [HttpGet("issued")]
        public ActionResult<IEnumerable<InvoiceGetDto>> GetIssuedInvoices([FromQuery] string ico)
        {
            var invoices = invoiceManager.GetIssuedInvoicesByIco(ico);
            return Ok(invoices);
        }

        /// <summary>
        /// Vrátí seznam faktur, které byly přijaty firmou s daným IČO.
        /// </summary>
        /// <param name="ico">
        /// Identifikační číslo organizace (IČO), podle kterého se vyhledají přijaté faktury.
        /// </param>
        /// <returns>
        /// Vrátí 200 (OK) s kolekcí <see cref="InvoiceGetDto"/> odpovídající přijatým fakturám.
        /// </returns>
        [HttpGet("received")]
        public ActionResult<IEnumerable<InvoiceGetDto>> GetReceivedInvoices([FromQuery] string ico)
        {
            var invoices = invoiceManager.GetReceivedInvoicesByIco(ico);
            return Ok(invoices);
        }

        /// <summary>
        /// Vrátí souhrnné statistiky pro všechny faktury v databázi.
        /// </summary>
        /// <returns>
        /// HTTP 200 (OK) s objektem <see cref="InvoiceStatisticsDto"/>, který obsahuje:
        /// - součet cen všech faktur vystavených v aktuálním roce (CurrentYearSum),
        /// - součet cen všech faktur napříč všemi roky (AllTimeSum),
        /// - celkový počet faktur v databázi (InvoicesCount).
        /// </returns>
        [HttpGet("statistics")]
        public ActionResult<InvoiceStatisticsDto> GetInvoiceStatistics([FromServices] IStatisticsManager statisticsManager)
        {
            var stats = statisticsManager.GetInvoiceStatistics();
            return Ok(stats);
        }



    }
}
