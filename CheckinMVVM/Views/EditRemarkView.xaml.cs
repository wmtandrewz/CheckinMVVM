using System;
using System.Collections.Generic;
using CheckinMVVM.ViewModels;
using Xamarin.Forms;

namespace CheckinMVVM.Views
{
    public partial class EditRemarkView : ContentPage
    {
        private EditRemarksViewModel _editRemarksViewModel;

        public EditRemarkView(string operationType)
        {
            InitializeComponent();
            _editRemarksViewModel = new EditRemarksViewModel(Navigation,operationType);
            BindingContext = _editRemarksViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _editRemarksViewModel.PageOnLoadCommand.Execute(null);
            editor.Focus();
        }
    }
}
