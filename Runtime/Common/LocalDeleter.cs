using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JFramework.Common.Interface;

namespace JFramework.Common
{
    /// <summary>
    /// 本地文件删除器
    /// </summary>
    public class LocalDeleter : IDelete, IDeleteAsync
    {
        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="location"></param>
        public void Delete(string location)
        {
            if (File.Exists(location))
                File.Delete(location);
        }

        public Task DeleteAsync(string location)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除多个文件
        /// </summary>
        /// <param name="srcPath">目录路径</param>
        /// <param name="attr"></param>
        public void DeleteAllFiles(string srcPath)
        {
            foreach (FileInfo file in (new DirectoryInfo(srcPath)).GetFiles())
            {
                file.Attributes = FileAttributes.Normal;
                file.Delete();           
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="recursive"></param>
        public void DelectDir(string srcPath , bool recursive = false)
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            dir.Delete(recursive);
        }

        /// <summary>
        /// 是否存在文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ExistsFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 是否存在目录
        /// </summary>
        /// <param name="srcPath"></param>
        /// <returns></returns>
        public bool ExistsDirectory(string srcPath)
        {
            return Directory.Exists(srcPath);
        }
    }
}
