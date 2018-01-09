using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Kullanıcı adı 4-15 karakter arası olmalıdır.")]
        public string UsersName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Şifre 4-15 karakter arası olmalıdır.")]
        public string Password { get; set; }
    }
}
