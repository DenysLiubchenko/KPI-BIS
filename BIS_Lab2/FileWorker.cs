using BIS.Interface;
using System;
using System.IO;
using System.Text;

namespace BIS
{
    internal class FileWorker : IFileWorker
    {
        Encoding encoding = Encoding.UTF8;
        public string OpenFile(string path)
        {
            return File.ReadAllText(path, encoding);
        }
        public void CreateFile(string path, string text)
        {
            File.WriteAllTextAsync(path, text, encoding);
        }
        public void ExportFile(string path, string text)
        {
            CreateFile(path, text);
        }
        public void SaveFile(string path, string text)
        {
            CreateFile(path, text);
        }
        public string AboutMe()
        {
            return "Студент 2 курсу ЦТЕ\nТР-13 Любченко Денис";
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
