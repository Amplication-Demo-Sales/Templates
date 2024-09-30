using CrmManagement.Brokers.Infrastructure;

namespace CrmManagement.Brokers.Mymessagebroker;

public class MymessagebrokerProducerService : InternalProducer
{
    public MymessagebrokerProducerService(string bootstrapServers)
        : base(bootstrapServers) { }
}
