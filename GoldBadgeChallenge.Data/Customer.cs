
public class Customer
{
    public Customer(){}

    public Customer(int id, string name, List<Delivery> deliveries)
    {
        Name = name;
        Deliveries = deliveries;
    }

    public int ID { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public override string ToString()
    {
        var str = $"Customer Name: {Name}\n" +
                  $"List of Orders:\n";
        foreach (Delivery delivery in Deliveries)
        {
            str += delivery + "\n";
        }
        
        return str;
    }
}