using MediatR;

namespace ProductionStarter.Core.Events;

public record OrderCreatedEvent(Guid OrderId) : INotification;
