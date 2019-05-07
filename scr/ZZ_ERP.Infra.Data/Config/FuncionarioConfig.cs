using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.Infra.Data.Config
{
    public class FuncionarioConfig : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            try
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Documento).IsRequired();
                builder.Property(p => p.RazaoSocial).IsRequired();
                builder.Property(p => p.Codigo).IsRequired();

                builder.HasOne(f => f.Endereco);

            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
