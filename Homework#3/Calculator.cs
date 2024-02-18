using Avalonia.Controls;
using System;

namespace Calc
{
    public interface ICalculator
    {
        void Init(TextBlock display, TextBlock console);
        void Clear();
        double Addiction(double x, double y);
        double Substraction(double x, double y);
        double Multiplication(double x, double y);
        double Division(double x, double y);
        double Mod(double x, double y);
        double Factorial(double x);
        double Pow(double x, double y);
        double Log(double x);
        double NatLog(double x);
        double Sinus(double x);
        double Cosinus(double x);
        double Tangens(double x);
        double Floor(double x);
        double Ceil(double x);
        void RemoveLastDigit();
        void HandleInput(string input);
        void ExecuteLastOperation();
    }

    public class MyCalculator : ICalculator
    {
        private TextBlock _display;
        private TextBlock _console;
        private string _currentDisplayOperand = "";
        private double _x = 0;
        private double _y = 0;
        private int _condition = 1;
        private double _result = 0;
        private string _lastOperand = "";
        private string _lastOperator = "";

        public void Init(TextBlock display, TextBlock console)
        {
            _display = display;
            _console = console;
            Clear();
        }

        public void Clear()
        {
            _currentDisplayOperand = "";
            _x = 0;
            _y = 0;
            _condition = 1;
            _result = 0;
            _lastOperand = "";
            _lastOperator = "";
            UpdateDisplay();
        }

        public double Addiction(double x, double y)
        {
            _lastOperator = "+";
            return x + y;
        }

        public double Substraction(double x, double y)
        {
            _lastOperator = "-";
            return x - y;
        }

        public double Multiplication(double x, double y)
        {
            _lastOperator = "*";
            return x * y;
        }

        public double Division(double x, double y)
        {
            _lastOperator = "/";
            return x / y;
        }

        public double Mod(double x, double y)
        {
            _lastOperator = "mod";
            return x % y;
        }

        public double Factorial(double x)
        {
            if (x == 0)
                return 1;

            double result = 1;
            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            return result;
        }

        public double Pow(double x, double y)
        {
            _lastOperator = "pow";
            return Math.Pow(x, y);
        }

        public double Log(double x)
        {
            _lastOperator = "log";
            return Math.Log10(x);
        }

        public double NatLog(double x)
        {
            _lastOperator = "ln";
            return Math.Log(x);
        }

        public double Sinus(double x)
        {
            _lastOperator = "sin";
            return Math.Sin(x);
        }

        public double Cosinus(double x)
        {
            _lastOperator = "cos";
            return Math.Cos(x);
        }

        public double Tangens(double x)
        {
            _lastOperator = "tan";
            return Math.Tan(x);
        }

        public double Floor(double x)
        {
            _lastOperator = "floor";
            return Math.Floor(x);
        }

        public double Ceil(double x)
        {
            _lastOperator = "ceil";
            return Math.Ceiling(x);
        }

        public void RemoveLastDigit()
        {
            if (!string.IsNullOrEmpty(_currentDisplayOperand))
            {
                _currentDisplayOperand = _currentDisplayOperand.Remove(_currentDisplayOperand.Length - 1);
                _lastOperand = _currentDisplayOperand;
                UpdateDisplay();
            }
        }

        public void HandleInput(string input)
        {
            if (input == "-" && string.IsNullOrEmpty(_currentDisplayOperand))
            {
                _currentDisplayOperand = "-";
            }
            else if (char.IsDigit(input[0]) || input == ",")
            {
                if (input == "," && !_currentDisplayOperand.Contains(","))
                {
                    _currentDisplayOperand += ",";
                }
                else if (input != ",")
                {
                    _currentDisplayOperand += input;
                }
            }
            else
            {
                if (_condition == 1)
                {
                    if (string.IsNullOrEmpty(_currentDisplayOperand))
                    {
                        if (input == "-")
                        {
                            _currentDisplayOperand = "-";
                        }
                        else
                        {
                            _x = 0;
                        }
                    }
                    else
                    {
                        _x = double.Parse(_currentDisplayOperand);
                        _lastOperand = _currentDisplayOperand;
                        _lastOperator = input;
                        if (input == "+" || input == "-" || input == "*" || input == "/" || input == "x^y" || input == "mod")
                            _condition = 2;
                        _currentDisplayOperand = "";
                    }
                }
                else if (_condition == 2)
                {
                    _y = double.Parse(_currentDisplayOperand);
                    ExecuteLastOperation();
                    _lastOperand = _currentDisplayOperand;
                    _lastOperator = input;
                    _currentDisplayOperand = "";
                }
            }

            UpdateDisplay();
        }



        public void ExecuteLastOperation()
        {
            if (_condition == 2)
            {
                _y = double.Parse(_currentDisplayOperand);
            }

            switch (_lastOperator)
            {
                case "+":
                    _result = Addiction(_x, _y);
                    break;
                case "-":
                    _result = Substraction(_x, _y);
                    break;
                case "*":
                    _result = Multiplication(_x, _y);
                    break;
                case "/":
                    _result = Division(_x, _y);
                    break;
                case "mod":
                    _result = Mod(_x, _y);
                    break;
                case "n!":
                    _result = Factorial(_x);
                    break;
                case "x^y":
                    _result = Pow(_x, _y);
                    break;
                case "log":
                    _result = Log(_x);
                    break;
                case "ln":
                    _result = NatLog(_x);
                    break;
                case "sin":
                    _result = Sinus(_x);
                    break;
                case "cos":
                    _result = Cosinus(_x);
                    break;
                case "tan":
                    _result = Tangens(_x);
                    break;
                case "floor":
                    _result = Floor(_x);
                    break;
                case "ceil":
                    _result = Ceil(_x);
                    break;
                default:
                    break;
            }

            _currentDisplayOperand = _result.ToString();
            _condition = 1;
            _x = _result;
            _lastOperand = Convert.ToString(_result);
            UpdateDisplay();
        }


        private void UpdateDisplay()
        {
            _display.Text = _currentDisplayOperand;
        }
    }
}
