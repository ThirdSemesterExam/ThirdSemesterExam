namespace Domain;

public class Pets
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string DogBreeds { get; set; }
    public string? Address { get; set; }
    public int Zipcode { get; set; }
    public string? City { get; set; }
    public string? Email { get; set; }
    public Pets() { }

    
    public Pets(int id, string name, string address, int zip, string city, string? email, string dogBreeds, int price, string description)
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

    public Pets(int id, string name, string address, int zip, string city, string dogBreeds, int price, string description)
        : this(id, name, address, zip, city, null, dogBreeds, price, description) { }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        Pets other = (Pets)obj;
        return this.Id == other.Id;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}