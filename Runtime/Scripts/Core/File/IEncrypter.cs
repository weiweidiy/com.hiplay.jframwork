using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Interface
{
    public interface IEncrypter
    {
        byte[] Encrypt(byte[] bytes);
    }
}
