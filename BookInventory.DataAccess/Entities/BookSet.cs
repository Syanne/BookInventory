namespace BookInventory.DataAccess.Entities
{
    public class BookSet
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string PublicationYear { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
