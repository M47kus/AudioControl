﻿using System.IO.Ports;
using NAudio.CoreAudioApi;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public static class SerializeAndDeserialize
{
    public static string Serialize<T>(T obj)
    {
        var serializer = new SerializerBuilder().Build();
        return serializer.Serialize(obj);
    }
    public static T Deserialize<T>(string yaml)
    {
        var deserializer = new DeserializerBuilder().Build();
        return deserializer.Deserialize<T>(yaml);
    }
}

class Program
{

    bool applyChanges = true;
    bool getdevice = true;
    public SerialPort serialPort = new SerialPort();
    public MMDeviceEnumerator enumerator = new MMDeviceEnumerator();

    public YamlMappingNode yamlMap = new YamlMappingNode();

    public static void Main()
                => new Program().MainInterface();

    public void MainInterface()
    {

        //print audio devices
        if (getdevice)
        {
            foreach (var endpoint in enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active))
            {
                Console.WriteLine(endpoint.FriendlyName);
                Console.WriteLine(endpoint.ID);
            }
        }

        yamlMap = readMapFile(@"C:\Users\Schre\Documents\IdeaProjects\AudioControl\AudioControl\config.yaml");

        var portNames = SerialPort.GetPortNames();
        foreach (var port in portNames)
        {
            if (port == "COM8")
            {
                serialPort.PortName = "COM8";
                serialPort.BaudRate = 9600;
                serialPort.DtrEnable = true;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialDataRecieved);
                serialPort.Open();

                Console.ReadKey();
                serialPort.Close();
            }
        }

    }

    public void SerialDataRecieved(object sender, SerialDataReceivedEventArgs e)
    {
        string inData = serialPort.ReadLine();

        Console.WriteLine(inData);

        string[] tokens = inData.Split(';');
        foreach (string token in tokens)
        {
            Console.WriteLine(token);

            string[] items = token.Split(':');
            foreach (string item in items)
            {

                foreach (var entry in yamlMap.Children)
                {
                    // Console.WriteLine((YamlScalarNode)entry.Key);
                    // Console.WriteLine((YamlScalarNode)entry.Value);

                    string key = (string)(YamlScalarNode)entry.Key;

                    if (key == items[0])
                    {
                        //ApplySoundChanges((string)(YamlScalarNode)entry.Value, float.Parse(items[1]), bool.Parse(items[2]));
                    }

                }

            }

        }

    }

    public void ApplySoundChanges(String id, float value, bool mute)
    {
        if (applyChanges)
        {
            foreach (var endpoint in enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active))
            {

                if (endpoint.ID == id)
                {
                    endpoint.AudioEndpointVolume.MasterVolumeLevelScalar = value / 100;
                    endpoint.AudioEndpointVolume.Mute = mute;
                }
            }
        }
    }

    public YamlMappingNode readMapFile(string filepath)
    {
        using (var reader = new StreamReader(filepath))
        {
            // Load the stream
            var yaml = new YamlDotNet.RepresentationModel.YamlStream();
            yaml.Load(reader);

            // Examine the stream
            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            return mapping;
        }
    }
}
