namespace Application.DTOs
{
    public class PostPetsDTO
    {
        public PostPetsDTO()
        {

        }

        public PostPetsDTO(int id, string name, string address, int zip, string city, string? email, string dogBreeds, int price, string description)
        {
            Id = id;
            Name = name;
            Address = address;
            Zipcode = zip;
            City = city;
            Email = email;
            DogBreeds = dogBreeds;
            Price = price;
            Description = description;
        }
        public PostPetsDTO(string name, string address, int zip, string city, string? email, string dogBreeds, int price, string description)
        {
            Name = name;
            Address = address;
            Zipcode = zip;
            City = city;
            Email = email;
            DogBreeds = dogBreeds;
            Price = price;
            Description = description;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string DogBreeds { get; set; }
        public string? Address { get; set; }
        public int Zipcode { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
    }


    public class PutPetsDTO
    {
            public int? Price { get; set; }
            public string? Name { get; set; }
            //public string? Image { get; set; }
            public string? DogBreeds { get; set;}
            public string? Description { get; set;}
            public int Id { get; set; }
    }
}
