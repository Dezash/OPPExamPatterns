Image image1 = new("image1");

Console.WriteLine("Protection proxy (incorrect password):");
IImage adminImageBad = new ImageProtectionProxy(image1, "admin", "badpassword");
adminImageBad.DrawImage();

Console.WriteLine("Protection proxy (correct password):");
IImage adminImageGood = new ImageProtectionProxy(image1, "admin", "mypassword");
adminImageGood.DrawImage();

Console.WriteLine("Smart proxy (logging):");
IImage smartProxy = new ImageSmartProxy(image1);
smartProxy.DrawImage();

Console.WriteLine("Virtual proxy (lazy-load):");
IImage virtualProxy = new ImageVirtualProxy("image2");
virtualProxy.DrawImage();

public interface IImage
{
    public string Name { get; set; }
    public void DrawImage();
}

public class Image : IImage
{
    public string Name { get; set; }
    private readonly byte[] _imageData;

    public Image(string name)
    {
        Name = name;
        _imageData = new byte[50];
        Random rnd = new();
        rnd.NextBytes(_imageData);
    }

    public void DrawImage()
    {
        Console.WriteLine(_imageData.ToString());
    }
}

public class ImageProtectionProxy : IImage
{
    private readonly Image _image;
    public string Name { get; set; }

    private readonly string _username;
    private readonly string _password;

    public ImageProtectionProxy(Image image, string username, string password)
    {
        _image = image;
        _username = username;
        _password = password;
        Name = image.Name;
    }

    public void DrawImage()
    {
        if (_username == "admin" && _password == "mypassword")
        {
            _image.DrawImage();
        }
        else
            Console.WriteLine("Access denied!");
    }
}

public class ImageSmartProxy : IImage
{
    private readonly Image _image;
    public string Name { get; set; }

    public ImageSmartProxy(Image image)
    {
        _image = image;
        Name = image.Name;
    }


    public void DrawImage()
    {
        LogImageDrawRequest();
        _image.DrawImage();
    }

    private void LogImageDrawRequest()
    {
        Console.WriteLine($"Drawing image {_image.Name}");
    }
}

public class ImageVirtualProxy : IImage
{
    private Image? _image;
    public string Name { get; set; }

    public ImageVirtualProxy(string name)
    {
        Name = name;
    }

    public void DrawImage()
    {
        _image ??= new Image(Name);
        _image.DrawImage();
    }
}
