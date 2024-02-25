using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.DAQmx;
using Opc.UaFx.Client;

namespace WinForm_OPC_UA_Client
{
    public partial class Form1 : Form
    {
        private double temp = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void main()
        {
            // Method for reading sensor data and calculating temperature

            double temp, voltage, resistance;
            NationalInstruments.DAQmx.Task analogInTask = new NationalInstruments.DAQmx.Task();
            AIChannel myAIChannel;
            myAIChannel = analogInTask.AIChannels.CreateVoltageChannel(
            "dev2/ai0",
            "myAIChannel",
            AITerminalConfiguration.Rse,
            0,
            5,
            AIVoltageUnits.Volts
            );
            AnalogSingleChannelReader reader = new AnalogSingleChannelReader(analogInTask.Stream);
            
            voltage = reader.ReadSingleSample();
            resistance = (voltage * 10000) / (5 - voltage);
            temp = 1 / (0.001129148 + 0.000234125 * Math.Log(resistance) + 0.0000000876741 * Math.Pow(Math.Log(resistance), 3)) -273.15;
            txtTemp.Text = temp.ToString("0.##");
            WriteToServer(temp);
            Thread.Sleep(1000);

        }
        private void WriteToServer(double writeTemp)
        {
            // Method for writhing the sensor data to the server
            using (var client = new OpcClient("opc.tcp://localhost:4840"))
            {
                client.Connect();
                client.WriteNode("ns=2;s=Temperature", writeTemp);
                client.Disconnect();
            }

        }

        private void btnTemp_Click(object sender, EventArgs e)
        {
            // Enable and dissable timer
            if (tmr1.Enabled)
            {
                tmr1.Stop();
                btnTemp.Text = "Read";
            }
            else
            {
                tmr1.Start();
                btnTemp.Text = "Stop reading";
            }
        }

        private void tmr1_Tick(object sender, EventArgs e)
        {
            //Timer for collecting data and sending to server each time step
            main();
        }
    }
}
