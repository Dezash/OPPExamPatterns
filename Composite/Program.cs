// See https://aka.ms/new-console-template for more information
IComponent leafComposite = new Composite();
IComponent leaf1 = new Leaf("leaf1");
IComponent leaf2 = new Leaf("leaf2");
IComponent leaf3 = new Leaf("leaf3");
leafComposite.Add(leaf1);
leafComposite.Add(leaf2);
leafComposite.Add(leaf3);

IComponent leafComposite2 = new Composite();
leafComposite2.Add(new Leaf("upperLeaf"));
leafComposite2.Add(leafComposite);
leafComposite.Remove(leaf2);

leafComposite2.PrintName();

public interface IComponent
{
    public void Add(IComponent leaf);
    public void Remove(IComponent leaf);
    public List<IComponent> GetChildren();
    public void PrintName();
}

public class Leaf : IComponent
{
    public string Name { get; set; }

    public Leaf(string name)
    {
        Name = name;
    }

    public void PrintName()
    {
        Console.WriteLine(Name);
    }

    public void Add(IComponent leaf)
    {
        throw new NotImplementedException();
    }

    public void Remove(IComponent leaf)
    {
        throw new NotImplementedException();
    }

    public List<IComponent> GetChildren()
    {
        throw new NotImplementedException();
    }
}

public class Composite : IComponent
{
    private readonly List<IComponent> _leaves;

    public Composite()
    {
        _leaves = new List<IComponent>();
    }

    public void Add(IComponent leaf)
    {
        _leaves.Add(leaf);
    }

    public void Remove(IComponent leaf)
    {
        _leaves.Remove(leaf);
    }

    public List<IComponent> GetChildren()
    {
        return _leaves;
    }

    public void PrintName()
    {
        foreach (IComponent leaf in _leaves)
        {
            leaf.PrintName();
        }
    }
}
