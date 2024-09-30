using Microsoft.Extensions.DependencyInjection;
using ReservationManagementMobile.Brokers.Infrastructure;
using ReservationManagementMobile.Brokers.Mymessagebroker;

namespace ReservationManagementMobile.Brokers.Mymessagebroker;

public class MymessagebrokerConsumerService
    : KafkaConsumerService<MymessagebrokerMessageHandlersController>
{
    public MymessagebrokerConsumerService(
        IServiceScopeFactory serviceScopeFactory,
        KafkaOptions kafkaOptions
    )
        : base(serviceScopeFactory, kafkaOptions) { }
}
