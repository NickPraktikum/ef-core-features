using Interceptors.Entities;
namespace Interceptors.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.Navigation(author => author.Books)
                .AutoInclude();
            builder.HasQueryFilter(author => author.IsDeleted == false);
        }
    }
}
