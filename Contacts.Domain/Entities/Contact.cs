﻿
using System.ComponentModel.DataAnnotations;

namespace Contacts.Domain.Entities
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome não pode ter mais de 100 caracteres")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "DDD é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "DDD deve conter dois caracteres")]

        public required string DDD { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [Phone(ErrorMessage = "Não é um formato válido de telefone")]
        public required string Phone { get; set; }


        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public required string Email { get; set; }
    }
}