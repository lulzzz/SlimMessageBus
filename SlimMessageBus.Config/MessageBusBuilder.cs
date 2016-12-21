using System;

namespace SlimMessageBus.Config
{
    public class MessageBusBuilder
    {
        private readonly MessageBusSettings _settings = new MessageBusSettings();
        private Func<MessageBusSettings, IMessageBus> _factory; 

        public MessageBusBuilder Publish<T>(Action<PublisherBuilder<T>> publisherBuilder)
        {
            var item = new PublisherSettings();
            publisherBuilder(new PublisherBuilder<T>(item));
            _settings.Publishers.Add(item);
            return this;
        }

        public MessageBusBuilder SubscribeTo<T>(Action<SubscriberBuilder<T>> subscriberBuilder)
        {
            var item = new SubscriberSettings();
            subscriberBuilder(new SubscriberBuilder<T>(item));
            _settings.Subscribers.Add(item);
            return this;
        }

        public MessageBusBuilder ExpectRequestResponses(Action<RequestResponseBuilder> reqRespBuilder)
        {
            var item = new RequestResponseSettings();
            reqRespBuilder(new RequestResponseBuilder(item));
            _settings.RequestResponse = item;
            return this;
        }

        /*
        public MessageBusBuilder WithGroup(Action<GroupBuilder> groupBuilder)
        {
            _group = new GroupSettings();
            groupBuilder(new GroupBuilder(_group));
            return this;
        }
        */

        public MessageBusBuilder WithSerializer(IMessageSerializer serializer)
        {
            _settings.Serializer = serializer;
            return this;
        }

        public MessageBusBuilder WithProvider(Func<MessageBusSettings, IMessageBus> provider)
        {
            _factory = provider;
            return this;
        }

        public IMessageBus Build()
        {
            return _factory(_settings);
        }
    }
}