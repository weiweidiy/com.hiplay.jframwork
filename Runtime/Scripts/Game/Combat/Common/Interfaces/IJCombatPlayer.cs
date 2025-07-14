namespace JFramework.Game
{
    public interface IJCombatPlayer
    {
        void Play();

        void RePlay();

        void Stop();

        void SetScale(float scale);

        float GetScale();
    }
}
