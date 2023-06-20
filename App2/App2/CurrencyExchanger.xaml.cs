using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyExchanger :ContentPage
    {
        public CurrencyExchanger ()
        {
            InitializeComponent();
        }

        private void usdEntry_TextChanged (object sender, TextChangedEventArgs e)
        {
            try
            {
                if (double.Parse(usdEntry.Text) >= 0)
                {
                    eurLabel.Text = (double.Parse(usdEntry.Text) * 1.075).ToString();
                } else
                {
                    this.DisplayToastAsync("Значение должно быть положительным", 5000);
                }
            } catch
            {
                this.DisplayToastAsync("Вы должны ввести число", 5000);
            }
        }
    }
}