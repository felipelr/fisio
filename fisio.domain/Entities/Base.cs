using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fisio.domain.Entities
{
    public class Base
    {
        [Key]
        [Column("id")]
        public string Id { get; protected set; } = Guid.NewGuid().ToString();
    }
}
