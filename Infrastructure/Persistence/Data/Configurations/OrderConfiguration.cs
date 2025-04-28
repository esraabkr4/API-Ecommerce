using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            #region ShipingAddress
            builder.OwnsOne(order => order.ShipingAddress, Address => Address.WithOwner());
            #endregion
            #region OrderItem
            builder.HasMany(order => order.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region PaymentStatus
            builder.Property(order => order.paymentStatus)
                .HasConversion(s => s.ToString(),s=>Enum.Parse<OrderPaymentStatus>(s));
            #endregion
            #region DeliveryMethod
            builder.HasOne(order => order.deliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            #endregion
            #region SubTotal
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,3)");
            #endregion
            //builder.HasMany(o => o.orderItems)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
