using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatBaseUnitBuilder : IJCombatUnitBuilder
    {
        Dictionary<int, IJCombatUnitInfo> _unitCache = new Dictionary<int, IJCombatUnitInfo>();

        bool useCache = false;

        protected IJCombatAttrBuilder attrBuilder;
        protected IJCombatActionBuilder actionBuilder;
        public JCombatBaseUnitBuilder(IJCombatAttrBuilder attrBuilder, IJCombatActionBuilder actionBuilder)
        {
            this.actionBuilder = actionBuilder;
            this.attrBuilder = attrBuilder;
        }

        public bool UseCache { get => useCache; set => useCache = value; }
        

        public IJCombatUnitInfo Build(int key)
        {
            if(useCache)
            {
                if (_unitCache.ContainsKey(key))
                    return GetFromCache(key);
            }

            var info = Create(key);
            _unitCache[key] = info;
            return info;
        }

        protected virtual IJCombatUnitInfo GetFromCache(int key)
        {
            return _unitCache[key];
        }

        protected abstract IJCombatUnitInfo Create(int key);
        //{
        //    var info = new IJCombatUnitInfo();
        //    info.Uid = Guid.NewGuid().ToString();
        //    info.AttrList = attrBuilder.Create(key);
        //    info.Actions = actionBuilder.Create(key);
        //    return info;
        //}
    }
}
