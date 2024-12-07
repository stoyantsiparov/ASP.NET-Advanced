using FitnessApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data.Seeding;

public static class MembershipTypeSeeder
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<MembershipType>().HasData(
			new MembershipType
			{
				Id = 1,
				Name = "Basic",
				Price = 59.99m,
				Duration = 30,
				ImageUrl = "https://i0.wp.com/poolstats.co/wp-content/uploads/2019/01/Basic-Membership.png?fit=400%2C327&ssl=1",
				Description = "A basic membership that grants access to all regular classes and gym facilities."
			},
			new MembershipType
			{
				Id = 2,
				Name = "Elite",
				Price = 99.99m,
				Duration = 60,
				ImageUrl = "https://cdn.vectorstock.com/i/500p/49/16/elite-gold-label-vector-2944916.jpg",
				Description = "An elite membership offering access to all classes, gym facilities, and spa treatments."
			},
			new MembershipType
			{
				Id = 3,
				Name = "Premium",
				Price = 299.99m,
				Duration = 180,
				ImageUrl = "https://thumbs.dreamstime.com/b/premium-membership-badge-stamp-golden-red-ribbon-text-30827692.jpg",
				Description = "A premium membership offering access to all classes, gym facilities, and spa treatments."
			},
			new MembershipType
			{
				Id = 4,
				Name = "VIP",
				Price = 499.99m,
				Duration = 365,
				ImageUrl = "https://cdn11.bigcommerce.com/s-2ooutu2zpl/images/stencil/original/products/35315/51564/VIP_Badge_2__62906.1641934958.png?c=2",
				Description = "An exclusive membership with additional perks including priority booking for events and personal training."
			}
		);
	}
}