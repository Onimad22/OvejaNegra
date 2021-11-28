using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data.Entities
{
    public class Caja
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset Fecha { get; set; }

        [Display(Name = "$0,10")]
        public int  diezc  { get; set; }

        [Display(Name = "$0,20")]
        public int veintec { get; set; }

        [Display(Name = "$0,50")]
        public int cincuentac { get; set; }

        [Display(Name = "$1")]
        public int unb { get; set; }

        [Display(Name = "$2")]
        public int dosb { get; set; }

        [Display(Name = "$5")]
        public int cincob { get; set; }

        [Display(Name = "$10")]
        public int diezb { get; set; }

        [Display(Name = "$20")]
        public int veinteb { get; set; }

        [Display(Name = "$50")]
        public int cincuentab { get; set; }

        [Display(Name = "$100")]
        public int cienb { get; set; }

        [Display(Name = "$200")]
        public int doscientosb { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }
    }
}
