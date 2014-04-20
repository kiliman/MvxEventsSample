using System;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace MvxEventsSample.Core.Services
{
    public class EventsBase
    {
        private readonly IMvxMessenger _messenger;

        public EventsBase(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        protected void SetHandler<TMessage>(Action<TMessage> action, ref MvxSubscriptionToken token) where TMessage : MvxMessage
        {
            if (token != null)
            {
                if (action != null)
                {
                    // already subscribed, so return
                    return;
                }
                // no action so unsubcribe
                _messenger.Unsubscribe<TMessage>(token);
                token = null;
                return;
            }
            //Check that action is not null (Can occur if unsubscribe invoked multiple times without matching subscription)
            if (action != null)
            {
              token = _messenger.Subscribe(action);
            }
        }
    }
}