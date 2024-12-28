// See https://aka.ms/new-console-template for more information
using Evolution.Client.CSharp;
using Evolution.Client.CSharp.Models.Instance.Create;
using Evolution.Client.CSharp.Models.Message.SendText;

Console.WriteLine("Hello, World!");

var evo = new EvolutionClient("https://00-servicos-service-evolution.a5m7vd.easypanel.host", "5CK99Gjz48P4ec1hXW60");
/*var request = new RequestCreateInstance() { instanceName = "x" };
await evo.Instances.CreateInstance(request);*/

await evo.Messages.SendText("Elias-Teste", new RequestMessage() { Number = "5561991866977", Text = "Hello, World!" });