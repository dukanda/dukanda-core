using DukandaCore.Domain.Identity;

namespace DukandaCore.Domain.Events;
public class UserCreatedEvent : BaseEvent
{
    public User User { get; }

    public UserCreatedEvent(User user)
    {
        User = user;
    }
}
