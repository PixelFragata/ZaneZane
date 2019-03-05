using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace ZZ_ERP.Infra.Data.Config
{
    public class PermissaoTelaConfig : IEntityTypeConfiguration<PermissaoTela>
    {
        public void Configure(EntityTypeBuilder<PermissaoTela> builder)
        {
            try
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.NomeTela).HasMaxLength(Int32.MaxValue).IsRequired();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
            }
        }
    }
}
