using MeApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MeApp.ViewsModel
{
    public class HomeModel
    {
        public ICommand FavourCommand { get; private set; }
        SqlConnection sqlConnection;

        public HomeModel()
        {
            FavourCommand = new Command<Present>(async (model) => await FavourExec(model));
        }

        private async Task FavourExec(Present model)
        {
            var connectionString = $"Data Source=sql.bsite.net\\MSSQL2016; Initial Catalog=danka0060_SampleDB; User ID=danka0060_SampleDB; Password=danka0060";
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Favourites VALUES(@UserId, @PresentId)", sqlConnection))
                {
                    cmd.Parameters.Add(new SqlParameter("UserId", "1"));
                    cmd.Parameters.Add(new SqlParameter("PresentId", model.Id));
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
    }
}
