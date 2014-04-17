using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace MvxEventsSample.Core.Services
{
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

        public class Events : EventsBase
        {
            public Events(IMvxMessenger messenger) : base(messenger)
            {
            }

            private MvxSubscriptionToken _logonChangedToken;
            public Action<LogonChangedMessage> OnLogonChanged
            {
                set { SetHandler(value, ref _logonChangedToken); } 
            }

            private MvxSubscriptionToken _anotherEventToken;
            public Action<AnotherEventMessage> OnAnotherEvent
            {
                set { SetHandler(value, ref _anotherEventToken); }
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
