namespace Studio23.SS2.IngameAchievements.Core
{
	public abstract class AchievementManager
	{
		public abstract string Name { get; }
		public abstract void SetupAchievements();
		public abstract bool UnlockAchievement(string achievementIdentifier);
		public abstract void SaveAchievements();
		public abstract void LoadAchievements();
	}
}