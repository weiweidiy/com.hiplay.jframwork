namespace JFramework.Game
{
    /// <summary>
    /// 回合事件监听器接口
    /// </summary>
    public interface IJCombatTurnBasedEventListener 
    {
        void OnTurnStart(int frame);
        void OnTurnEnd(int frame);
    }
}
