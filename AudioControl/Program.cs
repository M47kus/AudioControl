using System.IO.Ports;
using NAudio.CoreAudioApi;
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

    public SerialPort serialPort = new SerialPort();
    public MMDeviceEnumerator enumerator = new MMDeviceEnumerator();

    public static void Main()
                => new Program().MainInterface();

    public void MainInterface()
    {

        readMapFile(@"C:\Users\Schre\Documents\IdeaProjects\AudioControl\AudioControl\config.yaml");

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
        Console.WriteLine($"Data Received: {inData}");

    }

    public void ApplySoundChanges(String id, float value, bool mute)
    {
        foreach (var endpoint in enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active))
        {
            if (endpoint.ID == id)
            {
                Console.WriteLine(endpoint.FriendlyName);
                float volume = value;
                endpoint.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
                endpoint.AudioEndpointVolume.Mute = mute;
            }
        }
    }

    public void readMapFile(string filepath)
    {

    }
}
