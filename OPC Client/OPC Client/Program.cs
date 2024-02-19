using Opc.UaFx.Client;
using System.Threading;

using (var client = new OpcClient("opc.tcp://localhost:4841"))
{
    client.Connect();
    while (true)
    {
        var temperature = client.ReadNode("ns=2;s=Temperature");
        Console.WriteLine("Current Temperature is {0} °C", temperature);
        Thread.Sleep(1000);
    }
    
}