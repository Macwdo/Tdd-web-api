namespace TddWebApi.Models;

public class Address
{
    public string Street { get; set; } = string.Empty!;
    public string City { get; set; } = string.Empty!;
    public int Number { get; set; }
}