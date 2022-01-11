using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewPresent : ContentPage
    {
        SqlConnection sqlConnection;
        public byte[] imageArray;

        public AddNewPresent()
        {
            InitializeComponent();
            NewPresentAdd();
        }

        private void NewPresentAdd()
        {
            string connectionString = $"Data Source=sql.bsite.net\\MSSQL2016; Initial Catalog=danka0060_SampleDB; User ID=danka0060_SampleDB; Password=danka0060";
            sqlConnection = new SqlConnection(connectionString);
        }

        private async void postButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Presents VALUES(@Name, @Description, @Link, @Price, @UserID, @DataFiles)", sqlConnection))
                {
                    cmd.Parameters.Add(new SqlParameter("Name", Name.Text));
                    cmd.Parameters.Add(new SqlParameter("Description", Description.Text));
                    cmd.Parameters.Add(new SqlParameter("Link", Link.Text));
                    cmd.Parameters.Add(new SqlParameter("Price", Convert.ToDouble(Price.Text)));
                    cmd.Parameters.Add(new SqlParameter("UserID", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("DataFiles", imageArray));
                    cmd.ExecuteNonQuery();
                }

                sqlConnection.Close();
                await App.Current.MainPage.DisplayAlert("Alert", "Success", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }

        private async void uploadfile_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "not support", "Ok");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CompressionQuality = 40
            });

            //Conver to byte array
            imageArray = System.IO.File.ReadAllBytes(file.Path);

            //Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);           
        }
    }
}