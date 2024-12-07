using FitnessApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data.Seeding;

public static class ClassesSeeder
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Class>().HasData(
			new Class
			{
				Id = 1,
				Name = "Morning Yoga",
				InstructorId = 1,
				Schedule = DateTime.Parse("2024-12-05T07:00:00"),
				Duration = 60,
				Description = "A calm and peaceful yoga session to start your day.",
				Price = 50.00m,
				ImageUrl = "https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg"
			},
			new Class
			{
				Id = 2,
				Name = "HIIT Challenge",
				InstructorId = 2,
				Schedule = DateTime.Parse("2024-12-05T18:00:00"),
				Duration = 45,
				Description = "An intense, high-energy interval training session.",
				Price = 50.00m,
				ImageUrl = "https://i.ytimg.com/vi/66_hHeSUrzU/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQjZQw/2.jpg"
			},
			new Class
			{
				Id = 3,
				Name = "Zumba Dance",
				InstructorId = 3,
				Schedule = DateTime.Parse("2024-12-06T10:00:00"),
				Duration = 60,
				Description = "A fun and energetic Zumba dance class for all levels.",
				Price = 90.00m,
				ImageUrl = "https://i.ytimg.com/vi/N3wBXogMYfM/hq720.jpg?sqp=-oaymwE7CK4FEIIDSFryq4qpAy0IARUAAAAAGAElAADIQj0AgKJD8AEB-AH-CYAC0AWKAgwIABABGGUgUihUMA8=&rs=AOn4CLD9yvCPKa7mHvL_lLUQr-TvnlNYRw"
			},
			new Class
			{
				Id = 4,
				Name = "Pilates Core",
				InstructorId = 4,
				Schedule = DateTime.Parse("2024-12-07T08:30:00"),
				Duration = 60,
				Description = "Strengthen your core and improve posture with this Pilates class.",
				Price = 85.00m,
				ImageUrl = "https://media.self.com/photos/5b9c24c208e0b96633983ce8/2:1/w_2580,c_limit/pilates-butt-core-workout.jpg"
			},
			new Class
			{
				Id = 5,
				Name = "Strength Training Basics",
				InstructorId = 5,
				Schedule = DateTime.Parse("2024-12-07T17:00:00"),
				Duration = 45,
				Description = "An introductory strength training session focusing on weightlifting techniques.",
				Price = 95.00m,
				ImageUrl = "https://www.jefit.com/_next/image?url=https%3A%2F%2Fcdn.jefit.com%2Fuc%2Ffile%2Fc34238b8cd6e3cf7%2F1.jpg&w=3840&q=75"
			},
			new Class
			{
				Id = 6,
				Name = "ММА Essentials",
				InstructorId = 6,
				Schedule = DateTime.Parse("2024-12-08T20:00:00"),
				Duration = 30,
				Description = "Learn the basics of ММА in this high-energy and engaging class.",
				Price = 150.00m,
				ImageUrl = "https://mf.b37mrtl.ru/rbthmedia/images/2018.02/article/5a93bf3385600a57b0096f7e.jpg"
			}
		);
	}
}