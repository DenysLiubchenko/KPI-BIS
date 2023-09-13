using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Interface
{
    internal interface IFileWorker
    {
        public void CreateFile(String path, String text);
        public String OpenFile(String path);
        public void SaveFile(String path, String text);
        public void ExportFile(String path, String text);
        public String AboutMe();
        public void Exit();
    }
}
