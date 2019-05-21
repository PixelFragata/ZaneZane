using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.Infra.Data.Config
{
    public class CompraManualConfig : IEntityTypeConfiguration<CompraManual>
    {
        public void Configure(EntityTypeBuilder<CompraManual> builder)
        {
            try
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Codigo).IsRequired();
                builder.Property(p => p.Observacao).IsRequired(false);
                builder.Property(p => p.DataEmissao).IsRequired();
                builder.Property(p => p.ControlaEstoque).IsRequired();
                builder.Property(p => p.MovimentouEstoque).IsRequired();
                builder.Property(p => p.ValorTotal).IsRequired();

                builder.HasOne(c => c.TipoEntrada);
                builder.HasOne(c => c.Fornecedor);
                builder.HasMany(c => c.Itens).WithOne(i => i.CompraManual);
                
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
