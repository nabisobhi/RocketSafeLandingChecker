namespace RocketSafeLandingChecker.Models
{
    public class Platform
    {
        public Point Location { get; set; }
        public Rectangle Area { get; set; }

        public Platform(Point location, Rectangle area)
        {
            Location = location;
            Area = area;
        }
    }
}
