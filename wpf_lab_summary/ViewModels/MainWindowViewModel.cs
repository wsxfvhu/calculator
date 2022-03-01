using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_lab_summary.Models;

namespace wpf_lab_summary.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public MainWindowViewModel()
        {
            field = "0";
            operationField = "";

            OneButton = new RelayCommand(OnOneButtonExecute);
            TwoButton = new RelayCommand(OnTwoButtonExecute);
            ThreeButton = new RelayCommand(OnThreeButtonExecute);
            FourButton = new RelayCommand(OnFourButtonExecute);
            FiveButton = new RelayCommand(OnFiveButtonExecute);
            SixButton = new RelayCommand(OnSixButtonExecute);
            SevenButton = new RelayCommand(OnSevenButtonExecute);
            EightButton = new RelayCommand(OnEightButtonExecute);
            NineButton = new RelayCommand(OnNineButtonExecute);
            ZeroButton = new RelayCommand(OnZeroButtonExecute);

            PlusButton = new RelayCommand(OnPlusButtonExecute);
            MinusButton = new RelayCommand(OnMinusButtonExecute);
            DivideButton = new RelayCommand(OnDivideButtonExecute);
            MultiplyButton = new RelayCommand(OnMultiplyButtonExecute);
            ClearButton = new RelayCommand(OnClearButtonExecute);
            EqualButton = new RelayCommand(OnEqualButtonExecute);

            BackspaceButton = new RelayCommand(OnBackspaceButtonExecute);
            CleanEntryButton = new RelayCommand(OnCleanEntryButtonExecute);
            ChangeSignButton = new RelayCommand(OnChangeSignButtonExecute);
            CommaButton = new RelayCommand(OnCommaButtonExecute);
        }

        #region Свойства        
        private string field;
        public string Field
        {
            get => field;
            set
            {
                field = value;
                OnPropertyChanged();
            }
        }

        //Состояние строки операции
        private string operationField;
        public string OperationField
        {
            get => operationField;
            set
            {
                operationField = value;
                OnPropertyChanged();
            }
        }

        //Первый операнд
        private double operand1;
        public double Operand1
        {
            get => operand1;
            set
            {
                operand1 = value;
                OnPropertyChanged();
            }
        }

        //Второй операнд
        private double operand2;
        public double Operand2
        {
            get => operand2;
            set
            {
                operand2 = value;
                OnPropertyChanged();
            }
        }

        //Типы операций
        public enum Operations
        {
            Empty, Add, Sub, Div, Mult
        }

        //Текущая операция
        private Operations operation;
        public Operations Operation
        {
            get => operation;
            set
            {
                operation = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Команды для ввода значений
        public ICommand OneButton { get; }
        private void OnOneButtonExecute(object p)
        {
            InputSymbol("1");
        }

        public ICommand TwoButton { get; }
        private void OnTwoButtonExecute(object p)
        {
            InputSymbol("2");
        }

        public ICommand ThreeButton { get; }
        private void OnThreeButtonExecute(object p)
        {
            InputSymbol("3");
        }

        public ICommand FourButton { get; }
        private void OnFourButtonExecute(object p)
        {
            InputSymbol("4");
        }

        public ICommand FiveButton { get; }
        private void OnFiveButtonExecute(object p)
        {
            InputSymbol("5");
        }

        public ICommand SixButton { get; }
        private void OnSixButtonExecute(object p)
        {
            InputSymbol("6");
        }

        public ICommand SevenButton { get; }
        private void OnSevenButtonExecute(object p)
        {
            InputSymbol("7");
        }

        public ICommand EightButton { get; }
        private void OnEightButtonExecute(object p)
        {
            InputSymbol("8");
        }

        public ICommand NineButton { get; }
        private void OnNineButtonExecute(object p)
        {
            InputSymbol("9");
        }

        public ICommand ZeroButton { get; }
        private void OnZeroButtonExecute(object p)
        {
            InputSymbol("0");
        }

        public ICommand CommaButton { get; }
        private void OnCommaButtonExecute(object p)
        {
            InputSymbol(",");
        }

        public ICommand BackspaceButton { get; }
        private void OnBackspaceButtonExecute(object p)
        {
            Backspace();
        }

        public ICommand CleanEntryButton { get; }
        private void OnCleanEntryButtonExecute(object p)
        {
            CleanEntry();
        }

        public ICommand ChangeSignButton { get; }
        private void OnChangeSignButtonExecute(object p)
        {
            ChangeSign();
        }
        #endregion

        #region Команды для расчета
        public ICommand ClearButton { get; }
        private void OnClearButtonExecute(object p)
        {
            Clear();
        }

        public ICommand PlusButton { get; }
        private void OnPlusButtonExecute(object p)
        {
            Input(Operations.Add);
        }

        public ICommand MinusButton { get; }
        private void OnMinusButtonExecute(object p)
        {
            Input(Operations.Sub);
        }

        public ICommand DivideButton { get; }
        private void OnDivideButtonExecute(object p)
        {
            Input(Operations.Div);
        }

        public ICommand MultiplyButton { get; }
        private void OnMultiplyButtonExecute(object p)
        {
            Input(Operations.Mult);
        }

        public ICommand EqualButton { get; }
        private void OnEqualButtonExecute(object p)
        {
            Equal();
        }

        #endregion

        #region Калькулятор
        //Строка ввода
        void InputSymbol(string symbol)
        {
            if (Field.Replace(" ", "").Replace(",", "").Replace("-", "").Length >= 15)
            {
                return;
            }

            if (OperationField.Contains("="))
            {
                Clear();
                if (symbol != ",")
                {
                    Field = symbol;
                }
                else
                {
                    Field += symbol;
                }

            }
            else if (Field == "0" && symbol != ",")
            {
                Field = symbol;
            }
            else if (Field.Contains(",") && symbol == ",")
            {
                return;
            }
            else if (symbol == ",")
            {
                Field += symbol;
            }
            else
            {
                Field += symbol;
                Field = Ariph.Format(Field, false);
            }
        }

        //Удаление последнего символа
        void Backspace()
        {
            if (Field == "Ошибка!")
            {
                Clear();
                return;
            }
            if (Field.Length == 1 ||
               (Field.Length == 2 && Field.Substring(0, 1) == "-"))
            {
                Field = "0";
                return;
            }

            else
            {
                Field = Ariph.Format(Field.Remove(Field.Length - 1), false);
            }

            if (OperationField.Contains("="))
            {
                string temp = Field;
                Clear();
                Field = temp;
            }
        }

        //Очистка поля ввода
        void CleanEntry()
        {
            if (OperationField.Contains("="))
            {
                Clear();
            }

            Field = "0";
        }

        //Смена знака
        void ChangeSign()
        {
            Field = Ariph.Format(Field, true);
        }

        //Очистка данных
        void Clear()
        {
            Operand1 = 0;
            Operand2 = 0;
            Field = "0";
            Operation = Operations.Empty;
            OperationField = "";
        }

        //Операция
        void Input(Operations operation)
        {
            if (Field == "Ошибка!")
            {
                Clear();
                return;
            }

            string result;
            string opSign = "";
            switch (operation)
            {
                case Operations.Empty:
                    return;
                case Operations.Add:
                    opSign = "+";
                    break;
                case Operations.Sub:
                    opSign = "-";
                    break;
                case Operations.Div:
                    opSign = "÷";
                    break;
                case Operations.Mult:
                    opSign = "×";
                    break;
            }

            if (Operation != Operations.Empty)
            {
                result = Ariph.Calculate(Operand1, double.Parse(Field), Operation);
                Operand1 = result != "Ошибка!" ? double.Parse(result) : 0;
            }
            else
            {
                Operand1 = double.Parse(Field);
            }

            OperationField = Ariph.Format($"{Operand1}", false) + $" {opSign}";
            Operation = operation;
            CleanEntry();
        }

        //Результат
        void Equal()
        {
            if (Field == "Ошибка!")
            {
                Clear();
                return;
            }
            if (Operation == Operations.Empty)
            {
                return;
            }

            Operand2 = double.Parse(Field);
            OperationField += " " + Ariph.Format($"{Operand2}", false) + " =";
            Field = Ariph.Calculate(Operand1, Operand2, Operation);
            Operation = Operations.Empty;
        }
        #endregion
    }
}
