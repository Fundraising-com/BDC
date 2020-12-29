using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class ViewPortMapper
    {
        public static ViewPort Hydrate(view_port viewPort)
        {
            var result = new ViewPort
            {
                Id = viewPort.id,
                Name = viewPort.name,
                BootstrapClass = viewPort.bootstrap_class,
                Created = viewPort.created_on
            };
            return result;
        }
    }
}
