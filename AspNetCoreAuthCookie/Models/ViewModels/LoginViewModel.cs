using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthCookie.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Usuario é obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo Login recebe no máximo 100 Caracteres")]
        [DisplayName("Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a sua senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [DisplayName("Senha")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
