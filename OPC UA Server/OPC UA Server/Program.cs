using Opc.UaFx;
using Opc.UaFx.Server;

var temperatureNode = new OpcDataVariableNode<double>("Temperature", 100.0);

using (var server = new OpcServer("opc.tcp://localhost:4841/", temperatureNode))
{
    server.Start();

    while (true)
    {
        if (temperatureNode.Value == 110)
            temperatureNode.Value = 100;
        else
            temperatureNode.Value++;

        temperatureNode.ApplyChanges(server.SystemContext);
        Thread.Sleep(1000);
    }
}