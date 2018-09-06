using System;
using System.Collections.Generic;
using System.Text;


namespace Auditoria_V5
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);

        bool Exists(string filename);

        void WriteText(string filename, string text);

        void AppendText(string filename, string text);

        string ReadText(string filename);

        IEnumerable<string> GetFiles();

        void Delete(string filename);

    }
}