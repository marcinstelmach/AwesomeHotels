using System.Reflection;
using AwesomeHotels.Services.Users.Db.Migrations;
using CommandLine;
using DbUp;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(UpdateDatabase);

static void UpdateDatabase(Options options)
{
    var connectionString = options.ConnectionString
                           ?? "Server=localhost; Database=AwesomeHotels.Users; Trusted_connection=true";

    var engine = DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .Build();


    var result = engine.PerformUpgrade();

    if (!result.Successful)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(result.Error);
        Console.ResetColor();
#if DEBUG
        Console.ReadLine();
#endif
        return;
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Success!");
    Console.ResetColor();
    return;
}