

namespace RestaurantDesktopClient
{
    public class Services
    {
        public readonly static CustomerService.ICustomerService _CustomerProxy;
        public readonly static ItemService.IItemService _ItemProxy;
        public readonly static MenuService.IMenuService _MenuProxy;
        public readonly static PriceService.IPriceService _PriceProxy;
        public readonly static RestaurantService.IRestaurantService _RestaurantProxy;

        static Services()
        {
            _CustomerProxy = new CustomerService.CustomerServiceClient();
            _ItemProxy = new ItemService.ItemServiceClient();
            _MenuProxy = new MenuService.MenuServiceClient();
            _PriceProxy = new PriceService.PriceServiceClient();
            _RestaurantProxy = new RestaurantService.RestaurantServiceClient();
        }
    }
}
