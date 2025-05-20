using System.Collections.Generic;
using Entities;

namespace BLL.Services.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Customer AddCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        bool DeleteCustomer(int id);
        List<Customer> SearchCustomers(string searchTerm);
    }
} 