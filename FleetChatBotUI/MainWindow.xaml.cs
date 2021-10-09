using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommonServiceLocator;
using FleetChatBotUI.ViewModels;

namespace FleetChatBotUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool autoScroll = true;

        public MainWindow()
        {
            InitializeComponent();

            chatTextBox.GotFocus += TxtChatActivities_GotFocus;
            chatTextBox.LostFocus += TxtChatActivities_LostFocus;
            EventManager.RegisterClassHandler(typeof(TextBox),
                TextBox.KeyUpEvent,
                new System.Windows.Input.KeyEventHandler(TextBox_KeyUp));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            (DataContext as MainWindowViewModel).SendMessage();

            e.Handled = true;
        }

        private void TxtChatActivities_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(chatTextBox.Text))
                (DataContext as MainWindowViewModel).UserMessage = "Talk with Hexagon";
        }

        private void TxtChatActivities_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((DataContext as MainWindowViewModel).UserMessage == "Talk with Hexagon")
            {
                (DataContext as MainWindowViewModel).UserMessage = "";
            }
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindowViewModel).StartRecordingCommand.Execute(null);
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindowViewModel).StopRecordingCommand.ExecuteAsync();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // User scroll event : set or unset auto-scroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (chatScrollViewer.VerticalOffset == chatScrollViewer.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set auto-scroll mode
                    autoScroll = true;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset auto-scroll mode
                    autoScroll = false;
                }
            }

            // Content scroll event : auto-scroll eventually
            if (autoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and auto-scroll mode set
                // Autoscroll
                chatScrollViewer.ScrollToVerticalOffset(chatScrollViewer.ExtentHeight);
            }
        }
    }
}