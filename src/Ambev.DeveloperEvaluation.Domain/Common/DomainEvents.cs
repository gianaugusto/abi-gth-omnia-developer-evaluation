using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public static class DomainEvents
    {
        private static readonly List<Delegate> _handlers = new List<Delegate>();
        private static IMediator? _mediator;

        public static void Initialize(IMediator mediator)
        {
            _mediator = mediator;
        }

        public static void Register<T>(Action<T> handler) where T : class
        {
            _handlers.Add(handler);
        }

        public static void Raise<T>(T args) where T : class
        {
            foreach (var handler in _handlers)
            {
                if (handler is Action<T> action)
                {
                    action(args);
                }
            }

            _mediator?.Publish(args);
        }
    }
}
