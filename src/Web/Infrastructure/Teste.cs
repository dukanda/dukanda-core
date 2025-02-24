using Hangfire.Dashboard;

namespace DukandaCore.Web.Infrastructure;

public class AllowAllConnectionsFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true;
}