Originator originator = new("", "pass123");
Caretaker caretaker = new();

while (true)
{
    string input = Console.ReadLine();
    if (input == null)
        return;

    if (input == "undo")
        caretaker.Undo(originator);
    else
    {
        caretaker.WriteText(originator, input);
    }

    originator.PrintText();
}

public class Originator
{
    private string _text;

    private readonly string _password;

    public Originator(string text, string password)
    {
        _text = text;
        _password = password;
    }

    public void Restore(Memento memento)
    {
        memento.SetState(this);
    }

    public Memento Save()
    {
        return new Memento(_text, _password);
    }

    public bool ValidatePassword(string password)
    {
        return _password == password;
    }

    public void WriteText(string text)
    {
        _text += text;
    }

    public void PrintText()
    {
        Console.WriteLine(_text);
    }

    public void SetState(string text)
    {
        _text = text;
    }
}

public class Memento
{
    private string state;

    private readonly string _password;

    public Memento(string state, string password)
    {
        this.state = state;
        this._password = password;
    }

    public void SetState(Originator originator)
    {
        if (originator.ValidatePassword(_password))
        {
            originator.SetState(state);
        }
    }

}

public class Caretaker
{
    private readonly List<Memento> _snapshots = new();


    public void WriteText(Originator originator, string text)
    {
        _snapshots.Add(originator.Save());
        originator.WriteText(text);
    }

    public void Undo(Originator originator)
    {
        originator.Restore(_snapshots.Last());
        _snapshots.RemoveAt(_snapshots.Count - 1);
    }
}
