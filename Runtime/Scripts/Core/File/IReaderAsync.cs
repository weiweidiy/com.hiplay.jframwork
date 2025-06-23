using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JFramework.Common.Interface
{
    public interface IReaderAsync
    {
        Task<T> ReadAsync<T>(string location, IConverter<T> converter);
    }
}
