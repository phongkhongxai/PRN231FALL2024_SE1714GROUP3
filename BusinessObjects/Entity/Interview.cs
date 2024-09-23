using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entity
{
    public class Interview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ApplicationId { get; set; }
        public long ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public string? Status { get; set; }
        public string? Result { get; set; }
        public bool IsDelete { get; set; } = false;
        public long? Round { get; set; }

        public Application Application { get; set; }
        public Schedule Schedule { get; set; }
    }
}
