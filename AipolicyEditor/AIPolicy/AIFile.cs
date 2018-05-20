using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using WPFLocalizeExtension.Extensions;

namespace AipolicyEditor.AIPolicy
{
    public partial class AIFile : INotifyPropertyChanged
    {
        private string path = "";
        public bool InAnotherThread = true;
        public byte[] Header { get; set; }
        private ObservableCollection<CPolicyData> _Controllers = new ObservableCollection<CPolicyData>();
        public ObservableCollection<CPolicyData> Controllers
        {
            get
            {
                return _Controllers;
            }
            set
            {
                if (_Controllers != value)
                {
                    _Controllers = value;
                    OnPropertyChanged("Controllers");
                }
            }
        }

        private int _ControllerIndex = -1;
        public int ControllerIndex
        {
            get
            {
                return _ControllerIndex;
            }
            set
            {
                if (_ControllerIndex != value)
                {
                    _ControllerIndex = value;
                    TriggerIndex = -1;
                    OperationIndex = -1;
                    OnPropertyChanged("ControllerIndex");
                    OnPropertyChanged("CurrentTriggers");
                    OnPropertyChanged("CurrentController");
                    OnPropertyChanged("TriggersHeader");
                }
            }
        }

        private int _TriggerIndex = -1;
        public int TriggerIndex
        {
            get
            {
                return _TriggerIndex;
            }
            set
            {
                if (_TriggerIndex != value)
                {
                    _TriggerIndex = value;
                    OperationIndex = -1;
                    OnPropertyChanged("TriggerIndex");
                    OnPropertyChanged("CurrentTrigger");
                    OnPropertyChanged("CurrentOperations");
                    OnPropertyChanged("OperationsHeader");
                }
            }
        }

        private int _OperationIndex = -1;
        public int OperationIndex
        {
            get
            {
                return _OperationIndex;
            }
            set
            {
                if (_OperationIndex != value)
                {
                    _OperationIndex = value;
                    OnPropertyChanged("OperationIndex");
                    OnPropertyChanged("CurrentOperation");
                }
            }
        }

        public CPolicyData CurrentController
        {
            get
            {
                if (ControllerIndex > -1)
                    return Controllers[ControllerIndex];
                else
                    return null;
            }
            set
            {
                if (ControllerIndex > -1 && Controllers[ControllerIndex] != value)
                    Controllers[ControllerIndex] = value;
            }
        }

        public ObservableCollection<CTriggerData> CurrentTriggers
        {
            get
            {
                if (ControllerIndex > -1)
                    return Controllers[ControllerIndex].Triggers;
                else
                    return null;
            }
            set
            {
                if (ControllerIndex > -1 && Controllers[ControllerIndex].Triggers != value)
                    Controllers[ControllerIndex].Triggers = value;
            }
        }

        public CTriggerData CurrentTrigger
        {
            get
            {
                if (ControllerIndex > -1 && TriggerIndex > -1)
                    return Controllers[ControllerIndex].Triggers[TriggerIndex];
                else
                    return null;
            }
            set
            {
                if (ControllerIndex > -1 && TriggerIndex > -1 && Controllers[ControllerIndex].Triggers[TriggerIndex] != value)
                    Controllers[ControllerIndex].Triggers[TriggerIndex] = value;
            }
        }

        public ObservableCollection<IOperation> CurrentOperations
        {
            get
            {
                if (ControllerIndex > -1 && TriggerIndex > -1)
                    return Controllers[ControllerIndex].Triggers[TriggerIndex].Operations;
                else
                    return null;
            }
            set
            {
                if (ControllerIndex > -1 && TriggerIndex > -1 && Controllers[ControllerIndex].Triggers[TriggerIndex].Operations != value)
                    Controllers[ControllerIndex].Triggers[TriggerIndex].Operations = value;
            }
        }

        public IOperation CurrentOperation
        {
            get
            {
                if (TriggerIndex > -1 && OperationIndex > -1)
                    return Controllers[ControllerIndex].Triggers[TriggerIndex].Operations[OperationIndex];
                else
                    return null;
            }
            set
            {
                if (TriggerIndex > -1 && OperationIndex > -1 && Controllers[ControllerIndex].Triggers[TriggerIndex].Operations[OperationIndex] != value)
                    Controllers[ControllerIndex].Triggers[TriggerIndex].Operations[OperationIndex] = value;
            }
        }

        public void Read(string path)
        {
            this.path = path;
            if (InAnotherThread)
                new Thread(() => _Read(path)).Start();
            else
                _Read(path);
        }

        public void Save()
        {
            _Save(path);
        }

        public void SaveAs(string path)
        {
            _Save(path);
        }

        private void _Read(string path)
        {
            CPolicyData.MaxVersion = 0;
            CTriggerData.MaxVersion = 0;
            BinaryReader br = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));
            Header = br.ReadBytes(4);
            int count = br.ReadInt32();
            ObservableCollection<CPolicyData> data = new ObservableCollection<CPolicyData>();
            for (int i = 0; i < count; ++i)
            {
                CPolicyData cpd = new CPolicyData();
                cpd.Read(br);
                data.Add(cpd);
            }
            _Controllers = new ObservableCollection<CPolicyData>(data);
            br.Close();
            OnPropertyChanged("Controllers");
        }

        private void _Save(string path)
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write));
            bw.Write(Header);
            bw.Write(Controllers.Count);
            for (int i = 0; i < Controllers.Count; ++i)
            {
                Controllers[i].Write(bw);
            }
            bw.Close();
            Utils.ShowMessage(MainWindow.Provider.GetLocalizedString("FileSaved"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
