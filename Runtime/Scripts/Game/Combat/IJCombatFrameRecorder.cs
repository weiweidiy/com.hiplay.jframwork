namespace JFrame.Game
{
    /// <summary>
    /// 战斗更新逻辑次数统计
    /// </summary>
    public interface IJCombatFrameRecorder
    {
        /// <summary>
        /// 重置帧数
        /// </summary>
        void ResetFrame();
        /// <summary>
        /// 获取当前帧
        /// </summary>
        /// <returns></returns>
        int GetCurFrame();
        /// <summary>
        /// 下一帧
        /// </summary>
        /// <returns></returns>
        int NextFrame();
    }
}
