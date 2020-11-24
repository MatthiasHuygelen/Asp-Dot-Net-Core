using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scaffolding.Domain
{
    public class Boek
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public int Jaar { get; set; }
        public int AantalPaginas { get; set; }
    }
}
