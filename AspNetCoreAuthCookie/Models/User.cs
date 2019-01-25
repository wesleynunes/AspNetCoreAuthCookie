using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthCookie.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "O campo Usuario é obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo Login recebe no máximo 100 Caracteres")]
        [DisplayName("Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }       
                
        [Required(ErrorMessage = "O campo Tipo de Usuário é obrigatório")]
        [DisplayName("Tipo de Usúario")]
        public UserType UserTypes { get; set; }

        [Required(ErrorMessage = "O campo Ativo de Usuário é obrigatório")]
        [DisplayName("Usuario Ativo")]
        public bool ActiveUser { get; set; }

        [DisplayName("Lembre de mim")]
        public bool RememberMe { get; set; }
    }
}
