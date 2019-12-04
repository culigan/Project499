using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUser : ContentPage
    {
        NewUserViewModel viewModel;
        
        public NewUser()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewUserViewModel();
        }
        //Need to add to XAML File <local:RequiredValidatorBehavior x:Name="passwordValidator"/>
        public class RequiredValidatorBehavior : Behavior<Entry>
        {
            static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(RequiredValidatorBehavior), false);
            static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

            public bool IsValid
            {
                get { return (bool)base.GetValue(IsValidProperty); }
                private set { base.SetValue(IsValidPropertyKey, value); }
            }

            protected override void OnAttachedTo(Entry bindable)
            {
                bindable.Unfocused += HandleFocusChanged;
                base.OnAttachedTo(bindable);
            }
            protected override void OnDetachingFrom(Entry bindable)
            {
                bindable.Unfocused -= HandleFocusChanged;
                base.OnDetachingFrom(bindable);
            }
            void HandleFocusChanged(object sender, FocusEventArgs e)
            {
                IsValid = !string.IsNullOrEmpty(((Entry)sender).Text);
            }
        }

        //Need to add to XAML file: <local:ConfirmPasswordBehavior x:Name="confirmPasswordBehavior" CompareToEntry="{Binding Source={x:Reference password}}" />
        public class ComparisonBehavior : Behavior<Entry>
        {
            private Entry thisEntry;

            static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(ComparisonBehavior), false);
            public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

            public static readonly BindableProperty CompareToEntryProperty = BindableProperty.Create("CompareToEntry", typeof(Entry), typeof(ComparisonBehavior), null);

            public Entry CompareToEntry
            {
                get { return (Entry)base.GetValue(CompareToEntryProperty); }
                set
                {
                    base.SetValue(CompareToEntryProperty, value);
                    if (CompareToEntry != null)
                        CompareToEntry.TextChanged -= baseValue_changed;
                    value.TextChanged += baseValue_changed;
                }
            }

            void baseValue_changed(object sender, TextChangedEventArgs e)
            {
                IsValid = ((Entry)sender).Text.Equals(thisEntry.Text);
                thisEntry.TextColor = IsValid ? Color.Default : Color.Red;
            }


            public bool IsValid
            {
                get { return (bool)base.GetValue(IsValidProperty); }
                private set { base.SetValue(IsValidPropertyKey, value); }
            }
            protected override void OnAttachedTo(Entry bindable)
            {
                thisEntry = bindable;

                if (CompareToEntry != null)
                    CompareToEntry.TextChanged += baseValue_changed;

                bindable.TextChanged += HandleTextChanged;
                base.OnAttachedTo(bindable);
            }

            protected override void OnDetachingFrom(Entry bindable)
            {
                bindable.TextChanged -= HandleTextChanged;
                if (CompareToEntry != null)
                    CompareToEntry.TextChanged -= baseValue_changed;
                base.OnDetachingFrom(bindable);
            }

            void HandleTextChanged(object sender, TextChangedEventArgs e)
            {
                string theBase = CompareToEntry.Text;
                string confirmation = e.NewTextValue;
                IsValid = (bool)theBase?.Equals(confirmation);

                ((Entry)sender).TextColor = IsValid ? Color.Green : Color.Red;
            }
        }

        async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd");

            try
            {
                UserEntry.BackgroundColor = Color.Transparent;
                FirstNameEntry.BackgroundColor = Color.Transparent;
                LastNameEntry.BackgroundColor = Color.Transparent;
                FirstPasswordEntry.BackgroundColor = Color.Transparent;
                SecondPasswordEntry.BackgroundColor = Color.Transparent;

                viewModel.IsBusy = true;
                viewModel.DataQuery.expenseSelect = "SELECT * FROM Users ";
                viewModel.DataQuery.expenseWhere = "WHERE Username = '" + viewModel.Username + "'";
                viewModel.UsersInfo = viewModel.DataQuery.ExecuteAQuery<Users>();

                if (viewModel.UsersInfo.Count >= 1)
                {
                    UserEntry.BackgroundColor = Color.Red;
                    DependencyService.Get<IToast>().Show("Username is not unique");
                }
                else if ((viewModel.FirstPasswordHash == viewModel.SecondPasswordHash) && (viewModel.Username != null) && (viewModel.Firstname != null) && (viewModel.Lastname != null)  && (viewModel.FirstPassword != null))
                {
                    viewModel.DataQuery.expenseSelect = "INSERT INTO users VALUES ";
                    viewModel.DataQuery.expenseWhere = "('" + viewModel.Username + "', '" + viewModel.Firstname + "', '" + viewModel.Lastname + "', '" + viewModel.SecondPasswordHash + "', '" + sqlFormattedDate + "')";
                    viewModel.UsersInfo = viewModel.DataQuery.ExecuteAQuery<Users>();
                    DependencyService.Get<IToast>().Show("New User " + viewModel.Username + " Created");
                    await Navigation.PopModalAsync();
                }

                else if (viewModel.FirstPasswordHash != viewModel.SecondPasswordHash)
                {
                    SecondPasswordEntry.BackgroundColor = Color.Red;
                    DependencyService.Get<IToast>().Show("Passwords do not match");
                }

                else if (viewModel.Username == null)
                {
                    UserEntry.BackgroundColor = Color.Red;
                    DependencyService.Get<IToast>().Show("Username cannot be blank");
                }

                else if (viewModel.Firstname == null)
                {
                    FirstNameEntry.BackgroundColor = Color.Red;
                    DependencyService.Get<IToast>().Show("First name cannot be blank");
                }

                else if (viewModel.Lastname == null)
                {
                    LastNameEntry.BackgroundColor = Color.Red;
                    DependencyService.Get<IToast>().Show("Last name cannot be blank");
                }

                else if (viewModel.FirstPassword == null)
                {
                    FirstPasswordEntry.BackgroundColor = Color.Red;
                    SecondPasswordEntry.BackgroundColor = Color.Red;
                    DependencyService.Get<IToast>().Show("Password cannot be blank");
                }

                viewModel.IsBusy = false;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Unsuccessful user creation", ex.Message, "OK");
                viewModel.IsBusy = false;
            }
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}