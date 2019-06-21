using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    [Table("ReturnBook")]
    public class ReturnBook
    {
        [Key]
        public int ReturnBookId { get; set; }
        public string ReturnDate { get; set; }
        public int ReturnQty { get; set; }
        public int BookRentId { get; set; }
        public virtual BookRent BookRent { get; set; }
    }
}
