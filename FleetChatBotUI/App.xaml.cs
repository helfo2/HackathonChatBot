using System.Windows;
using CommonServiceLocator;
using DeepSpeechClient.Interfaces;
using FleetChatBotServer.ChatBot;
using FleetChatBotServer.Infrastructure.Commons.HttpConnection;
using FleetChatBotUI.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using Hxgn.Mop.UG.Simulator.Infrastructure.Commons.Configuration;
using Serilog;

namespace FleetChatBotUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File(InfraConfiguration.Instance().LogRelativePath)
                    .CreateLogger();

                //Register instance of DeepSpeech
                DeepSpeechClient.DeepSpeech deepSpeechClient =
                    new DeepSpeechClient.DeepSpeech(InfraConfiguration.Instance().ChatBot.DeepSpeechModelRelativePath);

                SimpleIoc.Default.Register<IDeepSpeech>(() => deepSpeechClient);
                SimpleIoc.Default.Register<IHttpClientCollection, HttpClientCollection>();

                IHttpClientCollection httpClients = ServiceLocator.Current.GetInstance<IHttpClientCollection>();
                ChatBot chatbot = new ChatBot(httpClients);

                SimpleIoc.Default.Register<IChatBot>(() => chatbot);
                SimpleIoc.Default.Register<MainWindowViewModel>();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                Current.Shutdown();
            }
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            //Dispose instance of DeepSpeech
            ServiceLocator.Current.GetInstance<IDeepSpeech>()?.Dispose();
        }
    }
}
