using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lust.Infra.CrossCutting.Identity.Models
{
    public class ApplicationRole : IdentityRole
    {
        [StringLength(250)]
        public string Description { get; set; }

    }
}
