// See https://aka.ms/new-console-template for more information

using DoNEtBatch14MTZO.MiniATM.ConsoleApp;
Console.WriteLine("Hello, World!");

MainService mainService = new MainService();
await mainService.RunAsync();
Console.ReadLine();
