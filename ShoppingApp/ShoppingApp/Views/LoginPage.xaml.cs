using System;
using Xamarin.Forms;

namespace ShoppingApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void TapBackArrow_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}