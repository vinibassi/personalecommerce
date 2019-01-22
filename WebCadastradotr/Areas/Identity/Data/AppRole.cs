using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCadastrador.Areas.Identity.Data
{
    public class AppRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
