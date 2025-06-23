using System;
using System.Collections.Generic;

namespace JFramework.Game
{

    public class JConfigManager : IConfigManager
    {

        //要先加载所有文本文件
        Dictionary<string, List<IUnique>> allConfig;

        public void LoadAllConfigFiles(Dictionary<string,string> configInfo)
        {
            
        }


        List<T> IConfigManager.GetAllConfigDataList<T>(string configName)
        {
  
            throw new NotImplementedException();
        }

        T IConfigManager.GetConfigData<T>(string configName, int key)
        {
            throw new NotImplementedException();
        }

        T IConfigManager.GetConfigData<T>(string configName, Func<T, bool> perdicate)
        {
            throw new NotImplementedException();
        }

        List<T> IConfigManager.GetConfigDataList<T>(string configName, Func<T, bool> perdicate)
        {
            throw new NotImplementedException();
        }
    }
}