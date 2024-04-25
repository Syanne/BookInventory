using BookInventory.Interfaces;
using System;
using System.Windows.Forms;

namespace BookInventory
{
    public partial class DeleteForm : Form
    {
        public int Id { get; set; }
        private readonly IBookService _bookService;

        public DeleteForm(IBookService bookService)
        {
            InitializeComponent();
            _bookService = bookService;
        }

        private async void DeleteForm_Load(object sender, EventArgs e)
        {
            var book = await _bookService.Get(Id);
            label2.Text = $"'{book.CategoryName}'";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _bookService.Delete(Id);
            DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
