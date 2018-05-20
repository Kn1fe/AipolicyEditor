using MahApps.Metro.Controls;
using System;
using System.ComponentModel;
using System.Windows;

namespace AipolicyEditor.AIPolicy.Conditions
{
    public delegate void Reload();

    public partial class ConditionEdit : MetroWindow, INotifyPropertyChanged
    {
        public Condition C { get; set; }

        public event Reload Reload;

        public string Param1Name
        {
            get
            {
                string[] fields = Conditions.GetFileds(C.ID);
                if (fields.Length > 0)
                    return fields[0];
                else
                    return "";
            }
        }

        public string Param2Name
        {
            get
            {
                string[] fields = Conditions.GetFileds(C.ID);
                if (fields.Length > 1)
                    return fields[1];
                else
                    return "";
            }
        }

        public object Param1
        {
            get
            {
                return C.Value.Length > 0 ? C.Value[0] : "";
            }
            set
            {
                if (C.Value.Length > 0 && C.Value[0] != value)
                {
                    C.Value[0] = value;
                    OnPropertyChanged("Param1");
                    Reload?.Invoke();
                }
            }
        }

        public object Param2
        {
            get
            {
                return C.Value.Length > 1 ? C.Value[1] : "";
            }
            set
            {
                if (C.Value.Length > 1 && C.Value[1] != value)
                {
                    C.Value[1] = value;
                    OnPropertyChanged("Param2");
                    Reload?.Invoke();
                }
            }
        }

        public int ConditionIndex
        {
            get => C.ID;
            set
            {
                if (C.ID != value)
                {
                    C.ID = value;
                    C.Value = Conditions.CreateEmptyValue(value);
                    C.Type = Conditions.TypeByID(value);
                    (C.SubNodeL, C.SubNodeR) = Conditions.GetSubNodeByType(C.Type);
                    OnPropertyChanged("Param1Name");
                    OnPropertyChanged("Param2Name");
                    OnPropertyChanged("Param1");
                    OnPropertyChanged("Param2");
                    OnPropertyChanged("ConditionIndex");
                    OnPropertyChanged("Visible1");
                    OnPropertyChanged("Visible2");
                    Reload?.Invoke();
                }
            }
        }

        public Visibility Visible1 => C.Value.Length > 0 ? Visibility.Visible : Visibility.Hidden;

        public Visibility Visible2 => C.Value.Length > 1 ? Visibility.Visible : Visibility.Hidden;

        public ConditionEdit(Condition c)
        {
            InitializeComponent();
            C = c;
            var data = Conditions.GetConditions();
            for (int i = 0; i < data.Count; ++i)
                ConditionBox.Items.Add(data[i]);
            DataContext = this;
            OnPropertyChanged("ConditionIndex");
            Visibility = Visibility.Visible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
