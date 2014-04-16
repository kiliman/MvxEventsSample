using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace MvxEventsSample.Core.Services
{
    public interface IMyService
    {
        void Logon();
        void DoSomething();
    }

    public class MyService : IMyService
    {
        private readonly IMvxMessenger _messenger;

        public MyService(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        public void Logon()
        {
            _messenger.Publish(new LogonChangedMessage(this));
        }

        public void DoSomething()
        {
            _messenger.Publish(new AnotherEventMessage(this, string.Format("Do Something at {0}", DateTime.Now)));
        }

        public class Events
        {
            private readonly IMvxMessenger _messenger;

            public Events(IMvxMessenger messenger)
            {
                _messenger = messenger;
            }

            private MvxSubscriptionToken _logonChangedToken;
            public Action<LogonChangedMessage> OnLogonChanged
            {
                set { _logonChangedToken = _messenger.Subscribe(value); } 
            }

            private MvxSubscriptionToken _anotherEventToken;
            public Action<AnotherEventMessage> OnAnotherEvent
            {
                set { _anotherEventToken = _messenger.Subscribe(value); }
            }
        }

        public class LogonChangedMessage : MvxMessage
        {
            public LogonChangedMessage(object sender) : base(sender)
            {
            }
        }

        public class AnotherEventMessage : MvxMessage
        {
            public AnotherEventMessage(object sender, string text) : base(sender)
            {
                Text = text;
            }

            public string Text { get; set; }
        }
    }
}
