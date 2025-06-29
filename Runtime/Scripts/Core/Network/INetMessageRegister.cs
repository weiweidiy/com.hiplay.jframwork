using System;
using System.Collections.Generic;

namespace JFramework
{
    public interface INetMessageRegister
    {
        Dictionary<int, Type> GetAllTables();
    }
}
