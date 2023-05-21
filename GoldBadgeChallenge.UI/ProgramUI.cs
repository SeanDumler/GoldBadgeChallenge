
public class ProgramUI
{
    private CustomerRepository _cRepo = new CustomerRepository();

    private DeliveryRepository _dRepo = new DeliveryRepository();

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();

            System.Console.WriteLine("Welcome to Delivery Service.\n" +
            "========== Deliveries ==========\n" +
            "1. View All Deliveries\n" +
            "2. View Delivery by Order Number\n" +
            "3. Add a Delivery\n" +
            "4. Update a Delivery\n" +
            "5. Delete a Delivery\n" +
            "========== Customers ==========\n" +
            "6. View All Customers\n" +
            "7. View Customer by ID\n" +
            "8. Add a Customer\n" +
            "9. Update a Customer\n" +
            "10. Delete a Customer\n" +
            "========== Exit Application ===========\n" +
            "00. Exit Application");

            var userInput = int.Parse(Console.ReadLine()!);
            switch (userInput)
            {
                case 1:
                    ViewAllDeliveries();
                    break;
                case 2:
                    ViewDeliveryByID();
                    break;
                case 3:
                    AddDelivery();
                    break;
                case 4:
                    UpdateDelivery();
                    break;
                case 5:
                    DeleteDelivery();
                    break;
                case 6:
                    ViewAllCustomers();
                    break;
                case 7:
                    ViewCustomerById();
                    break;
                case 8:
                    AddCustomer();
                    break;
                case 9:
                    UpdateCusomer();
                    break;
                case 10:
                    DeleteCustomer();
                    break;
                case 00:
                    isRunning = false;
                    ExitApplication();
                    break;
                default:
                    System.Console.WriteLine("Invalid Selection. Please choose between 1-10.");
                    PressAnyKey();
                    break;
            }
        }
    }

    private void ExitApplication()
    {
        Console.Clear();
        System.Console.WriteLine("Thank you for using Delivery Services!");
        PressAnyKey();
        Console.Clear();
    }

    private void DeleteCustomer()
    {
        Console.Clear();
        ShowAllCustomers();

        try
        {
            System.Console.WriteLine("Select the Customer ID you would like to remove.");

            int userInputCustomerDelete = int.Parse(Console.ReadLine()!);

            var isConfirmed = ConfirmCustomerInDb(userInputCustomerDelete);

            if (isConfirmed)
            {
                if (_cRepo.DeleteCustomer(userInputCustomerDelete))
                {
                    System.Console.WriteLine("Customer was deleted.");
                }
                else
                {
                    System.Console.WriteLine("Customer was deleted.");                    
                }
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }

        PressAnyKey();
    }

    private void UpdateCusomer()
    {
        Console.Clear();
        ShowAllCustomers();
        try
        {
            System.Console.WriteLine("Select the Customer ID you would like to update.");

            int userInputCustomerId = int.Parse(Console.ReadLine()!);

            Customer customerInDb = GetCustomerFromDb(userInputCustomerId);

            bool isConfirmed = ConfirmCustomerInDb(customerInDb.ID);

            if (isConfirmed)
            {
                Customer updatedCustomer = SetupCustomer();

                if (_cRepo.UpdateCustomer(customerInDb.ID, updatedCustomer))
                {
                    System.Console.WriteLine($"Customer {updatedCustomer.Name} has been updated.");
                }
                else
                {
                    System.Console.WriteLine($"Customer {updatedCustomer.Name} has been updated.");                    
                }
            }
            else
            {
                System.Console.WriteLine("Customer not found.");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private void AddCustomer()
    {
        Console.Clear();

        try
        {
            Customer customer = SetupCustomer();
            if (_cRepo.AddCustomer(customer))
            {
                System.Console.WriteLine($"{customer.Name} has been added to the system.");
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private Customer SetupCustomer()
    {
        Customer customer = new Customer();

        System.Console.WriteLine("Please enter Customer Information.");

        try
        {
            System.Console.WriteLine("What is the customer name?");
            customer.Name = Console.ReadLine()!;

            bool hasAddedDelivery = false;

            List<Delivery> listOfDeliveries = _dRepo.GetAllDeliveries();

            while (hasAddedDelivery == false)
            {
                System.Console.WriteLine("Does this customer have a delivery?\n"+
                                        "Yes or No");
                string userInputAddedDelivery = Console.ReadLine()!.ToLower();

                if (userInputAddedDelivery == "yes")
                {
                    if (listOfDeliveries.Count() > 0)
                    {
                        DisplayCustomersInDb(listOfDeliveries);
                        System.Console.WriteLine("====================");
                        System.Console.WriteLine("Select Order Number");
                        int userInputDeliveryID = int.Parse(Console.ReadLine()!);
                        Delivery selectedDelivery = _dRepo.GetDelivery(userInputDeliveryID);

                        if (selectedDelivery != null)
                        {
                            customer.Deliveries.Add(selectedDelivery);
                            listOfDeliveries.Remove(selectedDelivery);
                        }
                        else
                        {
                            System.Console.WriteLine("Delivery not found");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No deliveries left to add.");
                        PressAnyKey();
                        break;
                    }
                }
                else
                {
                    hasAddedDelivery = true;
                }
            }
            return customer;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
        return null!;
    }

    private void DisplayCustomersInDb(List<Delivery> listOfDeliveries)
    {
        if (listOfDeliveries.Count > 0)
        {
            foreach (Delivery delivery in listOfDeliveries)
            {
                System.Console.WriteLine(delivery);
            }
        }
    }

    private void ViewCustomerById()
    {
        Console.Clear();
        ShowAllCustomers();
        System.Console.WriteLine("====================");

        try
        {
            System.Console.WriteLine("Please enter Customer ID.");
            int userInputCustomerId = int.Parse(Console.ReadLine()!);
            ConfirmCustomerInDb(userInputCustomerId);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }

        PressAnyKey();
    }

    private bool ConfirmCustomerInDb(int userInputCustomerId)
    {
        Customer customer =GetCustomerFromDb(userInputCustomerId);
        if (customer != null)
        {
            Console.Clear();
            DisplayCustomer(customer);
            return true;
        }
        else
        {
            System.Console.WriteLine("Customer was not found.");
            return false;
        }
    }

    private Customer GetCustomerFromDb(int userInputCustomerId)
    {
        return _cRepo.GetCustomer(userInputCustomerId);
    }

    private void ViewAllCustomers()
    {
        Console.Clear();
        ShowAllCustomers();
        PressAnyKey();
    }

    private void ShowAllCustomers()
    {
        Console.Clear();
        System.Console.WriteLine("List of Customers\n"+
        "====================");
        List<Customer> customerInDb = _cRepo.GetCustomers();
        ConfirmCustomerDb(customerInDb);
    }

    private void ConfirmCustomerDb(List<Customer> customerInDb)
    {
        if (customerInDb.Count() > 0)
        {
            Console.Clear();
            foreach (Customer customer in customerInDb)
            {
                DisplayCustomer(customer);
            }
        }
        else
        {
            System.Console.WriteLine("There are no customers in the system.");
        }
    }

    private void DisplayCustomer(Customer customer)
    {
        System.Console.WriteLine(customer);
    }

    private void DeleteDelivery()
    {
        Console.Clear();
        ShowAllDeliveries();

        try
        {
            System.Console.WriteLine("Select the Delivery Number you would like to remove.");

            int userInputDeliveryDelete = int.Parse(Console.ReadLine()!);

            var isConfirmed = ConfirmDeliveryInDb(userInputDeliveryDelete);

            if (isConfirmed)
            {
                if (_dRepo.DeleteDelivery(userInputDeliveryDelete))
                {
                    System.Console.WriteLine("Delivery was deleted.");
                }
                else
                {
                    System.Console.WriteLine("Delivery was deleted.");                    
                }
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }

        PressAnyKey();
    }

    private void UpdateDelivery()
    {
        Console.Clear();
        ShowAllDeliveries();
        try
        {
            System.Console.WriteLine("Select the Delivery Number you would like to update.");

            int userInputDeliveryID = int.Parse(Console.ReadLine()!);

            Delivery deliveryInDb = GetDeliveryFromDb(userInputDeliveryID);

            bool isConfirmed = ConfirmDeliveryInDb(deliveryInDb.ID);

            if (isConfirmed)
            {
                Delivery updatedDelivery = SetupDelivery();

                if (_dRepo.UpdateDelivery(deliveryInDb.ID, updatedDelivery))
                {
                    System.Console.WriteLine($"Order Number {updatedDelivery.ID} has been updated.");
                }
                else
                {
                    System.Console.WriteLine($"Order Number {updatedDelivery.ID} has not been updated.");                    
                }
            }
            else
            {
                System.Console.WriteLine("Delivery not found.");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }

        PressAnyKey();
    }

    private void AddDelivery()
    {
        Console.Clear();

        try
        {
            Delivery delivery = SetupDelivery();
            if (_dRepo.AddDelivery(delivery))
            {
                System.Console.WriteLine($"Delivery Number {delivery.ID} of {delivery.ItemQuantity} {delivery.ItemDescription} has been added.\n"+
                                        $"Current Status of the order: {delivery.OrderStatus}.");
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);            
            SomethingWentWrong();
        }

        PressAnyKey();
    }

    private Delivery SetupDelivery()
    {
        Delivery delivery = new Delivery();

        System.Console.WriteLine("Please enter Order Information.");

        //? Need to figure this out
        System.Console.WriteLine("When is the Order Date? YYYY/MM/DD");
        delivery.OrderDate = DateTime.Parse(Console.ReadLine()!);

        System.Console.WriteLine("When does it need to be delivered? YYYY/MM/DD");
        delivery.DeliveryDate = DateTime.Parse(Console.ReadLine()!);

        System.Console.WriteLine("What is the item being delivered?");
        delivery.ItemDescription = Console.ReadLine()!;

        System.Console.WriteLine("How many items did they order?");
        delivery.ItemQuantity = int.Parse(Console.ReadLine()!);

        System.Console.WriteLine("What is the current delivery status?\n" +
                                "Scheduled, EnRoute, Complete, Canceled");
        string userInputDelStatus = Console.ReadLine()!.ToLower();


        switch (userInputDelStatus.ToLower())
        {
            case "scheduled":
                delivery.OrderStatus = "Scheduled";
                break;
            case "enroute":
                delivery.OrderStatus = "EnRoute";
                break;
            case "complete":
                delivery.OrderStatus = "Complete";
                break;
            case "canceled":
                delivery.OrderStatus = "Canceled";
                break;
            default:
                SomethingWentWrong();
                break;

        }

        return delivery;
    }

    private void ViewDeliveryByID()
    {
        Console.Clear();
        ShowAllDeliveries();
        System.Console.WriteLine("====================");

        try
        {
            System.Console.WriteLine("Please enter Order Number.");
            int userInputOrderNumber = int.Parse(Console.ReadLine()!);
            ConfirmDeliveryInDb(userInputOrderNumber);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }

        PressAnyKey();
    }

    private bool ConfirmDeliveryInDb(int userInputOrderNumber)
    {
        Delivery delivery = GetDeliveryFromDb(userInputOrderNumber);
        if (delivery != null)
        {
            Console.Clear();
            DisplayDelivery(delivery);
            return true;
        }
        else
        {
            System.Console.WriteLine("Delivery was not found.");
            return false;
        }
    }

    private Delivery GetDeliveryFromDb(int userInputOrderNumber)
    {
        return _dRepo.GetDelivery(userInputOrderNumber);
    }

    private void ViewAllDeliveries()
    {
        Console.Clear();
        ShowAllDeliveries();
        PressAnyKey();
    }

    private void ShowAllDeliveries()
    {
        Console.Clear();
        System.Console.WriteLine("List of Deliveries\n" +
        "=====================");
        List<Delivery> delInDb = _dRepo.GetAllDeliveries();
        ConfirmDeliveryDb(delInDb);
    }

    private void ConfirmDeliveryDb(List<Delivery> delInDb)
    {
        if (delInDb.Count() > 0)
        {
            Console.Clear();
            foreach (Delivery delivery in delInDb)
            {
                DisplayDelivery(delivery);
            }
        }
        else
        {
            System.Console.WriteLine("There are no deliveries in the system.");
        }
    }

    private void DisplayDelivery(Delivery delivery)
    {
        System.Console.WriteLine(delivery);
    }

    private void SomethingWentWrong()
    {
        System.Console.WriteLine("Something went wrong. Please try again.");
    }

    private void PressAnyKey()
    {
        System.Console.WriteLine("Press any key to continue.");
        Console.ReadLine();
    }
}
