using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entity
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public bool IsDelete { get; set; } = false;
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
