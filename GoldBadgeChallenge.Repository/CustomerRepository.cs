
public class CustomerRepository
{
    private readonly List<Customer> _customerDb = new List<Customer>();
    private int _count = 0;

    public bool AddCustomer(Customer customer)
    {
        if (customer is null)
        {
            return false;
        }
        else
        {
            _count++;
            customer.ID = _count;
            _customerDb.Add(customer);
            return true;
        }
    }

    public List<Customer> GetCustomers()
    {
        return _customerDb;
    }

    public Customer GetCustomer(int customerId)
    {
        return _customerDb.SingleOrDefault(c => c.ID == customerId)!;
    }

    public bool UpdateCustomer(int customerId, Customer updatedCustomerInfo)
    {
        var oldCustomerInfo = GetCustomer(customerId);
        if (oldCustomerInfo != null)
        {
            oldCustomerInfo.Name = updatedCustomerInfo.Name;
            oldCustomerInfo.Deliveries = updatedCustomerInfo.Deliveries;
            return true;
        }
        return false;
    }

    public bool DeleteCustomer(int customerId)
    {
        return _customerDb.Remove(GetCustomer(customerId));
    }
}
