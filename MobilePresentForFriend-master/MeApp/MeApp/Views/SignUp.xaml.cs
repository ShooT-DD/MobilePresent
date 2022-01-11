using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        SqlConnection sqlConnection;
        private Dictionary<string, string> logins = new Dictionary<string, string>();
        public SignUp()
        {
            InitializeComponent();
            LoadLogins();
        }
        private async Task LoadLogins()
        {
            var connectionString = $"Data Source=sql.bsite.net\\MSSQL2016; Initial Catalog=danka0060_SampleDB; User ID=danka0060_SampleDB; Password=danka0060";
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                var queryString = "Select * from dbo.AspNetUsers";
                var cmd = new SqlCommand(queryString, sqlConnection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    logins.Add(
                        reader["UserName"].ToString(), 
                        reader["PasswordHash"].ToString()
                    );
                }
                reader.Close();
                sqlConnection.Close();
            }

            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }
            
        void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if(logins.ContainsKey(login.Text) && logins[login.Text] == password.Text)
            {
                Application.Current.MainPage = new Home();
            }
            else
            {
                error.Text = "Wrong password or login!";
            }
        }

    }
}