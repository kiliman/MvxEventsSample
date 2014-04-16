using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using MvxEventsSample.Core.Services;

namespace MvxEventsSample.Core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        private readonly IMyService _myService;
        private readonly MyService.Events _events;
        private int _counter = 0;

        public FirstViewModel(IMyService myService, IMvxMessenger messenger)
        {
            _myService = myService;

            // Initialize event handlers using lambda expressions
            _events = new MyService.Events(messenger)
            {
                OnLogonChanged = message =>
                {
                    Hello = "OnLogonChanged: " + (++_counter);
                    Mvx.Trace("OnLogonChanged");
                },
                OnAnotherEvent = message =>
                {
                    Hello = message.Text;
                    Mvx.Trace("OnAnotherEvent");
                }
            };
        }

        private string _hello;
        public string Hello
        {
            get { return _hello; }
            set { _hello = value; RaisePropertyChanged(() => Hello); }
        }


        private MvxCommand _logonCommand;
        public ICommand LogonCommand
        {
            get
            {
                _logonCommand = _logonCommand ?? new MvxCommand(_myService.Logon);
                return _logonCommand;
            }
        }

        private MvxCommand _doSomethingCommand;
        public ICommand DoSomethingCommand
        {
            get
            {
                _doSomethingCommand = _doSomethingCommand ?? new MvxCommand(_myService.DoSomething);
                return _doSomethingCommand;
            }
        }
    }
}
