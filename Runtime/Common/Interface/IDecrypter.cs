using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Interface
{
    public interface IDecrypter
    {
        byte[] Decrypt(byte[] bytes);
    }
}
