using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;

namespace MVVM
{
    internal class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool OpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
        public bool SaveFileDialog()
        {
            var saveFileDialog = new SaveFileDialog();
            if ((bool)saveFileDialog.ShowDialog())
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }
        public void ShowMessage(string message) => MessageBox.Show(message);
    }
}
