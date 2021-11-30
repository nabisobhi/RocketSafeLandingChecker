using Moq;
using NUnit.Framework;
using RocketSafeLandingChecker.Models;
using System.Collections.Generic;

namespace RocketSafeLandingChecker.Tests
{
    public class RocketSafeLandingCheckTests
    {
        private RocketSafeLandingCheck _rocketSafeLandingCheck;
        private Mock<IRocketTrajectories> _rocketTrajectories;

        private static LandingArea DefaultLandingArea;
        private static Platform DefaultPlatform;

        static RocketSafeLandingCheckTests()
        {
            DefaultLandingArea = new LandingArea(new Rectangle(100, 100));
            DefaultPlatform = new Platform(new Point(5, 5), new Rectangle(10, 10));
        }

        [SetUp]
        public void Setup()
        {
            _rocketTrajectories = new Mock<IRocketTrajectories>();
            _rocketSafeLandingCheck = new RocketSafeLandingCheck(
                _rocketTrajectories.Object,
                DefaultLandingArea, 
                DefaultPlatform);
        }

        [Test]
        [TestCaseSource(nameof(GetOnTrajectoryTestCases))]
        public void ShouldBeOkForBoardingWhenRocketIsOnTrajectory(Point trajectory)
        {
            Assert.AreEqual("ok for landing", _rocketSafeLandingCheck.Check(trajectory));
        }

        public static IEnumerable<Point> GetOnTrajectoryTestCases()
        {
            for (int x = DefaultPlatform.Location.X; x <= DefaultPlatform.Location.X + DefaultPlatform.Area.Width; x++)
            {
                for (int y = DefaultPlatform.Location.Y; y <= DefaultPlatform.Location.Y + DefaultPlatform.Area.Length; y++)
                {
                    yield return new Point(x, y);
                }
            }
        }

        [Test]
        [TestCaseSource(nameof(GetOffTrajectoryTestCases))]
        public void ShouldBeOkForBoardingWhenRocketIsNotOnTrajectory(Point trajectory)
        {
            Assert.AreEqual("out of platform", _rocketSafeLandingCheck.Check(trajectory));
        }

        public static IEnumerable<Point> GetOffTrajectoryTestCases()
        {
            for (int x = 1; x <= DefaultLandingArea.Area.Width; x++)
            {
                if (x >= DefaultPlatform.Location.X && x <= DefaultPlatform.Location.X + DefaultPlatform.Area.Width)
                    continue;

                for (int y = 1; y <= DefaultLandingArea.Area.Length; y++)
                {
                    if (y >= DefaultPlatform.Location.Y && y <= DefaultPlatform.Location.Y + DefaultPlatform.Area.Length)
                        continue;

                    yield return new Point(x, y);
                }
            }
        }
    }
}