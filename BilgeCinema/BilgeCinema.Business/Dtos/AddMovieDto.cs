using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeCinema.Business.Dtos
{
    public class AddMovieDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Director { get; set; }
        public decimal UnitPrice { get; set; }


    }
}
