using BookInventory.Interfaces;
using BookInventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BookInventory
{
    public partial class AddBook : Form
    {
        IBookService _bookService;

        public AddBook(IBookService bookService)
        {
            InitializeComponent();
            _bookService = bookService;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var selectedCategory = category.SelectedItem as dynamic;
            var key = selectedCategory?.Key ?? 0;

            //todo: implement validation
            if (!string.IsNullOrWhiteSpace(title.Text)
                && !string.IsNullOrWhiteSpace(author.Text)
                && !string.IsNullOrWhiteSpace(isbn.Text)
                && !string.IsNullOrWhiteSpace(pubYear.Text))
            {
                await _bookService.Add(
                    title.Text,
                    author.Text,
                    isbn.Text,
                    pubYear.Text,
                    (int)quantity.Value,
                    key);
            }

            DialogResult = DialogResult.OK;
        }

        private async void AddBook_Load(object sender, EventArgs e)
        {
            var categories = await _bookService.GetCategories();
            category.DataSource = categories
                .Select(s => new KeyValuePair<int, string>(s.Id, s.CategoryName))
                .ToList();
        }
    }
}
