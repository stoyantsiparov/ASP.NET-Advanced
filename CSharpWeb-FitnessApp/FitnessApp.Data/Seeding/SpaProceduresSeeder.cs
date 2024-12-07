using FitnessApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data.Seeding;

public static class SpaProceduresSeeder
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SpaProcedure>().HasData(
			new SpaProcedure
			{
				Id = 1,
				Name = "Relaxing Massage",
				Description = "A soothing massage to relieve tension and stress.",
				Duration = 60,
				Price = 50.00m,
				ImageUrl = "https://www.dshieldsusa.com/wp-content/uploads/2021/05/relaxing-massage-slide.jpg"
			},
			new SpaProcedure
			{
				Id = 2,
				Name = "Facial Treatment",
				Description = "A rejuvenating facial to nourish and hydrate your skin.",
				Duration = 45,
				Price = 40.00m,
				ImageUrl = "https://spamd.net/wp-content/uploads/2022/03/medications-facial-treatments.jpg"
			},
			new SpaProcedure
			{
				Id = 3,
				Name = "Aromatherapy Session",
				Description = "A session using essential oils to promote relaxation and well-being.",
				Duration = 30,
				Price = 30.00m,
				ImageUrl = "https://elementsmassage.com/files/shared/AZ%20-%20Elements%20Massage%205-1864269.jpg"
			},
			new SpaProcedure
			{
				Id = 4,
				Name = "Hot Stone Massage",
				Description = "A therapeutic massage using smooth, heated stones to ease tension.",
				Duration = 75,
				Price = 70.00m,
				ImageUrl = "https://images-prod.healthline.com/hlcmsresource/images/topic_centers/1296x728_HEADER_benefits-of-hot-stone-massage.jpg"
			},
			new SpaProcedure
			{
				Id = 5,
				Name = "Deep Tissue Massage",
				Description = "A massage targeting deeper layers of muscle tissue to release chronic tension.",
				Duration = 60,
				Price = 60.00m,
				ImageUrl = "https://propelphysiotherapy.com/wp-content/uploads/2023/08/what-is-deep-tissue-massage-therapy-propel-physiotherapy.jpg"
			},
			new SpaProcedure
			{
				Id = 6,
				Name = "Seaweed Body Wrap",
				Description = "A detoxifying wrap using nutrient-rich seaweed to revitalize your skin.",
				Duration = 90,
				Price = 85.00m,
				ImageUrl = "https://s3.amazonaws.com/salonclouds-uploads/blog/blog_1605466361125864114.png"
			}
		);
	}
}