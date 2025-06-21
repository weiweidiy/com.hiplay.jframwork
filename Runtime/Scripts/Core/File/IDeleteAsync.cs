using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JFramework.Common.Interface
{
    public interface IDeleteAsync
    {
        Task DeleteAsync(string location);
    }
}
