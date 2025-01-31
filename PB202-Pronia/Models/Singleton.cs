namespace PB202_Pronia.Models;

public class Singleton
{
    public int Count { get; set; }

    public void Increase()
    {
        Count++;
    }
}



public class Transient
{
    public int Count { get; set; }

    public void Increase()
    {
        Count++;
    }
}


public class Scoped
{
    public int Count { get; set; }

    public void Increase()
    {
        Count++;
    }
}