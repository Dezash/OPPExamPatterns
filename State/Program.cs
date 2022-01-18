// See https://aka.ms/new-console-template for more information
IState idleState = new Idle();
IState searchState = new Searching();
IState chaseState = new Chasing();
IState fightState = new Fighting();
Context context = new(idleState);
context.Speak();
context.CurrentState = searchState;
context.Speak();


public interface IState
{
    void Speak();

    void PlayAnimation();

    void Act();

}

public class Context
{
    public IState CurrentState { get; set; }

    private List<IState> _npcStates = new();


    public Context(IState initialState)
    {
        CurrentState = initialState;
    }

    public void Speak()
    {
        CurrentState.Speak();
    }

    public void PlayAnimation()
    {
        CurrentState.PlayAnimation();
    }

    public void Act()
    {
        CurrentState.Act();
    }
}

public class Searching : IState
{
    public void Speak()
    {
        Console.WriteLine("Where are you?");
    }

    public void PlayAnimation()
    {
        Console.WriteLine("Search animation");
    }

    public void Act()
    {
        Console.WriteLine("Searching...");
    }
}

public class Idle : IState
{
    public void Speak()
    {
        Console.WriteLine("Shall we gather for whiskey and cigars tonight?");
    }

    public void PlayAnimation()
    {
        Console.WriteLine("Idle animation");
    }

    public void Act()
    {
        Console.WriteLine("Walking around...");
    }
}

public class Fighting : IState
{
    public void Speak()
    {
        Console.WriteLine("Take this!");
    }

    public void PlayAnimation()
    {
        Console.WriteLine("Swining animation");
    }

    public void Act()
    {
        Console.WriteLine("Attacking...");
    }
}

public class Chasing : IState
{
    public void Speak()
    {
        Console.WriteLine("Never should have come here!");
    }

    public void PlayAnimation()
    {
        Console.WriteLine("Chasing animation...");
    }

    public void Act()
    {
        Console.WriteLine("Chasing...");
    }
}
