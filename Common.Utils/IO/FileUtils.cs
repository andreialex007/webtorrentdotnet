using System;
using System.IO;

namespace Common.Utils.IO
{
    public class FileUtils
    {
        public string GetRandomFileName(string extension = "")
        {
            var suffix = string.IsNullOrEmpty(extension) ? string.Empty : string.Format(".{0}", extension);
            var fileName = string.Format("{0}{1}", Guid.NewGuid(), suffix);
            return Path.Combine(Path.GetTempPath(), fileName);
        }
    }
}
