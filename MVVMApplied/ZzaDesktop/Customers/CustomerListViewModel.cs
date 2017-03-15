using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers
{
    public class CustomerListViewModel : BindableBase
    {
        private ICustomersRepository _repo;

        public CustomerListViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        private string _searchInput;
        public string SearchInput
        {
            get { return _searchInput; }
            set
            {
                SetProperty(ref _searchInput, value);
                FilterCustomers(_searchInput);
            }
        }

        private void FilterCustomers(string searchInput)
        {
            if (string.IsNullOrEmpty(searchInput))
            {
                Customers = new ObservableCollection<Customer>(_allCustomers);
                return;
            }
            else
            {
                Customers = new ObservableCollection<Customer>(_allCustomers.Where(c => c.FullName.ToLower().Contains(searchInput.ToLower())));
            }
        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        public RelayCommand AddCustomerCommand { get; private set; }
        public RelayCommand<Customer> EditCustomerCommand { get; private set; }
        public RelayCommand ClearSearchCommand { get; private set; }

        private List<Customer> _allCustomers;
        public async void LoadCustomers()
        {
            _allCustomers = await _repo.GetCustomersAsync();
            Customers = new ObservableCollection<Customer>(_allCustomers);
        }

        public event Action<Guid> PlaceOrderRequested = delegate { };
        public event Action<Customer> AddCustomerRequested = delegate { };
        public event Action<Customer> EditCustomerRequested = delegate { };

        private void OnPlaceOrder(Customer customer)
        {
            PlaceOrderRequested(customer.Id);
        }
        private void OnAddCustomer()
        {
            AddCustomerRequested(new Customer { Id = Guid.NewGuid() });
        }
        private void OnEditCustomer(Customer customer)
        {          
            EditCustomerRequested(customer);
        }
        private void OnClearSearch()
        {
            SearchInput = null;
        }
    }
}
