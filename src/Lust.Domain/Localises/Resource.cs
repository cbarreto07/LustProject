using Lust.Domain.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lust.Domain.Localises
{
    public class Resource : Entity
    {
        
        
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid CultureId { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
