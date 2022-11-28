using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace KladrApi
{


    [Table("KLADR")]
    public class KLADR
    {

        public long Id { get; set; }
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string Socr { get; set; }

        [JsonIgnore]
        public int list { get; set; }

        [JsonIgnore]
        public int? priority { get; set; }


        public int? okcm { get; set; }

        [JsonIgnore]
        [Column("id_parent")]
        public long? ParentId { get; set; }
        public KLADR Parent { get; set; }


        public string FullName
        {
            get
            {
                if (list == 1)
                {
                    if (Socr == "Респ")
                    {
                        if (Name == "Кабардино-Балкарская" || Name == "Карачаево-Черкесская" || Name == "Удмуртская" || Name == "Чувашская" || Name == "Чеченская")
                            return Name + " республика";
                        else
                            return "Республика " + Name;
                    }
                    else if (Socr == "обл")
                        return Name + " область";
                }

                return Name + " " + Socr;
            }

        }


    }

}