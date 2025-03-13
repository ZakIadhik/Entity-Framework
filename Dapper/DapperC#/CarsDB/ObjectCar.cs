namespace CarsDB
{
    public class ObjectCar
    {
        public int Id {get; set;}
        public string Brand {get; set;}
        public string Model {get; set;}
        public int Year {get; set;}
        public decimal Price {get; set;}
        
        public string ToString()
        {
            return $"Id: {Id}, Brand: {Brand}, Model: {Model}, Year: {Year}, Price: {Price}";
        }
    }
}




