using System.Net;
using System.Net.Sockets;

namespace Core.Common;

public class MachineInfo
{
    public static string GetCurrentMachineName() => Environment.MachineName;

    public static IPAddress GetLocalIpAddress()
    {
        foreach (var address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
                return address;
        }
        return null;
    }
}
