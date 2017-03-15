using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers
{
    class AddEditCustomerViewModel : BindableBase
    {
        private ICustomersRepository _repo;

        public AddEditCustomerViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            SaveCommand = new RelayCommand(OnSave, CanSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private bool _editMode;
        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        private SimpleEditableCustomer _customer = null;
        public SimpleEditableCustomer Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        private Customer _editingCustomer = null;

        public void SetCustomer(Customer customer)
        {
            _editingCustomer = customer;
            if (Customer != null)
                Customer.ErrorsChanged -= RaiseCanExecuteChanged;
            Customer = new SimpleEditableCustomer();
            Customer.ErrorsChanged += RaiseCanExecuteChanged;
            CopyCustomer(customer, Customer);
        }

        private void RaiseCanExecuteChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void CopyCustomer(Customer source, SimpleEditableCustomer target)
        {
            target.Id = source.Id;
            if (EditMode)
            {
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
                target.Phone = source.Phone;
                target.Email = source.Email;
            }
        }

        public event Action Done = delegate { };

        private async void OnSave()
        {
            UpdateCustomer(Customer, _editingCustomer);
            if (EditMode)
                await _repo.UpdateCustomerAsync(_editingCustomer);
            else
                await _repo.AddCustomerAsync(_editingCustomer);
            Done();
        }

        private void UpdateCustomer(SimpleEditableCustomer source, Customer target)
        {
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.Email = source.Email;
        }

        private bool CanSave()
        {
            return !Customer.HasErrors  ;
        }
        private void OnCancel()
        {
            Done();
        }
    }
}
