using BIS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Interface
{
    abstract class Layout
    {
        public string Name;
        public MainWindow MainWindow;
        public abstract void Show();
        public abstract void Encrypt();
        public abstract void Decrypt();
        
    }
}
