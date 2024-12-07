using FitnessApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data.Seeding;

public static class InstructorsSeeder
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Instructor>().HasData(
			new Instructor
			{
				Id = 1,
				FirstName = "Natalie",
				LastName = "Asatryan",
				Bio = "Natalie is a certified yoga instructor with over 10 years of experience. She is passionate about helping others achieve their fitness goals and improve their overall well-being.",
				Specialization = "Yoga",
				ImageUrl = "https://horizonweekly.ca/wp-content/uploads/2021/01/Nat-2.jpg"
			},
			new Instructor
			{
				Id = 2,
				FirstName = "Warren",
				LastName = "Scott",
				Bio = "Warren is a certified personal trainer and fitness coach. He specializes in high-intensity interval training (HIIT) and enjoys helping clients push their limits and reach their full potential.",
				Specialization = "HIIT",
				ImageUrl = "https://images.squarespace-cdn.com/content/v1/651489d366d19e59b7bbf9cf/a68428a6-992f-45a4-adfc-1b5a75e5cfda/Warren_square500.jpg"
			},
			new Instructor
			{
				Id = 3,
				FirstName = "Emily",
				LastName = "Johnson",
				Bio = "Emily is a certified Zumba instructor with a background in dance and fitness. She loves creating a fun and inclusive environment where everyone can enjoy the benefits of Zumba.",
				Specialization = "Zumba",
				ImageUrl = "https://d29za44huniau5.cloudfront.net/uploads/2023/11/first-class-mobile.png"
			},
			new Instructor
			{
				Id = 4,
				FirstName = "Olivia",
				LastName = "Williams",
				Bio = "Olivia is a certified Pilates instructor with a passion for helping individuals improve their core strength and flexibility.",
				Specialization = "Pilates",
				ImageUrl = "https://www.clubpilates.com/hubfs/Leah-Pfrommer-Club-Pilates-instructor-exclusive-interview-with-Athletech-News-1.jpg"
			},
			new Instructor
			{
				Id = 5,
				FirstName = "Wolff",
				LastName = "Jameson",
				Bio = "Wolff is a certified strength training coach. He specializes in weightlifting and conditioning, helping clients build muscle and endurance.",
				Specialization = "Strength Training",
				ImageUrl = "https://jwfitnesssystems.com/wp-content/uploads/2023/02/CW1_7335-scaled.jpg"
			},
			new Instructor
			{
				Id = 6,
				FirstName = "Conor",
				LastName = "McGregor",
				Bio = "Conor Anthony McGregor (born 14 July 1988) is an Irish professional mixed martial artist, professional boxer, businessman and actor. He is a former Ultimate Fighting Championship (UFC) Featherweight and Lightweight Champion, becoming the first UFC fighter to hold UFC championships in two weight classes simultaneously.",
				Specialization = "ММА",
				ImageUrl = "https://a.espncdn.com/i/headshots/mma/players/full/3022677.png"
			}
		);
	}
}