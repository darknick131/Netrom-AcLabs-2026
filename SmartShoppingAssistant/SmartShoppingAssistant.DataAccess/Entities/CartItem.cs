namespace SmartShoppingAssistant.DataAccess.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; } = 0;

        // pentru navigatie, sa accesam direct obiectul
        // idk daca trb simplu sau colectie
        public Product Product { get; set; } = null!;
    }
}
