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
        /// Vrátí seznam všech faktur v systému.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<InvoiceGetDto>> GetAllInvoices()
        {
            var invoices = invoiceManager.GetAllInvoices();
            return Ok(invoices);
        }


        /// <summary>
        /// Vrátí detail faktury podle ID.
        /// </summary>
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
        [HttpPost]
        public ActionResult<InvoiceGetDto> AddInvoice([FromBody] InvoicePostDto dto)
        {
            InvoiceGetDto createdInvoice = invoiceManager.AddInvoice(dto);
            return Created(string.Empty, createdInvoice);
        }

        /// <summary>
        /// Aktualizuje existující fakturu.
        /// </summary>
        [HttpPut]
        public ActionResult<InvoiceGetDto> UpdateInvoice([FromBody] InvoicePostDto dto)
        {
            InvoiceGetDto updatedInvoice = invoiceManager.UpdateInvoice(dto);
            return Ok(updatedInvoice);
        }

        /// <summary>
        /// Smaže fakturu podle ID (hard delete).
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            bool success = invoiceManager.DeleteInvoice(id);

            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Vrátí seznam faktur podle kritéria.
        /// </summary>
        [HttpGet("filter/{criteria}")]
        public ActionResult<IEnumerable<InvoiceGetDto>> FilterInvoices(string criteria)
        {
            IEnumerable<InvoiceGetDto> invoices = invoiceManager.FilterInvoices(criteria);
            return Ok(invoices);
        }
    }
}
