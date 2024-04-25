namespace BookInventory.Models.ViewModels
{
    public class BookRowVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string PublicationYear { get; set; }

        public int Quantity { get; set; }

        public string CategoryName { get; set; }

        public string Edit => nameof(Edit);

        public string Delete => nameof(Delete);
    }
}
