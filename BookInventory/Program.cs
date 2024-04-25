using BookInventory.Interfaces;
using BookInventory.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;

namespace BookInventory
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<Home>());
        }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<IBookService, BookService>();
                    services.AddTransient<IBookInventoryClient, BookInventoryClient>();
                    services.AddTransient<Home>();
                    services.AddTransient<AddBookForm>();
                    services.AddTransient<DeleteBookForm>();
                    services.AddTransient<EditBookForm>();
                });
        }
    }
}
