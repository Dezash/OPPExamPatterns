ICoinSlot twoEuroSlot = new TwoEuroSlot();
ICoinSlot oneEuroSlot = new OneEuroSlot();
ICoinSlot fiftyCentsSlot = new FiftyCentsSlot();
ICoinSlot twentyCentsSlot = new TwentyCentsSlot();
ICoinSlot passThroughSlot = new PassThroughSlot();
twoEuroSlot.SetNext(oneEuroSlot);
oneEuroSlot.SetNext(fiftyCentsSlot);
fiftyCentsSlot.SetNext(twentyCentsSlot);
twentyCentsSlot.SetNext(passThroughSlot);

Console.WriteLine("Two euros:");
twoEuroSlot.HandleCoin(2.0m);
Console.WriteLine("One euro:");
twoEuroSlot.HandleCoin(1.0m);
Console.WriteLine("Fifty cents:");
twoEuroSlot.HandleCoin(0.5m);
Console.WriteLine("A button:");
twoEuroSlot.HandleCoin(0.0m);

public interface ICoinSlot
{
    void HandleCoin(decimal coin);

    void SetNext(ICoinSlot nextSlot);

}

public abstract class CoinSlot : ICoinSlot
{
    protected ICoinSlot? NextSlot;

    public abstract void HandleCoin(decimal coin);

    public void SetNext(ICoinSlot nextSlot)
    {
        NextSlot = nextSlot;
    }

}

public class FiftyCentsSlot : CoinSlot
{
    public override void HandleCoin(decimal coin)
    {
        Console.WriteLine("Checking FiftyCentsSlot");
        if (coin != 0.5m)
        {
            Console.WriteLine("Coin did not match!");
            NextSlot?.HandleCoin(coin);
            return;
        }

        Console.WriteLine("Coin matched!");
    }
}

public class OneEuroSlot : CoinSlot
{
    public override void HandleCoin(decimal coin)
    {
        Console.WriteLine("Checking OneEuroSlot");
        if (coin != 1.0m)
        {
            Console.WriteLine("Coin did not match!");
            NextSlot?.HandleCoin(coin);
            return;
        }

        Console.WriteLine("Coin matched!");
    }
}

public class TwentyCentsSlot : CoinSlot
{
    public override void HandleCoin(decimal coin)
    {
        Console.WriteLine("Checking TwentyCentsSlot");
        if (coin != 0.2m)
        {
            Console.WriteLine("Coin did not match!");
            NextSlot?.HandleCoin(coin);
            return;
        }

        Console.WriteLine("Coin matched!");
    }
}

public class TwoEuroSlot : CoinSlot
{
    public override void HandleCoin(decimal coin)
    {
        Console.WriteLine("Checking TwoEuroSlot");
        if (coin != 2.0m)
        {
            Console.WriteLine("Coin did not match!");
            NextSlot?.HandleCoin(coin);
            return;
        }

        Console.WriteLine("Coin matched!");
    }
}

public class PassThroughSlot : CoinSlot
{
    public override void HandleCoin(decimal coin)
    {
        Console.WriteLine("Coin returned!");
        NextSlot?.HandleCoin(coin);
    }
}
