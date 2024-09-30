using CrmManagement.Brokers.Infrastructure;
using CrmManagement.Brokers.Mymessagebroker;
using Microsoft.Extensions.DependencyInjection;

namespace CrmManagement.Brokers.Mymessagebroker;

public class MymessagebrokerConsumerService
    : KafkaConsumerService<MymessagebrokerMessageHandlersController>
{
    public MymessagebrokerConsumerService(
        IServiceScopeFactory serviceScopeFactory,
        KafkaOptions kafkaOptions
    )
        : base(serviceScopeFactory, kafkaOptions) { }
}
