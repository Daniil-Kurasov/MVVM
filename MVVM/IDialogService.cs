using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    internal interface IDialogService
    {
        string FilePath { get; set; }
        bool OpenFileDialog();
        bool SaveFileDialog();
        void ShowMessage(string message);
    }
}
