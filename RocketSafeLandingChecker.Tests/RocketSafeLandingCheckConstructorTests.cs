using Moq;
using NUnit.Framework;
using RocketSafeLandingChecker;
using RocketSafeLandingChecker.Models;
using System;

namespace RocketSafeLandingChecker.Tests
{
    public class RocketSafeLandingCheckConstructorTests
    {
        private RocketSafeLandingCheck _rocketSafeLandingCheck;
        private Mock<IRocketTrajectories> _rocketTrajectories;

        private LandingArea DefaultLandingArea;
        private Platform DefaultPlatform;

        [SetUp]
        public void Setup()
        {
            _rocketTrajectories = new Mock<IRocketTrajectories>();
            DefaultLandingArea = new LandingArea(new Rectangle(100, 100));
            DefaultPlatform = new Platform(new Point(5, 5), new Rectangle(10, 10));
        }

        [Test]
        public void ShouldThrowExceptionWhenLandingAreaIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _rocketSafeLandingCheck = new RocketSafeLandingCheck(
                    _rocketTrajectories.Object,
                    null,
                    DefaultPlatform));
        }

        [Test]
        public void ShouldThrowExceptionWhenPlatformIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _rocketSafeLandingCheck = new RocketSafeLandingCheck(
                    _rocketTrajectories.Object, 
                    DefaultLandingArea
                    , null));
        }
    }
}