using AutoMapper;

namespace PurchasePortal.Web.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg => { cfg.AddProfile(new CustomProfile()); });
        }
    }
}
