using RocketSafeLandingChecker.Models;

namespace RocketSafeLandingChecker
{
    public class RocketTrajectories : IRocketTrajectories
    {
        private Point _rocketTrajectory;
        private readonly int _riskDistanse;

        public RocketTrajectories(int riskDistanse)
        {
            _riskDistanse = riskDistanse;
        }

        public void AddRocketTrajectory(Point rocketTrajectory)
        {
            _rocketTrajectory = rocketTrajectory;
        }

        public bool WillClashHappen(Point newRocketTrajectory)
        {
            if (_rocketTrajectory is null)
                return false;

            return newRocketTrajectory.X >= _rocketTrajectory.X - _riskDistanse && newRocketTrajectory.X <= _rocketTrajectory.X + _riskDistanse &&
                newRocketTrajectory.Y >= _rocketTrajectory.Y - _riskDistanse && newRocketTrajectory.Y <= _rocketTrajectory.Y + _riskDistanse;
        }
    }
}
