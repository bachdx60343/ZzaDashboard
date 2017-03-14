using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel : INotifyPropertyChanged
    {
        private ICustomersRepository _repo = new CustomersRepository();
        private ObservableCollection<Customer> _customers;

        public CustomerListViewModel()
        {
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
            OnChangeCustomerCommand = new RelayCommand(OnChangeCustomer);
        }

        public void LoadCustomers()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            Task<List<Customer>> customersResult = _repo.GetCustomersAsync();
            Customers = new ObservableCollection<Customer>(customersResult.Result);
        }

        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand OnChangeCustomerCommand { get; private set; }

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Customers"));
            }
        }
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                DeleteCommand.RaiseCanExecuteChanged();
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedCustomer"));
            }
        }
        private void OnDelete()
        {
             Customers.Remove(SelectedCustomer);
        }

        private bool CanDelete()
        {
            return SelectedCustomer != null;
        }

        private void OnChangeCustomer()
        {
            SelectedCustomer.FirstName = "Changed in background";
        }

        private bool CanChange()
        {
            return SelectedCustomer != null;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
