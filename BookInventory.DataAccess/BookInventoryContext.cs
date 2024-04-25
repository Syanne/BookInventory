using BookInventory.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.DataAccess
{
	public class BookInventoryContext : DbContext
    {
        public BookInventoryContext() : base()
        {
            //Database.SetInitializer(new DBInitializer());
        }
        public BookInventoryContext(DbContextOptions options) : base(options)
        {

		}
        //public BookInventoryContext CreateDbContext(string[] args)
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
        //    optionsBuilder.sq("Data Source=blog.db");

        //    return new BloggingContext(optionsBuilder.Options);
        //}

        public DbSet<Book> Books { get; set; }

		public DbSet<Category> Categories { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(
					"Data Source=DESKTOP-AUN9LHE;Initial Catalog=bookInventory;Integrated Security=True;Encrypt=False");
			}

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Category>().HasKey(x => x.Id);

			modelBuilder.Entity<Book>().HasKey(x => x.Id);
			modelBuilder.Entity<Book>().HasIndex(x => x.ISBN).IsUnique();

			modelBuilder.Entity<Book>()
				.HasOne(x => x.Category)
				.WithMany()
				.HasForeignKey(k => k.CategoryId)
				.IsRequired();

			modelBuilder.Entity<Category>().HasData(new Category
				{
					Id = 1,
					CategoryName = "Fantasy",
					Description =
						"This book genre is characterized by elements of magic or the supernatural and is often inspired by mythology or folklore."
				},
				new Category
				{
					Id = 2,
					CategoryName = "Horror",
					Description =
						"he horror genre aspires to evoke fear, dread, and spine-tingling unease in its readers."
				},
				new Category
				{
					Id = 3,
					CategoryName = "Mystery",
					Description =
						"Mystery is a fiction genre where the nature of an event, usually a murder or other crime, remains mysterious until the end of the story."
				},
				new Category
				{
					Id = 4,
					CategoryName = "Adventure",
					Description =
						"ction and adventure novels feature a main character on a quest to achieve an ultimate goal."
				},
				new Category
				{
					Id = 5,
					CategoryName = "Romance",
					Description =
						"A romance novel or romantic novel is a genre fiction novel that primary focuses on the relationship and romantic love between two people, typically with an emotionally satisfying and optimistic ending. "
				},
				new Category
				{
					Id = 6,
					CategoryName = "Thriller",
					Description =
						"Thriller is a genre of fiction with numerous, often overlapping, subgenres, including crime, horror, and detective fiction."
				},
				new Category
				{
					Id = 7,
					CategoryName = "Biography/Autobiography",
					Description = "Both biographies and autobiographies tell the true story of a person's life"
				},
				new Category
				{
					Id = 8,
					CategoryName = "Graphic novel",
					Description = "A graphic novel is a long-form work of sequential art."
				});
		}
	}
}