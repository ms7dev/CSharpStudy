using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.BL.Interfaces
{
    public interface IFileManager<T>
    {
        string GetContent(T filePath);
        string GetContent(T filePath, Encoding encoding);
        void SaveContent(string content, T filePath);
        void SaveContent(string content, T filePath, Encoding encoding);
        int GetSymbolCount(string content);
        bool IsExist(T filePath);
    }
}
