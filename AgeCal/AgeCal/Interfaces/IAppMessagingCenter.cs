using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface IAppMessagingCenter
    {
        /// <summary>
        /// Send message to all suscribers
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool Send<TMessage>(TMessage msg);
        /// <summary>
        /// Register object to listenn the defined message.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="subscriber"></param>
        /// <param name="action"></param>
        void Register<TMessage>(object subscriber, Action<TMessage> action);
        /// <summary>
        /// Unregister object for events of type TMessage.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="subscriber"></param>
        void Unregister<TMessage>(object subscriber);
        /// <summary>
        /// Unregister object for all events.
        /// </summary>
        /// <param name="subscriber"></param>
        void Unregister(object subscriber);
    }
}
