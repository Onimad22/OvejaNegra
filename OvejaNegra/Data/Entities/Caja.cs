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

        [Display(Name = "$0,10bs")]
        public int  diezc  { get; set; }

        [Display(Name = "$0,20bs")]
        public int veintec { get; set; }

        [Display(Name = "$0,50bs")]
        public int cincuentac { get; set; }

        [Display(Name = "$1bs")]
        public int unb { get; set; }

        [Display(Name = "$2bs")]
        public int dosb { get; set; }

        [Display(Name = "$5bs")]
        public int cincob { get; set; }

        [Display(Name = "$10bs")]
        public int diezb { get; set; }

        [Display(Name = "$20bs")]
        public int veinteb { get; set; }

        [Display(Name = "$50bs")]
        public int cincuentab { get; set; }

        [Display(Name = "$100bs")]
        public int cienb { get; set; }

        [Display(Name = "$200bs")]
        public int doscientosb { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }
    }
}
