using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;

namespace ZzaDesktop.Customers
{
    class AddEditCustomerViewModel : BindableBase
    {
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
            Customer = new SimpleEditableCustomer();
            CopyCustomer(customer, Customer);
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
    }
}
