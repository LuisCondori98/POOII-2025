using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb02.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }
}