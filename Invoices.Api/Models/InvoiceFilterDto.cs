namespace Invoices.Api.Models
{
    /// <summary>
    /// Datový objekt pro filtrování faktur při dotazech na API.
    /// Obsahuje volitelné parametry, podle kterých se výsledky omezí.
    /// </summary>
    public class InvoiceFilterDto
    {
        /// <summary>
        /// Identifikátor odběratele (Buyer).
        /// Pokud je zadán, vrátí se pouze faktury pro daného odběratele.
        /// </summary>
        public int? BuyerId { get; set; }

        /// <summary>
        /// Identifikátor dodavatele (Seller).
        /// Pokud je zadán, vrátí se pouze faktury pro daného dodavatele.
        /// </summary>
        public int? SellerId { get; set; }

        /// <summary>
        /// Název produktu nebo služby.
        /// Pokud je zadán, vrátí se faktury obsahující daný produkt.
        /// </summary>
        public string? Product { get; set; }

        /// <summary>
        /// Minimální cena faktury.
        /// Vrátí faktury s cenou vyšší nebo rovnou této hodnotě.
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Maximální cena faktury.
        /// Vrátí faktury s cenou nižší nebo rovnou této hodnotě.
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Maximální počet vrácených faktur.
        /// Pokud je zadán, výsledky se omezí na daný počet.
        /// </summary>
        public int? Limit { get; set; }
    }

}
