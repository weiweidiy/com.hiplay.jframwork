using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IConfigManager
    {
        T GetConfigData<T>(string configName, int key) where T : IUnique;

        List<T> GetConfigDataList<T>(string configName, Func<T, bool> perdicate) where T : IUnique;

        T GetConfigData<T>(string configName, Func<T, bool> perdicate) where T : IUnique;

        List<T> GetAllConfigDataList<T>(string configName) where T : IUnique;
    }
}