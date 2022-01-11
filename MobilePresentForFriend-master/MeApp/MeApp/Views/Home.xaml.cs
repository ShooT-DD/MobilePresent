using MeApp.Models;
using MeApp.ViewsModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        SqlConnection sqlConnection;
        public static List<Present> presents = new List<Present>();
        private HomeModel viewModel { get; set; }

        public Home()
        {
            InitializeComponent();
            ShowPresentListAsync();
            viewModel = new HomeModel();
            BindingContext = viewModel;
        }

        private async Task ShowPresentListAsync()
        {
            var connectionString = $"Data Source=sql.bsite.net\\MSSQL2016; Initial Catalog=danka0060_SampleDB; User ID=danka0060_SampleDB; Password=danka0060";
            sqlConnection = new SqlConnection(connectionString);
            

            try
            {
                sqlConnection.Open();
                string queryString = "Select * from dbo.Presents";
                var cmd = new SqlCommand(queryString, sqlConnection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    presents.Add(new Present
                    {
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Link = reader["Link"].ToString(),
                        Price = (float)reader["Price"],
                        DataFiles = LoadImage((byte[])reader["DataFiles"]),                  
                    });
                }
                reader.Close();
                sqlConnection.Close();
                //LoadImages(presents);
                presentList.ItemsSource = presents;
            }

            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }

        //private async void moreButton_Clicked(object sender, EventArgs e)
        //{
        //    var kek = presentList.SelectedItem.GetType();
        //    var str = presents[0].Link;
        //    await Navigation.PushAsync(new More(kek.Name));
        //}

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = (e.CurrentSelection.FirstOrDefault() as Present)?.Link;
            await Navigation.PushAsync(new More(current));
        }
        async void OnFavourButton_Clicked(object sender, EventArgs e)
        {
            
        }
        public static ImageSource LoadImage(byte[] imageIn)
        {
            var ms = new MemoryStream(imageIn);
            return ImageSource.FromStream(() => ms);
        }
    }
}