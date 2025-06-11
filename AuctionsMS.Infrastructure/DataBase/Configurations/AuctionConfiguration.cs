using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionMS.Domain.Entities;

namespace AuctionMS.Infrastructure.DataBase.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Description).IsRequired();
            builder.Property(s => s.Images).IsRequired();
            builder.Property(s => s.BasePrice).IsRequired();
            builder.Property(s => s.StartTime).IsRequired();
            builder.Property(s => s.EndTime).IsRequired();
            builder.Property(s => s.MinimumIncrement).IsRequired();
            builder.Property(s => s.ReservePrice).IsRequired();
            builder.Property(s => s.AuctionType).IsRequired();
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.Property(s => s.UpdatedAt).IsRequired();
            builder.Property(s => s.Products).IsRequired();
            builder.Property(s => s.UserId).IsRequired();
        }
    }

}

