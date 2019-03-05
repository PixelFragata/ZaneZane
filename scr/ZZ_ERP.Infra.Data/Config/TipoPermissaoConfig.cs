using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.Infra.Data.Config
{
    public class TipoPermissaoConfig : IEntityTypeConfiguration<TipoPermissao>
    {
        public void Configure(EntityTypeBuilder<TipoPermissao> builder)
        {
            try
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Descricao).HasMaxLength(Int32.MaxValue).IsRequired();
                builder.Property(p => p.Valor).IsRequired();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
