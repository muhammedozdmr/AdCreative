using AdCreative.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdCreative.Dto;

namespace AdCreative.DataAccess.EntityConfigurations
{
    internal class RandomWordsConfigurations : IEntityTypeConfiguration<WordAdd>
    {
        public void Configure(EntityTypeBuilder<WordAdd> builder)
        {
            builder.ToTable(nameof(WordAdd));
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Word).IsUnicode().HasMaxLength(50);

            builder.HasData(
                new WordAdd() { Id = 1, Word = "AbCdEfG", CountWord = 7, UniqueId = Guid.NewGuid().ToString() },
                new WordAdd() { Id = 2, Word = "Test2AbCdEfG", CountWord = 12, UniqueId = Guid.NewGuid().ToString() },
                new WordAdd() { Id = 3, Word = "TeStUcAbCdEfG", CountWord = 13, UniqueId = Guid.NewGuid().ToString() }
                );
        }
    }
}
