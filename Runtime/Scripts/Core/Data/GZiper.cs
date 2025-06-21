using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using JFramework.Common.Interface;

namespace JFramework.Common
{
    public class GZiper : ICompress , IProcesser
    {
        /// <summary>
        /// 压缩字节
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Compress(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
                compressedzipStream.Write(bytes, 0, bytes.Length);
                compressedzipStream.Close();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 处理数据接口
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Process(byte[] bytes)
        {
            return Compress(bytes);
        }
    }
}
