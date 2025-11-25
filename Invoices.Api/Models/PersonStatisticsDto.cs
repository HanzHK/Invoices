namespace Invoices.Api.Models
{
    /// <summary>
    /// Datový objekt pro statistiky jednotlivých společností/osob.
    /// Obsahuje identifikátor, název a celkové příjmy z faktur.
    /// </summary>
    public class PersonStatisticsDto
    {
        /// <summary>
        /// Primární klíč osoby/společnosti v databázi.
        /// </summary>
        public int PersonId { get; set; }
        /// <summary>
        /// Název společnosti nebo jméno osoby.
        /// </summary>
        public string PersonName { get; set; } = string.Empty;
        /// <summary>
        /// Celkový objem fakturovaných příjmů pro danou osobu/společnost.
        /// </summary>
        public decimal Revenue { get; set; }
    }
}
