using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;

namespace MVVM
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        private Phone selectedPhone;
        public ObservableCollection<Phone> Phones { get; set; }

        private RelayCommand addCommand;

        IFileService fileService;
        IDialogService dialogService;

        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            if(dialogService.SaveFileDialog())
                            {
                                fileService.Save(dialogService.FilePath, Phones.ToList());
                                dialogService.ShowMessage("File saved");
                            }
                        }
                        catch(Exception ex)
                        {
                            dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }

        private RelayCommand openCommand;

        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (dialogService.OpenFileDialog())
                            {
                                Phones.Clear();
                                fileService.Open(dialogService.FilePath).ForEach(phone => Phones.Add(phone));
                                dialogService.ShowMessage("File opened");
                            }
                        }
                        catch(Exception ex)
                        {
                            dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }


        public RelayCommand AddCommand
        {
            get
            {
                return addCommand = new RelayCommand(obj =>
                {
                    Phones.Insert(0, new Phone());
                    SelectedPhone = Phones.First();
                });
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand = new RelayCommand(obj =>
                {
                    if (obj is Phone phone && phone != null)
                        Phones.Remove(phone);
                }, 
                obj => Phones.Count > 0);
            }
        }

        RelayCommand doubleCommand;
        public RelayCommand DoubleCommand
        {
            get
            {
                return doubleCommand ??
                    (doubleCommand = new RelayCommand(obj =>
                    {
                        if (obj is Phone phone && phone != null)
                            Phones.Insert(0, phone.Clone() as Phone);
                    }));
            }
        }

        public Phone SelectedPhone
        {
            get => selectedPhone;
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public ApplicationViewModel()
        {
            dialogService = new DefaultDialogService();
            fileService = new JsonFileService();

            Phones = new ObservableCollection<Phone>
            {
                new Phone("IPhone 7", "Apple", 600),
                new Phone("IPhone 11", "Apple", 700),
                new Phone("Elite x3", "HP", 800),
                new Phone("Mi5S", "Xiaomi", 900),
            };
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
