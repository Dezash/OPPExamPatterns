GameObjectFactory _factory = new();
List<GameObject> _gameObjects = new()
{
    new("object1"),
    new("object2"),
    new("object3"),
    new("object1"),
    new("object2"),
    new("object3"),
    new("object1"),
    new("object2"),
    new("object3"),
    new("object1"),
    new("object2"),
    new("object3")
};

foreach(GameObject gameObject in _gameObjects)
{
    Console.WriteLine($"{gameObject.Name}: {_factory.GetGameObject(gameObject.Name).Data.Length}");
}

public class GameObject
{
    public string Name { get; set; }

    public GameObject(string name)
    {
        Name = name;
    }
}

public class GameObjectFactory
{
    private readonly Dictionary<string, GameObjectContext> _contextTypes = new();

    public GameObjectContext GetGameObject(string name)
    {
        if (_contextTypes.ContainsKey(name))
        {
            return _contextTypes[name];
        }

        Random rnd = new();
        byte[] bytes = new byte[rnd.Next(1, 100)];
        rnd.NextBytes(bytes);
        GameObjectContext context = new(bytes);
        _contextTypes[name] = context;
        return context;
    }
}

public class GameObjectContext
{
    public byte[] Data;

    public GameObjectContext(byte[] bytes)
    {
        Data = bytes;
    }
}
