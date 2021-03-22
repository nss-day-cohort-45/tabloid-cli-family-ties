using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.Models
{
    class Journal
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public String CreateDateTime { get; set; }
    }
}

//createdatetime is supposed to be a type of datetime