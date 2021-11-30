using RocketSafeLandingChecker.Models;

namespace RocketSafeLandingChecker
{
    public interface IRocketTrajectories
    {
        void AddRocketTrajectory(Point rocketTrajectory);
        bool WillClashHappen(Point newRocketTrajectory);
    }
}