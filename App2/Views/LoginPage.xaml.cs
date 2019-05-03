using App2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BackgroundColor = Constants.BGColor;
            Lbl_User.TextColor = Constants.TxtColor;
            Lbl_Pass.TextColor = Constants.TxtColor;

            Ent_User.Completed += (s, e) => Ent_Pass.Focus();
            Ent_Pass.Completed += (s, e) => Evt_SigninAsync(s,e);
        }

        async void Evt_SigninAsync(object sender, EventArgs e)
        {
            User user = new User(Ent_User.Text, Ent_Pass.Text);
            if (user.CheckInfo())
            {
                DisplayAlert("Login","Login Successful","Ok");
                var result = await App.RestService.Login(user);
                if (result.access_token != null)
                {
                    App.UserDatabase.SaveUser(user);
                }
            }
            else
            {
                DisplayAlert("Login", "Login Failed, Please Enter Username and Password", "Ok");
            }

        }
	}
}