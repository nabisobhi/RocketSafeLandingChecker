using Moq;
using NUnit.Framework;
using RocketSafeLandingChecker.Models;

namespace RocketSafeLandingChecker.Tests
{
    public class RocketSafeLandingCheckTests
    {
        private RocketSafeLandingCheck _rocketSafeLandingCheck;
        private RocketTrajectories _rocketTrajectories;
        
        private int _riskDistanse;
        private LandingArea DefaultLandingArea;
        private Platform DefaultPlatform;

        private string[,] DefaultAreaMap;

        [SetUp]
        public void Setup()
        {
            _riskDistanse = 2;
            DefaultLandingArea = new LandingArea(new Rectangle(100, 100));
            DefaultPlatform = new Platform(new Point(5, 5), new Rectangle(10, 10));
            _rocketTrajectories = new RocketTrajectories(_riskDistanse);
            _rocketSafeLandingCheck = new RocketSafeLandingCheck(
                _rocketTrajectories,
                DefaultLandingArea, 
                DefaultPlatform);
            InitDefaultAreaMap();
        }

        [Test]
        public void ShouldBeOkForBoardingWhenRocketIsOnTrajectory()
        {
            for (int x = 0; x < DefaultPlatform.Area.Width; x++)
            {
                for (int y = 0; y < DefaultPlatform.Area.Length; y++)
                {
                    string actual = _rocketSafeLandingCheck.Check(new Point(x, y));
                    string expected = DefaultAreaMap[x, y];
                    
                    Assert.AreEqual(expected, actual);
                    
                    if (actual == "ok for landing")
                    {
                        InitDefaultAreaMap();
                        SetClashAround(x, y);
                    }
                }
            }
        }
        
        private void InitDefaultAreaMap()
        {
            DefaultAreaMap = new string[DefaultLandingArea.Area.Width, DefaultLandingArea.Area.Length];

            for (int x = 0; x < DefaultLandingArea.Area.Width; x++)
            {
                for (int y = 0; y < DefaultLandingArea.Area.Length; y++)
                {
                    if (x < DefaultPlatform.Location.X || x >= DefaultPlatform.Location.X + DefaultPlatform.Area.Width)
                        DefaultAreaMap[x, y] = "out of platform";
                    else if (y < DefaultPlatform.Location.Y || y >= DefaultPlatform.Location.Y + DefaultPlatform.Area.Length)
                        DefaultAreaMap[x, y] = "out of platform";
                    else
                        DefaultAreaMap[x, y] = "ok for landing";
                }
            }
        }

        private void SetClashAround(int x, int y)
        {
            for (int i = x - _riskDistanse; i <= x + _riskDistanse; i++)
            {
                for (int j = y - _riskDistanse; j <= y + _riskDistanse; j++)
                {
                    if(i == x && j == y)
                        continue;

                    if (IsPointOnPlatform(i, j))
                        DefaultAreaMap[i, j] = "clash";
                }
            }
        }

        private bool IsPointOnPlatform(int x, int y)
        {
            return x >= DefaultPlatform.Location.X &&
                   x <= DefaultPlatform.Location.X + DefaultPlatform.Area.Width &&
                   y >= DefaultPlatform.Location.Y &&
                   y <= DefaultPlatform.Location.Y + DefaultPlatform.Area.Length;
        }
    }
}