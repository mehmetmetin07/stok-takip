using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.Interfaces;
using DAL.Repositories;
using Entities;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                return _customerRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                // Loglama yapılabilir
                throw new Exception("Müşterileri getirirken bir hata oluştu", ex);
            }
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                return _customerRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Müşteri (ID: {id}) getirilirken bir hata oluştu", ex);
            }
        }

        public Customer AddCustomer(Customer customer)
        {
            try
            {
                customer.CreatedDate = DateTime.Now;
                _customerRepository.Add(customer);
                _customerRepository.SaveChanges();
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri eklenirken bir hata oluştu", ex);
            }
        }

        public Customer UpdateCustomer(Customer customer)
        {
            try
            {
                _customerRepository.Update(customer);
                _customerRepository.SaveChanges();
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception($"Müşteri (ID: {customer.Id}) güncellenirken bir hata oluştu", ex);
            }
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                var customer = _customerRepository.GetById(id);
                if (customer == null)
                    return false;

                _customerRepository.Remove(customer);
                _customerRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Müşteri (ID: {id}) silinirken bir hata oluştu", ex);
            }
        }

        public List<Customer> SearchCustomers(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return GetAllCustomers();

                return _customerRepository.GetAll().Where(c => 
                    c.Name.Contains(searchTerm) || 
                    (c.PhoneNumber != null && c.PhoneNumber.Contains(searchTerm)) || 
                    (c.Email != null && c.Email.Contains(searchTerm))
                ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Müşteri araması yapılırken bir hata oluştu: {ex.Message}", ex);
            }
        }
    }
} 