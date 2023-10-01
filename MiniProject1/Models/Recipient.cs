namespace MiniProject1.Models;

public class Recipient
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Ip { get; set; }
    
    public Recipient(string name, string email, string ip)
    {
        Name = name;
        Email = email;
        Ip = ip;
    }
}