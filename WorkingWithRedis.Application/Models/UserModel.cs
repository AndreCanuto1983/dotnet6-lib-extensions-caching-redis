﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models
{
    public class UserModel
    {
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter numbers only in Cpf/Cnpj")]
        [MaxLength(14, ErrorMessage = "Cpf/Cnpj must not be longer than 14 characters")]
        public string cpfCnpj { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public string Datetime { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
