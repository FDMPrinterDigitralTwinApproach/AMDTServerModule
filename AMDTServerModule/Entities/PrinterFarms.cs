using AMDTServerModule.GenericRepository;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMDTServerModule.Entities
{
    public class PrinterFarms : IEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("Location")]
        public string Location { get; set; } = string.Empty;
        [Column("aktif")]
        public bool Aktif { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Printers>? Printers { get; set; }
    }
}
