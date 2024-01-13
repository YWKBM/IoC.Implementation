using System.Diagnostics.Contracts;
using Demo;
using IoC.Implementation;

var services = new ServiceCollection();

services.AddSingleton<ExternalDependency>();
services.AddSingleton<Example>();
services.AddTransient<TransientExample>();

var serviceProvider = services.Build();
var exDependency = serviceProvider.GetService<ExternalDependency>();
var example = serviceProvider.GetService<Example>();

exDependency.Print();
example.Print();

var transientExample1 = serviceProvider.GetService<TransientExample>();
transientExample1.ShowCounter();

var transientExample2 = serviceProvider.GetService<TransientExample>();
transientExample2.ShowCounter();


var transientExample3 = serviceProvider.GetService<TransientExample>();
transientExample3.ShowCounter();



Console.ReadLine();
