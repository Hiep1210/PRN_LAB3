using System.Text.Json;

namespace PRN_Lab3.Logic
{
    public class CartService
    {
        ISession _session;
        private Dictionary<int, int> _cart;

        public CartService(ISession session)
        {
            _session = session;
            //if there is already cart, then deserialize cart 
            if (session.GetString("cart") != null)
            {
                string data = _session.GetString("cart");
                _cart = JsonSerializer.Deserialize<Dictionary<int, int>>(data);
            }
            //if there is no cart then serialize cart (case view when there is no cart ) 
            if (_cart == null)
            {
                _cart = new Dictionary<int, int>();
                _session.SetString("cart", JsonSerializer.Serialize(_cart));
            }

        }

        public void AddToCart(int productId)
        {
            if (_cart.ContainsKey(productId))
            {
                _cart[productId]++;

            }
            else
                _cart.Add(productId, 1);
            _session.SetString("cart", JsonSerializer.Serialize(_cart));
        }

        public Dictionary<int, int> GetCart()
        {
            return _cart;
        }
    }
}
