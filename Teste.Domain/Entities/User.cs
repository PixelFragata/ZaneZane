using System;
using Teste.Domain.Enums;

namespace Teste.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public GenderEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }

    }
}
