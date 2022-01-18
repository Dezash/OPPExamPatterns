Element elementOne = new ElementOne();
Element elementTwo = new ElementTwo();
Visitor visitorOne = new VisitorOne();
Visitor visitorTwo = new VisitorTwo();
elementOne.Accept(visitorOne);
elementOne.Accept(visitorTwo);
elementTwo.Accept(visitorOne);
elementTwo.Accept(visitorTwo);

public abstract class Element
{
    public abstract void Accept(Visitor visitor);

}

public class ElementOne : Element
{
    public override void Accept(Visitor visitor)
    {
        visitor.GetFee(this);
    }
}
public class ElementTwo : Element
{
    public override void Accept(Visitor visitor)
    {
        visitor.GetFee(this);
    }

}

public interface Visitor
{
    decimal GetFee(ElementOne element);
    decimal GetFee(ElementTwo element);

}

public class VisitorOne : Visitor
{
    public decimal GetFee(ElementOne element)
    {
        Console.WriteLine("fee for element one from visitor one");
        return 10.5m;
    }

    public decimal GetFee(ElementTwo element)
    {
        Console.WriteLine("fee for element two from visitor one");
        return 12.5m;
    }
}

public class VisitorTwo : Visitor
{
    public decimal GetFee(ElementOne element)
    {
        Console.WriteLine("fee for element one from visitor two");
        return 9.5m;
    }

    public decimal GetFee(ElementTwo element)
    {
        Console.WriteLine("fee for element two from visitor two");
        return 13.5m;
    }
}
