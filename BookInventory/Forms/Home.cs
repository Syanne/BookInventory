using BookInventory.Interfaces;
using BookInventory.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookInventory
{
    public partial class Home : Form
    {
        private readonly IBookService _bookService;
        private readonly AddBookForm _addBookForm;
        private readonly DeleteBookForm _deleteBookForm;
        private readonly EditBookForm _editBookForm;

        public Home(IBookService bookService,
            AddBookForm addBookForm,
            EditBookForm editBookForm,
            DeleteBookForm deleteBookForm)
        {
            InitializeComponent();
            _bookService = bookService;
            _addBookForm = addBookForm;
            _editBookForm = editBookForm;
            _deleteBookForm = deleteBookForm;
        }
        private async void Home_Load(object sender, EventArgs e)
        {
            await UpdateItems();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await ShowAndUpdateForm(_addBookForm);
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var result = sender as DataGridView;
            if (result != null)
            {
                //edit action
                if (result.CurrentCell.ColumnIndex == 7)
                {
                    _editBookForm.Id = (int)result.CurrentRow.Cells[0].Value;
                    await ShowAndUpdateForm(_editBookForm);
                }
                //delete action
                if (result.CurrentCell.ColumnIndex == 8)
                {
                    _deleteBookForm.Id = (int)result.CurrentRow.Cells[0].Value;
                    await ShowAndUpdateForm(_deleteBookForm);
                }
            }
        }

        private async Task UpdateItems()
        {
            var books = await _bookService.Get(1, 10, string.Empty, false);
            bookRowVmBindingSource.DataSource = books
                .Select(x => new BookRowVm
                {
                    Author = x.Author,
                    Title = x.Title,
                    Id = x.Id,
                    ISBN = x.ISBN,
                    PublicationYear = x.PublicationYear,
                    CategoryName = x.CategoryName,
                    Quantity = x.Quantity
                })
                .ToArray();
        }

        private async Task ShowAndUpdateForm(Form form)
        {
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                Invalidate();
                bookRowVmBindingSource.Clear();
                await UpdateItems();
            }
        }
    }
}
