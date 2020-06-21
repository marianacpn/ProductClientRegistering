using Application.App.Core;
using AutoMapper;

namespace Application.App.Entity
{
    public class AppClient : AppBase, Interface.IAppClient
    {
        public AppClient(IMapper mapper) : base(mapper)
        {
        }
    }
}
