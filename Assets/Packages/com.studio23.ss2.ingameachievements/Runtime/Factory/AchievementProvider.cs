using Cysharp.Threading.Tasks;

namespace Studio23.SS2.AchievementSystem.Providers
{
	public  abstract class AchievementProvider
	{
		public abstract string Name { get; }
		public abstract int Priority {  get; }

		public abstract void Initialize();
		public abstract UniTask<bool> UnlockAchievementAsync(string achievementIdentifier);

	}
}