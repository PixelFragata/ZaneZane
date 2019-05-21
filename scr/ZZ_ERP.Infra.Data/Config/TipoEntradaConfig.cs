using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.Infra.Data.Config
{
    public class TipoEntradaConfig : IEntityTypeConfiguration<TipoEntrada>
    {
        public void Configure(EntityTypeBuilder<TipoEntrada> builder)
        {
            try
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Descricao).IsRequired();
                builder.Property(p => p.ControlaEstoque).IsRequired();
                builder.Property(p => p.NomeEntity).IsRequired(false);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
