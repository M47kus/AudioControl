using System.IO.Ports;
using System.Runtime.CompilerServices;
using NAudio.CoreAudioApi;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

class Program
{

    //default values
    bool applyChanges = true;
    bool getdevice = true;
    String comPort = "COM8";
    int BaudRate = 9600;
    String configPath = @"C:\Users\Schre\Documents\IdeaProjects\AudioControl\AudioControl\config.yaml";

    //global values

    bool portOpen = false;
    public SerialPort serialPort = new SerialPort();
    public MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
    public YamlMappingNode yamlMap = new YamlMappingNode();

    //Main Function
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

        Console.WriteLine("using config: " + configPath);
        yamlMap = readMapFile(configPath);

        //load values from config
        var portNames = SerialPort.GetPortNames();
        foreach (var entry in yamlMap.Children)
        {
            if ((string)(YamlScalarNode)entry.Key == "COM")
            {
                comPort = (string)(YamlScalarNode)entry.Value;
            }
            else if ((string)(YamlScalarNode)entry.Key == "BaudRate")
            {
                BaudRate = int.Parse((string)(YamlScalarNode)entry.Value);
            }
        }

        while (portOpen != true)
        {
            Console.WriteLine("not conected");
            //open COM port
            foreach (var port in portNames)
            {
                if (port == comPort)
                {
                    serialPort.PortName = comPort;
                    serialPort.BaudRate = BaudRate;
                    serialPort.DtrEnable = true;
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialDataRecieved);
                    serialPort.Open();

                    portOpen = true;
                    Console.WriteLine("conected");

                    // while(portNames.Contains(comPort)) {
                    //     portNames = SerialPort.GetPortNames();
                    //     Console.WriteLine(portNames.Contains(comPort));
                    // }
                    Console.ReadKey();
                    Console.WriteLine("disconect");
                    serialPort.Close();
                    portOpen = false;
                }
            }
        }

    }

    public void SerialDataRecieved(object sender, SerialDataReceivedEventArgs e)
    {
        string inData = serialPort.ReadLine();

        //Console.WriteLine(inData);

        string[] tokens = inData.Split(';');
        foreach (string token in tokens)
        {
            //Console.WriteLine(token);

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
                        ApplySoundChanges((string)(YamlScalarNode)entry.Value, float.Parse(items[1]), bool.Parse(items[2]));
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
