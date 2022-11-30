namespace Application.DTOs
{
    public class PostPetsDTO
    {
     
    }


    public class PartialUpdatePetsDTO
    {
            public int? Price { get; set; }
            public string? Name { get; set; }
            //public string? Image { get; set; }
            public string? DogBreeds { get; set;}
            public string? Description { get; set;}
            public int Id { get; set; }
    }
}
