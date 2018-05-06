namespace FitMyWay.Library
{
	public class Activity
	{
		public int ActivityId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int Calories { get; set; }

		public int IntensityLevel { get; set; }

		public string ImageURL { get; set; }

		public int Points { get; set; }

		public int ActivityTypeId { get; set; }
	}
}