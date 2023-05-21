
namespace GBC.Repository.Tests
{
    public class DeliveryRepoTest
    {
        private DeliveryRepository _delRepo;
        public DeliveryRepoTest()
        {
            var order1 = new Delivery
            {
                OrderDate = DateTime.Today,
                DeliveryDate = DateTime.Today,
                OrderStatus = "EnRoute",
                ItemDescription = "board games",
                ItemQuantity = 2
            };

            var order2 = new Delivery
            {
                OrderDate = DateTime.Today,
                DeliveryDate = DateTime.Today,
                OrderStatus = "Scheduled",
                ItemDescription = "tcg games",
                ItemQuantity = 4
            };

            _delRepo = new DeliveryRepository();

            _delRepo.AddDelivery(order1);
            _delRepo.AddDelivery(order2);
        }


        [Fact]
        public void AddDelivery_ShouldReturnTrue()
        {
            // Arrange
            Delivery deliveryInfo = new Delivery()
            {
                OrderDate = DateTime.Today,
                DeliveryDate = DateTime.Today,
                OrderStatus = "Delivered",
                ItemDescription = "miniatures",
                ItemQuantity = 2
            };

            // Act
            bool isSucccess = _delRepo.AddDelivery(deliveryInfo);

            // Assert
            Assert.True(isSucccess);
        }

        [Fact]
        public void GetAllDeliveries_ShouldReturnCorrectCount()
        {
            // Given
            
            // When
            int expected = 2;

            int actual = _delRepo.GetAllDeliveries().Count();

            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDelivery_ShouldReturnCorrectDelivery()
        {
            // Given
        
            // When
            var deliveryInDbItemDescription = "board games";
            var actualItemDescription = _delRepo.GetDelivery(1).ItemDescription;
        
            // Then
            Assert.Equal(deliveryInDbItemDescription, actualItemDescription);
        }

        [Fact]
        public void UpdateDelivery_ShouldUpdateCorrectDelivery()
        {
            // Given
            Delivery updatedDeliveryInfo = new Delivery();
            updatedDeliveryInfo.OrderDate = DateTime.Today;
            updatedDeliveryInfo.DeliveryDate = DateTime.Today;
            updatedDeliveryInfo.OrderStatus = "Canceled";
            updatedDeliveryInfo.ItemDescription = "rpg games";
            updatedDeliveryInfo.ItemQuantity = 2;

            // When
            int deliveryIdToUpdate = 1;

            bool isSucccess = _delRepo.UpdateDelivery(deliveryIdToUpdate, updatedDeliveryInfo);
            
            // Then
            Assert.True(isSucccess);
        }

        [Fact]
        public void DeleteDelivery_ShouldReturnTrue()
        {
            // Given

            // When
            bool isSucccess = _delRepo.DeleteDelivery(2);

            int expectedCount = 1;

            int actualCount = _delRepo.GetAllDeliveries().Count();

            // Then
            Assert.True(isSucccess);
            Assert.Equal(expectedCount, actualCount);
        }
    }
}