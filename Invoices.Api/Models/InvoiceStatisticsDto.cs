namespace Invoices.Api.Models
{
    /// <summary>
    /// Datový objekt pro obecné statistiky faktur.
    /// Obsahuje souhrnné hodnoty napříč celou databází.
    /// </summary>
    public class InvoiceStatisticsDto
    {
        /// <summary>
        /// Součet cen všech faktur vystavených v aktuálním roce.
        /// </summary>
        public decimal CurrentYearSum { get; set; }

        /// <summary>
        /// Součet cen všech faktur napříč všemi roky.
        /// </summary>
        public decimal AllTimeSum { get; set; }

        /// <summary>
        /// Celkový počet faktur uložených v databázi.
        /// </summary>
        public int InvoicesCount { get; set; }
    }
}
