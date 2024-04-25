using Microsoft.EntityFrameworkCore.Migrations;

namespace BookInventory.DataAccess.Migrations
{
    public partial class Initinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    ISBN = table.Column<string>(nullable: false),
                    PublicationYear = table.Column<string>(maxLength: 4, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Fantasy", "This book genre is characterized by elements of magic or the supernatural and is often inspired by mythology or folklore." },
                    { 2, "Horror", "he horror genre aspires to evoke fear, dread, and spine-tingling unease in its readers." },
                    { 3, "Mystery", "Mystery is a fiction genre where the nature of an event, usually a murder or other crime, remains mysterious until the end of the story." },
                    { 4, "Adventure", "ction and adventure novels feature a main character on a quest to achieve an ultimate goal." },
                    { 5, "Romance", "A romance novel or romantic novel is a genre fiction novel that primary focuses on the relationship and romantic love between two people, typically with an emotionally satisfying and optimistic ending. " },
                    { 6, "Thriller", "Thriller is a genre of fiction with numerous, often overlapping, subgenres, including crime, horror, and detective fiction." },
                    { 7, "Biography/Autobiography", "Both biographies and autobiographies tell the true story of a person's life" },
                    { 8, "Graphic novel", "A graphic novel is a long-form work of sequential art." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            var createIndexSql = "CREATE FULLTEXT CATALOG FTCBooks AS DEFAULT; \r\n" +

            "CREATE FULLTEXT INDEX ON dbo.Books(ISBN,Author,Title,PublicationYear) \r\n" +
            "  KEY INDEX PK_Books ON FTCBooks \r\n" +
            "  WITH STOPLIST = SYSTEM; \r\n";

            migrationBuilder.Sql(createIndexSql, true);

            var createProcSql = "CREATE PROCEDURE SearchABook\r\n\t" +
                "@search nvarchar(50),\r\n\t" +
                "@pageNumber int,\r\n\t" +
                "@pageSize int,\r\n\t" +
                "@asc bit\r\n" +
                "AS\r\n" +
                "BEGIN\r\n" +
                "SET NOCOUNT ON\r\n\r\n" +
                "if @search is not NULL AND @search != ''\r\n\t" +
                "select b.*, c.CategoryName from Books b\r\n\t" +
                "join Categories c on c.Id = b.CategoryId\r\n\t" +
                "where contains(b.Title, @search)\r\n\t   " +
                "OR contains(b.Author, @search)\r\n\t   " +
                "OR contains(b.ISBN, @search)\r\n\t   " +
                "OR contains(b.PublicationYear, @search)\r\n\t   " +
                "order by \r\n\t\t" +
                "case when @asc = 0 THEN b.Id ELSE 0 END DESC,\r\n\t\t" +
                "case when @asc = 1 THEN b.Id ELSE 0 END ASC\r\n\t" +
                "OFFSET @pageSize * (@pageNumber - 1) ROWS\r\n\t" +
                "FETCH NEXT @pageSize ROWS ONLY\r\n" +
                "ELSE \r\n\t" +
                "select b.*, c.CategoryName from Books b\r\n\t" +
                "join Categories c on c.Id = b.CategoryId\r\n\t  " +
                " order by \r\n\t\t" +
                "case when @asc = 0 THEN b.Id ELSE 0 END DESC,\r\n\t\t" +
                "case when @asc = 1 THEN b.Id ELSE 0 END ASC\r\n\t" +
                "OFFSET @pageSize * (@pageNumber - 1) ROWS\r\n\t" +
                "FETCH NEXT @pageSize ROWS ONLY\r\n" +
                "END\r\n" +
                "GO\r\n";

            migrationBuilder.Sql(createProcSql, true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
