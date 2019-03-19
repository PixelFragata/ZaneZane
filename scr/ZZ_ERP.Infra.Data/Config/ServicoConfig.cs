using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.Infra.Data.Config
{
    public class ServicoConfig : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            try
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Codigo).HasMaxLength(20).IsRequired();
                builder.Property(p => p.DescricaoResumida).HasMaxLength(200).IsRequired();
                builder.Property(p => p.DescricaoCompleta).HasMaxLength(Int32.MaxValue);
                builder.Property(p => p.Observacoes).HasMaxLength(Int32.MaxValue);

                builder.HasOne(s => s.UnidadeMedida);
                builder.HasOne(s => s.TipoServico);
                builder.HasOne(s => s.CentroCusto);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
