using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFLocalizeExtension.Extensions;

namespace AipolicyEditor.AIPolicy.Conditions
{
    public static class Conditions
    {
        public static int[] Non_central
        {
            get => new int[] { 0, 1, 2, 3, 4, 8, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };
        }

        public static Condition CreateEmpty() => new Condition()
        {
            ID = 2,
            Type = 3,
            Value = new object[0]
        };

        public static object[] CreateEmptyValue(int type)
        {
            switch (type)
            {
                case 0:
                case 1:
                case 3:
                case 16:
                case 17:
                case 19:
                case 20:
                case 21:
                case 23:
                case 25:
                case 27:
                    return new object[] { 0 };
                case 18:
                case 24:
                    return new object[] { 0, 0 };
                case 28:
                    return new object[] { "0 0 0", "0 0 0" };
                default:
                    return new object[0];
            }
        }

        public static int TypeByID(int id)
        {
            switch (id)
            {
                case 13:
                case 12:
                case 11:
                case 9:
                case 15:
                case 14:
                case 6:
                case 7:
                case 10:
                    return 1;
                case 5:
                    return 2;
                case 2:
                case 3:
                case 1:
                case 0:
                case 4:
                case 16:
                case 17:
                case 8:
                case 22:
                case 18:
                case 23:
                case 21:
                case 19:
                case 20:
                case 26:
                case 25:
                case 24:
                case 27:
                case 28:
                    return 3;
                default:
                    return 1;
            }
        }

        public static string GetName(int id)
        {
            switch (id)
            {
                case 5:
                    return Settings.ConditionType == 0 ? "!" : MainWindow.Provider.GetLocalizedString("c5");
                case 6:
                    return Settings.ConditionType == 0 ? "||" : MainWindow.Provider.GetLocalizedString("c6");
                case 7:
                    return Settings.ConditionType == 0 ? "&&" : MainWindow.Provider.GetLocalizedString("c7");
                case 9:
                    return Settings.ConditionType == 0 ? "+" : MainWindow.Provider.GetLocalizedString("c9");
                case 10:
                    return Settings.ConditionType == 0 ? "-" : MainWindow.Provider.GetLocalizedString("c10");
                case 11:
                    return Settings.ConditionType == 0 ? "*" : MainWindow.Provider.GetLocalizedString("c11");
                case 12:
                    return Settings.ConditionType == 0 ? "÷" : MainWindow.Provider.GetLocalizedString("c12");
                case 13:
                    return Settings.ConditionType == 0 ? ">" : MainWindow.Provider.GetLocalizedString("c13");
                case 14:
                    return Settings.ConditionType == 0 ? "<" : MainWindow.Provider.GetLocalizedString("c14");
                case 15:
                    return Settings.ConditionType == 0 ? "==" : MainWindow.Provider.GetLocalizedString("c15");
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 8:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                    return MainWindow.Provider.GetLocalizedString($"c{id}");
                default:
                    return "UNKNOWN";
            }
        }

        public static string[] GetFileds(int Type)
        {
            switch (Type)
            {
                case 0:
                    return new string[] { "ID" };
                case 1:
                    return new string[] { "Percent" };
                case 3:
                    return new string[] { "Probability" };
                case 2:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 22:
                case 26:
                    return new string[0];
                case 16:
                    return new string[] { "ID" };
                case 17:
                    return new string[] { "Value" };
                case 18:
                    return new string[] { "min_damage", "max_damage" };
                case 19:
                    return new string[] { "PathID" };
                case 20:
                    return new string[] { "ID" };
                case 21:
                    return new string[] { "Value" };
                case 23:
                    return new string[] { "ID" };
                case 24:
                    return new string[] { "PathID", "PathIDType" };
                case 25:
                    return new string[] { "ID" };
                case 27:
                    return new string[] { "Radius" };
                case 28:
                    return new string[] { "Min", "Max" };
                default:
                    Utils.ShowMessage($"Unknown condition type: {Type}");
                    return new string[0];
            }
        }

        public static (int, int) GetSubNodeByType(int type)
        {
            switch (type)
            {
                case 1:
                    return (2, 4);
                case 2:
                    return (4, 0);
                default:
                    return (0, 0);
            }
        }

