using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.Infra.Data.Config
{
    public class EnderecoConfig : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            try
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Cep).HasMaxLength(8).IsRequired();
                builder.Property(p => p.Numero).IsRequired();
                builder.Property(p => p.Logradouro).IsRequired();
                builder.Property(p => p.Bairro).IsRequired();
                builder.Property(p => p.Complemento).IsRequired(false);
                builder.Property(p => p.Ibge).IsRequired();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
