using NUnit.Framework;
using RocketSafeLandingChecker.Models;
using System.Collections.Generic;

namespace RocketSafeLandingChecker.Tests
{
    public class RocketTrajectoriesTests
    {
        private RocketTrajectories _rocketTrajectories;
        private Point _defaultPoint;
        
        private int _riskDistanse;

        [SetUp]
        public void Setup()
        {
            _riskDistanse = 2;
            _rocketTrajectories = new RocketTrajectories(_riskDistanse);
            _defaultPoint = new Point(2, 2);
            _rocketTrajectories.AddRocketTrajectory(_defaultPoint);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public void ShouldBeClashForWhenRocketIsNearOtherRocketTrajectory(Point trajectory)
        {
            Assert.AreEqual(IsThereClashRisk(_defaultPoint, trajectory), 
                _rocketTrajectories.WillClashHappen(trajectory));
        }

        private bool IsThereClashRisk(Point rt1, Point rt2)
        {
            return rt2.X >= rt1.X - _riskDistanse && rt2.X <= rt1.X + _riskDistanse &&
                rt2.Y >= rt1.Y - _riskDistanse && rt2.Y <= rt1.Y + _riskDistanse;
        }

        public static IEnumerable<Point> GetTestCases()
        {
            for (int x = 1; x < 5; x++)
            {
                for (int y = 1; y <= 5; y++)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}