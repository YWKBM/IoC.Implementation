using Demo;
using IoC.Implementation;

var services = new ServiceCollection();

services.AddSingleton<Example>();

var serviceProvider = services.Build();
var example = serviceProvider.GetService<Example>();

example.Print();

Console.ReadLine();