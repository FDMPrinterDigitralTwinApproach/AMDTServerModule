using AMDTServerModule.GenericRepository;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMDTServerModule.Entities
{
    public class Users : IEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [Column("lastName")]
        public string LastName { get; set; } = string.Empty;
        [Column("username")]
        public string Username { get; set; } = string.Empty;
        [Column("userPassword")]
        public string UserPassword { get; set; } = string.Empty;
        [Column("aktif")]
        public bool Aktif { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

    }
}
