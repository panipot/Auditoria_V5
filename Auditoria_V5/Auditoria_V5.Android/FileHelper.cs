
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Auditoria_V5.Droid;

[assembly: Dependency(typeof(FileHelper))]

namespace Auditoria_V5.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //            string path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "Download");

            return Path.Combine(path, filename);
        }

        string GetDocsPath()
        {
            //return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

             return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "Download");

        }

        public bool Exists(string filename)
        {
            string filepath = GetLocalFilePath(filename);
            //string filepath = GetFilePath(filename);  Probamoos
            return File.Exists(filepath);
        }

        public void WriteText(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            File.WriteAllText(filepath, text);
        }

        public void AppendText(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            File.AppendAllText(filepath, text);
        }

        public string ReadText(string filename)
        {
            string filepath = GetFilePath(filename);
            return File.ReadAllText(filepath);
        }

        public IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(GetDocsPath());
        }

        public void Delete(string filename)
        {
            File.Delete(GetFilePath(filename));
        }

        // Private methods.
        string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsPath(), filename);
        }




    }
}