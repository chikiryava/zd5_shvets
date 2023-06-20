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
    public partial class Calc :ContentPage
    {
        public Calc ()
        {
            InitializeComponent();
        }
        private void SliderValueChange (object sender, ValueChangedEventArgs e)
        {
            SliderLabel.Text = $"{Slider.Value}%";

            if (LoanEntry.Text != "" && MonthEntry.Text != "")
            {
                Calculation(LoanEntry.Text, MonthEntry.Text, PaymentTypePicker.SelectedIndex, Slider.Value);
            } else
            {
                this.DisplayToastAsync("Вы не ввели все значения", 1000);
                MonthlyPaymentLabel.Text = "Ежемесячный платеж: ...";
                TotalLabel.Text = "Общая сумма: ...";
                OverpaymentLabel.Text = "Переплата: ...";
            }
        }

        private void Calculation (string loanAmount, string loanTermInMonths, int PickerPayment, double interestRate)
        {
            try
            {
                if (Convert.ToDouble(loanTermInMonths) > 0 && Convert.ToDouble(loanAmount) > 0)
                {

                    double monthlyInterestRate = interestRate / 1200; // перевод процентной ставки в месячную долю
                    double annuityFactor = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, int.Parse(loanTermInMonths)) / (Math.Pow(1 + monthlyInterestRate, int.Parse(loanTermInMonths)) - 1); // вычисляем коэффициент аннуитета
                    double annuityPayment = Math.Round(double.Parse(loanAmount) * annuityFactor, 2); // вычисляем ежемесячный платеж

                    MonthlyPaymentLabel.Text = $"Ежемесячный платеж: {annuityPayment}";
                    TotalLabel.Text = $"Общая сумма: {Math.Round(annuityPayment, 2) * int.Parse(loanTermInMonths)}";
                    OverpaymentLabel.Text = $"Переплата: {Math.Round(Math.Round(annuityPayment, 2) * int.Parse(loanTermInMonths) - Math.Round(double.Parse(loanAmount), 2), 2)}";


                } else
                {
                    this.DisplayToastAsync("Значения должны быть положительными", 1000);
                    MonthlyPaymentLabel.Text = "Ежемесячный платеж: N/A";
                    TotalLabel.Text = "Общая сумма: N/A";
                    OverpaymentLabel.Text = "Переплата: N/A";
                }
            } catch
            {
                this.DisplayToastAsync("Вы должны вводить только числа");
            }
        }
    }
}