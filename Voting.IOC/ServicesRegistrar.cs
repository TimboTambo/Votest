using Microsoft.Practices.Unity;
using Voting.Hubs;
using Voting.Services;
using Voting.Services.Implementation.Repositories;
using Voting.Services.Implementation.Repositories.Implementations;
using Voting.Services.Implementation.Services;

namespace Voting.IOC
{
    public class ServicesRegistrar
    {
        public void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IPollService, PollService>()
               .RegisterType<IPollRepository, PollRepository>()
               .RegisterType<IPollHub, PollHub>();
        }
    }
}
