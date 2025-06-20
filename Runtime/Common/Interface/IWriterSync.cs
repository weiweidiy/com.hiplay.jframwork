using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Interface
{
    public interface IWriterSync
    {
        void Write(string toPath, byte[] buffer, Encoding encoding = null);

        void Write(string toPath, string buffer , Encoding encoding = null);
    }
}
