using System;

using NAudio.CoreAudioApi;

internal class Program
{
    private static void Main(string[] args)
    {
        var enumerator = new MMDeviceEnumerator();
        foreach (var endpoint in enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active))
        {
            //Console.WriteLine(endpoint.FriendlyName);
            //Console.WriteLine(endpoint.ID);
            //Console.WriteLine(endpoint.AudioEndpointVolume.MasterVolumeLevel);
            //Console.WriteLine(endpoint.AudioEndpointVolume.MasterVolumeLevelScalar);
            //Console.WriteLine(endpoint.AudioEndpointVolume.Mute);

            if (endpoint.ID == "{0.0.0.00000000}.{a3a272e2-8b47-41ec-8b55-95010bd41a90}") {
                Console.WriteLine(endpoint.FriendlyName);
                float volume = 0.1F;
                endpoint.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
            }
        }
    }
}
