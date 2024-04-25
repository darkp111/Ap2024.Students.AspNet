using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Common.Models
{
    public class AdministrationEmployee
    {
        public int Id { get; set; }
        public string? Position { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }

        public AdministrationEmployee() 
        {
            
        }
    }
}
