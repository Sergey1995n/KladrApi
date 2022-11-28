using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace KladrApi
{

    [Table("KLADR_ST")]
    public class KLADR_ST
    {

        [Key]
        [JsonIgnore]
        public int kod { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Socr { get; set; }
        public int? Ind { get; set; }

        [JsonIgnore]
        public long? id_parent { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + Socr;
            }

        }


    }

}