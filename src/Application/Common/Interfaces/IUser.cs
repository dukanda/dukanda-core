namespace DukandaCore.Application.Common.Interfaces;

public interface IUser
{
    Guid? Id { get; }
    bool IsInRole(string roleName);
}
