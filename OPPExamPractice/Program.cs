AbstractClass templateobject = new ConcreteClass();
templateobject.TemplateMethod();

public abstract class AbstractClass
{
    public void TemplateMethod()
    {
        if (HookOperation())
        {
            PrimitiveOperation();
        }

        PrimitiveOperation2();
    }

    protected virtual void PrimitiveOperation()
    {
        Console.WriteLine("First primitive operation");
    }

    protected virtual void PrimitiveOperation2()
    {
        Console.WriteLine("Second primitive operation");
    }

    protected virtual bool HookOperation()
    {
        return false;
    }
}

public class ConcreteClass : AbstractClass
{
    protected override void PrimitiveOperation()
    {
        Console.WriteLine("overriden first operation");
    }

    protected override void PrimitiveOperation2()
    {
        Console.WriteLine("overriden second operation");
    }

    protected override bool HookOperation()
    {
        return true;
    }
}
