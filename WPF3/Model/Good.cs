using System;
using System.ComponentModel.DataAnnotations;

namespace WPF3.Model
{
    public class Good
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? QrCode { get; set; }
    }
}
