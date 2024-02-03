using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAppWebApi.Models
{
    [Table("stocks")]
	public class Stock
	{
        [Key]
        [Column("stock_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }

        [Column("symbol")]
        [Required(ErrorMessage = "Symbol is required.")]
        [StringLength(10, ErrorMessage = "Symbol must not exceed 10 characters.")]
        public string? Symbol { get; set; } 

        [Column("company_name")]
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(255, ErrorMessage = "Company name must not exceed 255 characters.")]
        public string? CompanyName { get; set; } 

        [Column("market_cap")]
        [Range(0, double.MaxValue, ErrorMessage = "Market cap must be a positive number.")]
        public decimal? MarketCap { get; set; }

        [Column("sector")]
        [StringLength(200, ErrorMessage = "Sector must not exceed 200 characters.")]
        public string? Sector { get; set; }

        [Column("industry")]
        [StringLength(200, ErrorMessage = "Industry must not exceed 200 characters.")]
        public string? Industry { get; set; } 

        [Column("sector_en")]
        [StringLength(200, ErrorMessage = "Sector (English) must not exceed 200 characters.")]
        public string? SectorEn { get; set; } 

        [Column("industry_en")]
        [StringLength(200, ErrorMessage = "Industry (English) must not exceed 200 characters.")]
        public string? IndustryEn { get; set; }

        [Column("stock_type")]
        [StringLength(50, ErrorMessage = "Stock type must not exceed 50 characters.")]
        public string? StockType { get; set; } 

        [Column("rank")]
        [Range(1, int.MaxValue, ErrorMessage = "Rank must be a positive number.")]
        public int? Rank { get; set; }

        [Column("rank_source")]
        [StringLength(200, ErrorMessage = "Rank source must not exceed 200 characters.")]
        public string? RankSource { get; set; }

        [Column("reason")]
        [StringLength(255, ErrorMessage = "Reason must not exceed 255 characters.")]
        public string? Reason { get; set; } 

        public ICollection<WatchList>? WatchLists { get; set; }
        public Stock()
		{
		}
	}
}