        public static object[] Read(byte[] data, int type)
        {
            byte[] val1 = new byte[4];
            byte[] val2 = new byte[4];
            switch (type)
            {
                case 1:
                case 3:
                case 27:
                    Array.Copy(data, val1, 4);
                    return new object[] { BitConverter.ToSingle(val1, 0) };
                case 0:
                case 16:
                case 17:
                case 19:
                case 20:
                case 21:
                case 23:
                case 25:
                    Array.Copy(data, val1, 4);
                    return new object[] { BitConverter.ToInt32(val1, 0) };
                case 18:
                case 24:
                    Array.Copy(data, val1, 4);
                    Array.Copy(data, 4, val2, 0, 4);
                    return new object[] { BitConverter.ToInt32(val1, 0), BitConverter.ToInt32(val2, 0) };
                case 28:
                    return new object[]
                    {
                        $"{BitConverter.ToSingle(data, 0)} {BitConverter.ToSingle(data, 4)} {BitConverter.ToSingle(data, 8)}",
                        $"{BitConverter.ToSingle(data, 12)} {BitConverter.ToSingle(data, 16)} {BitConverter.ToSingle(data, 20)}"
                    };
                default:
                    return new object[0];
            }
        }

        public static byte[] Write(object[] param, int type)
        {
            byte[] data;
            Array.ForEach(param, x => x.ToString().Replace(".", ","));
            //if (param.Length > 0) param[0] = param[0]?.ToString().Replace('.', ',');
            //if (param.Length > 1) param[1] = param[1]?.ToString().Replace('.', ',');
            switch (type)
            {
                case 1:
                case 3:
                case 27:
                    return BitConverter.GetBytes(Convert.ToSingle(param[0]));
                case 0:
                case 16:
                case 17:
                case 19:
                case 20:
                case 21:
                case 23:
                case 25:
                    return BitConverter.GetBytes(Convert.ToInt32(param[0]));
                case 18:
                case 24:
                    data = new byte[8];
                    Array.Copy(BitConverter.GetBytes(Convert.ToInt32(param[0])), data, 4);
                    Array.Copy(BitConverter.GetBytes(Convert.ToInt32(param[1])), 0, data, 4, 4);
                    return data;
                case 28:
                    data = new byte[24];
                    string[] p1 = param[0].ToString().Split(' ');
                    string[] p2 = param[1].ToString().Split(' ');
                    Array.Copy(BitConverter.GetBytes(Convert.ToSingle(p1[0])), data, 4);
                    Array.Copy(BitConverter.GetBytes(Convert.ToSingle(p1[1])), 0, data, 4, 4);
                    Array.Copy(BitConverter.GetBytes(Convert.ToSingle(p1[2])), 0, data, 8, 4);
                    Array.Copy(BitConverter.GetBytes(Convert.ToSingle(p2[0])), 0, data, 12, 4);
                    Array.Copy(BitConverter.GetBytes(Convert.ToSingle(p2[1])), 0, data, 16, 4);
                    Array.Copy(BitConverter.GetBytes(Convert.ToSingle(p2[2])), 0, data, 20, 4);
                    return data;
                default:
                    return new byte[0];
            }
        }

        public static bool Search(Condition c, string str)
        {
            if (c.Value.Where(x => x.ToString().Contains(str)).Count() > 0)
                return true;
            if (c.ConditionLeft != null)
                if (Search(c.ConditionLeft, str))
                    return true;
            if (c.ConditionRight != null)
                if (Search(c.ConditionRight, str))
                    return true;
            return false;
        }

        public static bool Search(Condition c, int id)
        {
            if (c.ID == id)
                return true;
            if (c.ConditionLeft != null)
                if (Search(c.ConditionLeft, id))
                    return true;
            if (c.ConditionRight != null)
                if (Search(c.ConditionRight, id))
                    return true;
            return false;
        }

        public static List<string> GetConditions()
        {
            var list = new List<string>();
            int count = 29;
            if (CTriggerData.MaxVersion <= 1)
                count = 9;
            else if (CTriggerData.MaxVersion <= 9)
                count = 19;
            else if (CTriggerData.MaxVersion <= 10)
                count = 20;
            else if (CTriggerData.MaxVersion <= 14)
                count = 23;
            else if (CTriggerData.MaxVersion <= 20)
                count = 26;
            else if (CTriggerData.MaxVersion <= 22)
                count = 27;
            for (int i = 0; i < count; ++i)
                list.Add(GetName(i));
            return list;
        }
    }
}
