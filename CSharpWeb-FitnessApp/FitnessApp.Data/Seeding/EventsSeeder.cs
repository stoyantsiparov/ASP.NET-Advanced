using FitnessApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data.Seeding;

public static class EventsSeeder
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<FitnessEvent>().HasData(
			new FitnessEvent
			{
				Id = 1,
				Title = "Spring City Marathon",
				Description = "Join us for a thrilling 10K spring marathon through the city streets.",
				Location = "Downtown City Center",
				StartDate = DateTime.Parse("2025-04-12T09:00:00"),
				EndDate = DateTime.Parse("2025-04-12T12:00:00"),
				ImageUrl = "https://www.chicagospringhalf.com/wp-content/uploads/sites/32/2023/05/2022_SPRCHI_RaceDay_Ali_282-1024x683.jpg"
			},
			new FitnessEvent
			{
				Id = 2,
				Title = "Mountain Peak Hike",
				Description = "A challenging hike to the top of the mountain with stunning views.",
				Location = "Rocky Mountain Trail",
				StartDate = DateTime.Parse("2025-07-15T06:00:00"),
				EndDate = DateTime.Parse("2025-07-15T15:00:00"),
				ImageUrl = "https://www.reserveamerica.com/articles/wp-content/uploads/2024/07/11174967-1e85-45df-8097-ac30b3bb1c34.jpg"
			},
			new FitnessEvent
			{
				Id = 3,
				Title = "Autumn Lake Walk",
				Description = "A scenic walk around the beautiful autumn lake. Perfect for relaxation and exercise.",
				Location = "Autumn Lake Park",
				StartDate = DateTime.Parse("2025-10-08T10:00:00"),
				EndDate = DateTime.Parse("2025-10-08T12:00:00"),
				ImageUrl = "https://images.stockcake.com/public/c/a/0/ca09354d-17f0-4693-b9d3-fb2d399a07c1_large/autumn-lakeside-walk-stockcake.jpg"
			},
			new FitnessEvent
			{
				Id = 4,
				Title = "Winter Wonderland Run",
				Description = "A festive 5K run through a snowy winter park.",
				Location = "Snowy Pines Park",
				StartDate = DateTime.Parse("2025-12-05T08:00:00"),
				EndDate = DateTime.Parse("2025-12-05T10:00:00"),
				ImageUrl = "https://cdn.shopify.com/s/files/1/0203/9788/3467/files/Craft_AW22_ADV_SubZ_Wool-LS-Tee_4_1024x1024.jpg?v=1695349527"
			},
			new FitnessEvent
			{
				Id = 5,
				Title = "Summer Beach Yoga",
				Description = "Relax and stretch with a peaceful yoga session on the beach.",
				Location = "Golden Sands Beach",
				StartDate = DateTime.Parse("2025-06-20T07:00:00"),
				EndDate = DateTime.Parse("2025-06-20T09:00:00"),
				ImageUrl = "https://www.townofbethanybeach.com/ImageRepository/Document?documentID=7156"
			},
			new FitnessEvent
			{
				Id = 6,
				Title = "Diving",
				Description = "Diving is the sport of jumping or falling into water from a platform or springboard, often with acrobatics. It is part of the Olympic Games and also enjoyed recreationally as a non-competitive activity.",
				Location = "Blue hole",
				StartDate = DateTime.Parse("2025-09-25T12:00:00"),
				EndDate = DateTime.Parse("2025-09-25T16:00:00"),
				ImageUrl = "https://daysym.com/wp-content/uploads/2024/01/dream-about-scuba-diving.jpg"
			}
		);
	}
}