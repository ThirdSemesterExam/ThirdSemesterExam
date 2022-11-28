namespace Application.DTOs
{
    public class PostPetsDTO
    {
        public string Name { get; set; }
        //public string Image { get; set; }
        public string DogBreeds { get; set;}
        public int Price { get; set;}
        public string Description { get; set;}
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
