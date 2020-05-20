using AgeCal.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgeCal.Services
{
    public class AppMessagingCenter : IAppMessagingCenter
    {
        private readonly object mutex = new object();
        private IDictionary<Type, IDictionary<object, IList>> subscribers;
        private IDictionary<Type, Queue<object>> pendingMessage;
        public AppMessagingCenter()
        {
            subscribers = new Dictionary<Type, IDictionary<object, IList>>();
            pendingMessage = new Dictionary<Type, Queue<object>>();
        }
        public void Register<TMessage>(object subscriber, Action<TMessage> action)
        {
            if (subscriber == null || action == null)
                return;
            lock (mutex)
            {
                var messageType = typeof(TMessage);
                if (!subscribers.ContainsKey(messageType))
                {
                    subscribers[messageType] = new Dictionary<object, IList>();
                }
                var activeSubscribers = subscribers[messageType];
                if (!activeSubscribers.ContainsKey(subscriber))
                {
                    activeSubscribers[subscriber] = new List<Action<TMessage>>();
                }
                //register callback
                activeSubscribers[subscriber].Add(action);
            }

            //Send any  messages that are pending
            var messages = DequeuePendingMessage<TMessage>();
            foreach (TMessage message in messages)
                Send(message);
        }

        public bool Send<TMessage>(TMessage msg)
        {
            var messageType = typeof(TMessage);
            lock (mutex)
            {
                IDictionary<object, IList> activeSuscribers;
                if (!subscribers.TryGetValue(messageType, out activeSuscribers))
                {
                    //Nobody yet suscribe late's regain message 
                    QueuePendingMessage(msg);
                    return false;

                }
                foreach (var suscriber in activeSuscribers.ToList())
                {
                    foreach (var callback in suscriber.Value)
                    {
                        var action = (Action<TMessage>)callback;
                        action(msg);

                    }
                }
            }
            return true;
        }

        public void Unregister<TMessage>(object subscriber)
        {
            if (subscriber == null)
                return;
            lock (mutex)
            {
                var messageType = typeof(TMessage);

                IDictionary<object, IList> activeSuscribers;
                if (!subscribers.TryGetValue(messageType, out activeSuscribers))
                    return;
                if (activeSuscribers.ContainsKey(subscriber))
                    activeSuscribers.Remove(subscriber);
            }
        }

        public void Unregister(object subscriber)
        {
            if (subscriber == null)
                return;
            lock (mutex)
            {

                foreach (var suscriberByType in subscribers)
                {
                    if (suscriberByType.Value.ContainsKey(subscriber))
                        suscriberByType.Value.Remove(subscriber);
                }

            }
        }

        private void QueuePendingMessage<TMessage>(TMessage msg)
        {
            var messageType = typeof(TMessage);
            if (!pendingMessage.ContainsKey(messageType))
            {
                pendingMessage[messageType] = new Queue<object>();
            }
            pendingMessage[messageType].Enqueue(msg);
        }
        private IEnumerable<TMessage> DequeuePendingMessage<TMessage>()
        {
            var messageType = typeof(TMessage);
            Queue<object> activePendingMessgae;
            if (!pendingMessage.TryGetValue(messageType, out activePendingMessgae))
            {
                return Enumerable.Empty<TMessage>();

            }
            var message = activePendingMessgae.Select(msg => (TMessage)msg);
            pendingMessage.Remove(messageType);

            return message;
        }
    }
}
