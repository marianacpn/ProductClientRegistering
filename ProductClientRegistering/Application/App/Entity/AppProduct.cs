using AutoMapper;

namespace Application.App.Entity
{
    public class AppProduct : Core.AppBase, Interface.IAppProduct
    {
        public AppProduct(IMapper mapper) : base(mapper)
        {
        }
    }
}
