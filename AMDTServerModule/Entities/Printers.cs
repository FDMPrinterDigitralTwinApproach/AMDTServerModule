using AMDTServerModule.GenericRepository;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMDTServerModule.Entities
{
    public class Printers : IEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("brand_name")]
        public string BrandName { get; set; } = string.Empty;

        [Column("table_X")]
        public double TableX { get; set; }

        [Column("table_Y")]
        public double TableY { get; set; }
        [Column("guid")]
        public Guid SpesificID { get; set; } 
      
        [Column("farm_id")]
        [ForeignKey(nameof(PrinterFarms))]
        public int TipID { get; set; }

        [Column("aktif")]
        public bool Aktif { get; set; }

        [Column("created-at")]
        public DateTime CreatedAt { get; set; }

        [Column("created_id")]
        public int CreatedID { get; set; }
        public virtual PrinterFarms? PrinterFarms { get; set; }
    }
}
