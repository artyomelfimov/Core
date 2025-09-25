namespace Core.Attributes;

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class AssemblyEndpointAddressAttribute : Attribute
{
    public AssemblyEndpointAddressAttribute(string endpointAddress)
    {
        EndpointAddress = endpointAddress;
    }

    public string EndpointAddress { get; }
}