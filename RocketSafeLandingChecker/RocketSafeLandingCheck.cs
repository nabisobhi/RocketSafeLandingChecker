using RocketSafeLandingChecker.Models;
using System;

namespace RocketSafeLandingChecker
{
    public class RocketSafeLandingCheck
    {
        private readonly IRocketTrajectories _rocketTrajectories;

        private readonly LandingArea _landingArea;
        private readonly Platform _platform;

        public RocketSafeLandingCheck(IRocketTrajectories rocketTrajectories,
            LandingArea landingArea, 
            Platform platform)
        {
            _rocketTrajectories = rocketTrajectories;
            _landingArea = landingArea ?? throw new ArgumentNullException(nameof(_landingArea));
            _platform = platform ?? throw new ArgumentNullException(nameof(_platform));
            
            CheckLandinAreaValidity();
            CheckPlatformValidity();
        }

        private void CheckLandinAreaValidity()
        {
            if (_landingArea.Area is null)
                throw new ArgumentNullException(nameof(_landingArea.Area));

            if (_landingArea.Area.Width <= 0 || _landingArea.Area.Length <= 0)
                throw new ArgumentOutOfRangeException(nameof(_landingArea.Area));
        }

        private void CheckPlatformValidity()
        {
            if (_platform.Area is null)
                throw new ArgumentNullException(nameof(_platform.Area));

            if (_platform.Area.Width <= 0 || _platform.Area.Length <= 0)
                throw new ArgumentOutOfRangeException(nameof(_platform.Area));
        }

        public string Check(Point rocketTrajectory)
        {
            if (rocketTrajectory.X > _landingArea.Area.Width)
                throw new ArgumentOutOfRangeException(nameof(rocketTrajectory.X));

            if (rocketTrajectory.Y > _landingArea.Area.Length)
                throw new ArgumentOutOfRangeException(nameof(rocketTrajectory.Y));

            if (IsTrajectoryOnPlatform(rocketTrajectory))
            {
                if (_rocketTrajectories.WillClashHappen(rocketTrajectory))
                    return "clash";
                _rocketTrajectories.AddRocketTrajectory(rocketTrajectory);
                return "ok for landing";
            }

            return "out of platform";
        }

        private bool IsTrajectoryOnPlatform(Point trajectory)
        {
            return trajectory.X >= _platform.Location.X &&
                trajectory.X <= _platform.Location.X + _platform.Area.Width &&
                trajectory.Y >= _platform.Location.Y &&
                trajectory.Y <= _platform.Location.Y + _platform.Area.Length;
        }
    }
}
