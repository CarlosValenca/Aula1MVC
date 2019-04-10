using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aula1MVC.Models
{
    public class Cliente
    {
        // Utilizando Data Annotations : https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netframework-4.7.2

        // Chave Primária
        [Key]
        public int Id { get; set; }

        [MaxLength(150,ErrorMessage = "Máximo de caracteres (150) excedido")]
        [MinLength(2, ErrorMessage = "Mínimo de caracteres (2) requerido")]
        [DisplayName("Nome do Cliente")]
        [Required(ErrorMessage ="Preencher campo nome")]
        public string Nome { get; set; }

        [MaxLength(150, ErrorMessage = "Máximo de caracteres (150) excedido")]
        [MinLength(2, ErrorMessage = "Mínimo de caracteres (2) requerido")]
        [DisplayName("Sobrenome do Cliente")]
        [Required(ErrorMessage = "Preencher campo sobrenome")]
        public string Sobrenome { get; set; }

        // O scaffold não irá considerar esta coluna se o parâmetro abaixo for false !
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [MaxLength(150, ErrorMessage = "Máximo de caracteres (150) excedido")]
        [MinLength(2, ErrorMessage = "Mínimo de caracteres (2) requerido")]
        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Preencher campo email")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]        
        public string Email { get; set; }
    }
}