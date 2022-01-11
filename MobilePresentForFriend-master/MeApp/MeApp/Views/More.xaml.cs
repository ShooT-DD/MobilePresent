using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class More : ContentPage
    {
        public More(string a)
        {
            InitializeComponent();
            text.Text = a;
        }
    }
}