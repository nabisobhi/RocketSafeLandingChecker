namespace RocketSafeLandingChecker.Models
{
    public class LandingArea
    {
        public Rectangle Area { get; set; }

        public LandingArea(Rectangle area)
        {
            Area = area;
        }
    }
}
