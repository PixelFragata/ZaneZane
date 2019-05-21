using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.Infra.Data.Config
{
    public class MovimentoEstoqueConfig : IEntityTypeConfiguration<MovimentoEstoque>
    {
        public void Configure(EntityTypeBuilder<MovimentoEstoque> builder)
        {
            try
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Codigo).IsRequired();
                builder.Property(p => p.IsEntrada).IsRequired();
                builder.Property(p => p.Quantidade).IsRequired();
                builder.Property(p => p.DataMovimento).IsRequired();
                builder.Property(p => p.Data).IsRequired();

                builder.HasOne(c => c.TipoEntrada);
                builder.HasOne(c => c.Estoque);
                builder.HasOne(c => c.Servico);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
