
public class Delivery
{
    public Delivery(){}

    public Delivery(DateTime orderDate, DateTime deliveryDate, string orderStatus, string itemDescription, int itemQuantity)
    {
        OrderDate = orderDate;
        DeliveryDate = deliveryDate;
        OrderStatus = orderStatus;
        ItemDescription = itemDescription;
        ItemQuantity = itemQuantity;
    }

    public int ID { get; set; }
    public DateTime OrderDate { get; set; } 
    public DateTime DeliveryDate { get; set; } 
    public string OrderStatus { get; set; } = string.Empty; //* Scheduled, EnRoute, Complete, Canceled
    public string ItemDescription { get; set; } = string.Empty;
    public int ItemQuantity { get; set; }

    public override string ToString()
    {
        var str = $"Order Number: {ID}\n" +
                  $"Order Date: {OrderDate}\n" +
                  $"Delivery Date: {DeliveryDate}\n" +
                  $"Order Status: {OrderStatus}\n" +
                  $"Item Number: {ItemDescription}\n" +
                  $"Item Quantity: {ItemQuantity}";
        
        return str;
    }
}
