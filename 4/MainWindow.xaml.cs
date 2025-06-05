using System;
using System.Windows;
using System.ComponentModel;

namespace GuessNumberGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckGuess(object sender, RoutedEventArgs e)
        {
            if (DataContext is GameViewModel game)
            {
                game.CheckGuess();
            }
        }

        private void ResetGame(object sender, RoutedEventArgs e)
        {
            if (DataContext is GameViewModel game)
            {
                game.ResetGame();
            }
        }
    }
    public class GameViewModel : INotifyPropertyChanged
    {
        private int _targetNumber;
        private int _attemptCount;
        private string _message;
        private string _userInput;

        public event PropertyChangedEventHandler PropertyChanged;

        public GameViewModel()
        {
            ResetGame();
        }

        public int AttemptCount
        {
            get => _attemptCount;
            set
            {
                _attemptCount = value;
                OnPropertyChanged(nameof(AttemptCount));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public string UserInput
        {
            get => _userInput;
            set
            {
                _userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }
        }

        public void CheckGuess()
        {
            if (int.TryParse(UserInput, out int guess))
            {
                if (guess < 1 || guess > 100)
                {
                    Message = "Введите число от 1 до 100.";
                    return;
                }

                AttemptCount++;

                if (guess < _targetNumber)
                {
                    Message = "Слишком маленькое.";
                }
                else if (guess > _targetNumber)
                {
                    Message = "Слишком большое.";
                }
                else
                {
                    Message = $"Поздравляем! Вы угадали за {AttemptCount} попыток. Может ещё раз?";
                }
            }
            else
            {
                Message = "Введите допустимое число.";
            }
        }

        public void ResetGame()
        {
            Random rand = new Random();
            _targetNumber = rand.Next(1, 101);
            AttemptCount = 0;
            Message = "Okay let'go gambling!";
            UserInput = string.Empty;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
