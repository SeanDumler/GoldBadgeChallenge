
public class DeliveryRepository
{
    private List<Delivery> _deliveryDb = new List<Delivery>();

    private int _count = 0;

    //Create

    public bool AddDelivery(Delivery delivery)
    {
        if(delivery is null)
        {
            return false;
        }
        else
        {
            _count++;
            delivery.ID = _count;
            _deliveryDb.Add(delivery);
            return true;
        }
    }

    //Read All

    public List<Delivery> GetAllDeliveries()
    {
        return _deliveryDb;
    }

    //Read by ID

    public Delivery GetDelivery(int deliveryID)
    {
        return _deliveryDb.SingleOrDefault(d => d.ID == deliveryID)!;
    }

    //Update

    public bool UpdateDelivery(int deliveryID, Delivery updatedDeliveryInfo)
    {
        var oldDeliveryInfo = GetDelivery(deliveryID);
        if (oldDeliveryInfo != null)
        {
            oldDeliveryInfo.OrderDate = updatedDeliveryInfo.OrderDate;
            oldDeliveryInfo.DeliveryDate = updatedDeliveryInfo.DeliveryDate;
            oldDeliveryInfo.OrderStatus = updatedDeliveryInfo.OrderStatus;
            oldDeliveryInfo.ItemDescription = updatedDeliveryInfo.ItemDescription;
            oldDeliveryInfo.ItemQuantity = updatedDeliveryInfo.ItemQuantity;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Delete

    public bool DeleteDelivery(int deliveryID)
    {
        return _deliveryDb.Remove(GetDelivery(deliveryID));
    }
}
