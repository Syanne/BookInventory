﻿// <auto-generated />
using BookInventory.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookInventory.DataAccess.Migrations
{
    [DbContext(typeof(BookInventoryContext))]
    [Migration("20240424173745_Initinal")]
    partial class Initinal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookInventory.DataAccess.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PublicationYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ISBN")
                        .IsUnique();

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookInventory.DataAccess.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Fantasy",
                            Description = "This book genre is characterized by elements of magic or the supernatural and is often inspired by mythology or folklore."
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Horror",
                            Description = "he horror genre aspires to evoke fear, dread, and spine-tingling unease in its readers."
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Mystery",
                            Description = "Mystery is a fiction genre where the nature of an event, usually a murder or other crime, remains mysterious until the end of the story."
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Adventure",
                            Description = "ction and adventure novels feature a main character on a quest to achieve an ultimate goal."
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Romance",
                            Description = "A romance novel or romantic novel is a genre fiction novel that primary focuses on the relationship and romantic love between two people, typically with an emotionally satisfying and optimistic ending. "
                        },
                        new
                        {
                            Id = 6,
                            CategoryName = "Thriller",
                            Description = "Thriller is a genre of fiction with numerous, often overlapping, subgenres, including crime, horror, and detective fiction."
                        },
                        new
                        {
                            Id = 7,
                            CategoryName = "Biography/Autobiography",
                            Description = "Both biographies and autobiographies tell the true story of a person's life"
                        },
                        new
                        {
                            Id = 8,
                            CategoryName = "Graphic novel",
                            Description = "A graphic novel is a long-form work of sequential art."
                        });
                });

            modelBuilder.Entity("BookInventory.DataAccess.Entities.Book", b =>
                {
                    b.HasOne("BookInventory.DataAccess.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
