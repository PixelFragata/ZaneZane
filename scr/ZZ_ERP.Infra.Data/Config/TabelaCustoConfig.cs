using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.Infra.Data.Config
{
    public class TabelaCustoConfig : IEntityTypeConfiguration<TabelaCusto>
    {
        public void Configure(EntityTypeBuilder<TabelaCusto> builder)
        {
            try
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Descricao).HasMaxLength(Int32.MaxValue).IsRequired();
                builder.Property(p => p.Preco).IsRequired();
                builder.Property(p => p.DataTabela).IsRequired();

                builder.HasOne(t => t.Servico).WithMany(s => s.TabelasCusto).HasForeignKey(t => t.ServicoId);

            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
