using System.ComponentModel.DataAnnotations.Schema;

namespace KladrApi
{


    [Table("KLADR")]
    public class KLADR
    {

        public string Id { get; set; }
        
        public string Name { get; set; }

        [Column("id_parent")]
        public string? ParentId { get; set; }


     


    }

}