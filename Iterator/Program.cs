WestServer server = new WestServer();
server.Players.Add(new Player("John", false));
server.Players.Add(new Player("James", true));
server.Players.Add(new Player("Adam", true));
server.Players.Add(new Player("Julia", false));
server.Players.Add(new Player("Kevin", true));

AlivePlayerIterator aliveIterator = server.CreateAlivePlayerIterator();
Console.WriteLine("Alive players");
Player? player;
while ((player = aliveIterator.GetNext()) != null)
{
    Console.WriteLine(player.Name);
}

DeadPlayerIterator deadIterator = server.CreateDeadPlayerIterator();
Console.WriteLine("Dead players");
while ((player = deadIterator.GetNext()) != null)
{
    Console.WriteLine(player.Name);
}

public interface Server
{
    AlivePlayerIterator CreateAlivePlayerIterator();

    DeadPlayerIterator CreateDeadPlayerIterator();

}

public class Player
{
    public Player(string name, bool alive)
    {
        Name = name;
        Alive = alive;
    }

    public string Name { get; set; }
    public bool Alive { get; set; }
}

public class WestServer : Server
{
    public List<Player?> Players = new();

    public AlivePlayerIterator CreateAlivePlayerIterator()
    {
        return new AlivePlayerIterator(this);
    }

    public DeadPlayerIterator CreateDeadPlayerIterator()
    {
        return new DeadPlayerIterator(this);
    }

}

public interface PlayerIterator
{
    Player? GetFirst();

    bool HasNext();

    Player? GetNext();

    Player? GetCurrent();

}

public class DeadPlayerIterator : PlayerIterator
{
    private readonly WestServer _server;
    private int _index = 0;

    public DeadPlayerIterator(WestServer server)
    {
        _server = server;
    }

    public Player? GetFirst()
    {
        for (int i = 0; i < _server.Players.Count; i++)
        {
            if (!_server.Players[i].Alive)
            {
                _index = i;
                return _server.Players[_index];
            }
        }

        return null;
    }

    public bool HasNext()
    {
        var player = _server.Players.Skip(_index + 1).FirstOrDefault(player => !player.Alive);
        return player != null;
    }

    public Player? GetNext()
    {
        Player? player = _server.Players.Skip(_index + 1).FirstOrDefault(player => !player.Alive);
        if (player != null)
            _index = _server.Players.IndexOf(player);
        return player;
    }

    public Player? GetCurrent()
    {
        return _server.Players[_index];
    }
}

public class AlivePlayerIterator : PlayerIterator
{
    private readonly WestServer _server;
    private int _index = -1;

    public AlivePlayerIterator(WestServer server)
    {
        _server = server;
    }

    public Player? GetFirst()
    {
        for (int i = 0; i < _server.Players.Count; i++)
        {
            if (_server.Players[i].Alive)
            {
                _index = i;
                return _server.Players[_index];
            }
        }

        return null;
    }

    public bool HasNext()
    {
        var player = _server.Players.Skip(_index + 1).FirstOrDefault(player => player.Alive);
        return player != null;
    }

    public Player? GetNext()
    {
        Player? player = _server.Players.Skip(_index + 1).FirstOrDefault(player => player.Alive);
        if(player != null)
            _index = _server.Players.IndexOf(player);
        return player;
    }

    public Player? GetCurrent()
    {
        return _server.Players[_index];
    }
}
