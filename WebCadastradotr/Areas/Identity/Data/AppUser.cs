using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCadastrador.Areas.Identity.Data
{
    public class AppUser : IdentityUser
    {
        public string CustomTag { get; set; }
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}
