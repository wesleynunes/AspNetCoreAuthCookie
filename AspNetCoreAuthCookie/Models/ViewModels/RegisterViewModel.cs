using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthCookie.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo Nome recebe no máximo 100 Caracteres")]
        [DisplayName("Nome Completo")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Confrimar Senha é obrigatório")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [Compare(nameof(Password), ErrorMessage = "A senha e a confirmação não estão iguais")]
        [Display(Name = "Confirmar senha")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "O campo Tipo de Usuário é obrigatório")]
        [DisplayName("Tipo de Usúario")]
        public UserType UserTypes { get; set; } = UserType.Users;

        [Required(ErrorMessage = "O campo Ativo de Usuário é obrigatório")]
        [DisplayName("Usuario Ativo")]
        public bool ActiveUser { get; set; } = true;     
        
        public bool RememberMe { get; set; } 
    }
}
