Mediator mediator = new MediatorImpl();
Teacher teacher = new("Ms. Teacher", UserType.Teacher, mediator);
mediator.AddUser(teacher);

Student student1 = new ("John", UserType.Student, mediator);
Student student2 = new ("Julia", UserType.Student, mediator);
Student student3 = new ("Adam", UserType.Student, mediator);

mediator.AddUser(student1);
mediator.AddUser(student2);
mediator.AddUser(student3);

teacher.AskQuestion("What's 9 + 1?");
student1.AnswerQuestion("11");
student2.AnswerQuestion("91");
student3.AnswerQuestion("10");
teacher.TellAnswer("The answer is 10");

public enum UserType
{
    Student,
    Teacher,
}

public abstract class Mediator
{
    public abstract void BroadcastMessage(Colleague sender, string message);
    public abstract void AddUser(Colleague user);
}

public class Colleague
{
    public string Name { get; set; }

    public UserType UserType { get; set; }

    protected Mediator Mediator { get; set; }

    public Colleague(string name, UserType userType, Mediator mediator)
    {
        Name = name;
        UserType = userType;
        Mediator = mediator;
    }

    public void SendMessage(string message)
    {
        Mediator.BroadcastMessage(this, $"{Name}: {message}");
    }

    public void ReceiveMessage(string message)
    {
        Console.WriteLine($"{Name} received a message: {message}");
    }

}

public class Student : Colleague
{
    public void AnswerQuestion(string answer)
    {
        SendMessage($"My answer is {answer}");
    }

    public Student(string name, UserType userType, Mediator mediator) : base(name, userType, mediator)
    {
    }
}

public class Teacher : Colleague
{
    public void TellAnswer(string answer)
    {
        SendMessage($"The answer is {answer}");
    }

    public void AskQuestion(string question)
    {
        SendMessage(question);
    }

    public Teacher(string name, UserType userType, Mediator mediator) : base(name, userType, mediator)
    {
    }
}

public class MediatorImpl : Mediator
{
    private Colleague _teacher;
    private readonly List<Colleague> _students = new();

    public override void BroadcastMessage(Colleague sender, string message)
    {
        switch (sender.UserType)
        {
            case UserType.Student:
                _teacher.ReceiveMessage(message);
                break;
            case UserType.Teacher:
                foreach (Colleague student in _students)
                {
                    student.ReceiveMessage(message);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void AddUser(Colleague user)
    {
        switch (user.UserType)
        {
            case UserType.Teacher:
                _teacher = user;
                break;
            case UserType.Student:
                _students.Add(user);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
