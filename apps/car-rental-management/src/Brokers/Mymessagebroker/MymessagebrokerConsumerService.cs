using CarRentalManagement.Brokers.Infrastructure;
using CarRentalManagement.Brokers.Mymessagebroker;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalManagement.Brokers.Mymessagebroker;

public class MymessagebrokerConsumerService
    : KafkaConsumerService<MymessagebrokerMessageHandlersController>
{
    public MymessagebrokerConsumerService(
        IServiceScopeFactory serviceScopeFactory,
        KafkaOptions kafkaOptions
    )
        : base(serviceScopeFactory, kafkaOptions) { }
}
