﻿using System.Collections.Generic;
using System.Linq;

namespace SlimMessageBus.Core
{
    /// <summary>
    /// Implementation of <see cref="ISubscriberResolver"/> that delegates the resolve operation to a chain of other <see cref="ISubscriberResolver"/>-s.
    /// The resolvers are controbuting to the list in the order of the chain (e.g first resolver-s handlers are first on the resolved list).
    /// </summary>
    public class SubscriberResolverChain : ISubscriberResolver
    {
        private readonly IList<ISubscriberResolver> _chain = new List<ISubscriberResolver>();

        public void Add(ISubscriberResolver resolver)
        {
            _chain.Add(resolver);
        }

        #region Implementation of IHandlerResolver

        public IEnumerable<ISubscriber<TMessage>> Resolve<TMessage>()
        {
            IEnumerable<ISubscriber<TMessage>> allResolvers = null;

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var resolver in _chain)
            {
                var handlers = resolver.Resolve<TMessage>();
                allResolvers = allResolvers?.Concat(handlers) ?? handlers;
            }

            return allResolvers;
        }

        #endregion
    }
}