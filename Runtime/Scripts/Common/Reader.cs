using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JFramework.Common.Interface;
using System.Linq;

namespace JFramework.Common
{
    public abstract class Reader : IReader
    {
        public abstract byte[] Read(string location);
        public abstract Task<byte[]> ReadAsync(string location);

        /// <summary>
        /// 数据加工处理器
        /// </summary>
        private JDataProcesserManager _processer;

        /// <summary>
        /// 无参构造器
        /// </summary>
        public Reader() : this(null) { }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="processer">数据加工处理器</param>
        public Reader(JDataProcesserManager processer)
        {
            _processer = processer;
        }

        /// <summary>
        /// 返回加工处理结果
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        protected byte[] GetProcessResult(byte[] bytes)
        {
            byte[] result = (byte[])bytes.Clone();
            if (_processer != null)
                return _processer.GetResult(result);

            return result;
        }
    }
}
