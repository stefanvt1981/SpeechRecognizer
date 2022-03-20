
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

ConsoleKeyInfo currentKey;

while (true) {
    currentKey = Console.ReadKey(true);

    bool eventSent = false;

    while (currentKey.Key == Console.ReadKey(true).Key)
    {        
        if(!eventSent)
        {
            Console.WriteLine($"Send {currentKey.Key} key");
            eventSent = true;
        }
        else if (currentKey.Key != Console.ReadKey(true).Key && eventSent)
        {
            break;
        }        
    }
    Console.WriteLine($"Send {currentKey.Key} key stop pressed");
}
