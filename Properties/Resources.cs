using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Core.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class Resources
{
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static ResourceManager ResourceManager
    {
        get
        {
            if (Core.Properties.Resources.resourceMan == null)
                Core.Properties.Resources.resourceMan = new ResourceManager("Core.Properties.Resources", typeof(Core.Properties.Resources).Assembly);
            return Core.Properties.Resources.resourceMan;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
        get => Core.Properties.Resources.resourceCulture;
        set => Core.Properties.Resources.resourceCulture = value;
    }

    public static string StrError_AttributeNotFound
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrError_AttributeNotFound), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrError_OperationIsNotSetted
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrError_OperationIsNotSetted), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrErrorCollectionCantContainNullOrWhiteSpaceString
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrErrorCollectionCantContainNullOrWhiteSpaceString), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrErrorCollectionContainsEmptyGuid
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrErrorCollectionContainsEmptyGuid), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrErrorCollectionContainsNull
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrErrorCollectionContainsNull), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrErrorEntityNotFound
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrErrorEntityNotFound), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrErrorInternalException
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrErrorInternalException), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrErrorResourceTimeout
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrErrorResourceTimeout), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrErrorStringIsNullOrWhitespace
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrErrorStringIsNullOrWhitespace), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrErrorUserDoesntHaveAccess
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrErrorUserDoesntHaveAccess), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtErrorConcreteUserDoesntHaveAccessToConcreteResource
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtErrorConcreteUserDoesntHaveAccessToConcreteResource), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtErrorEntityNotFound
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtErrorEntityNotFound), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtErrorEntityWithTypeAndIdNotFound
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtErrorEntityWithTypeAndIdNotFound), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtErrorEntityWithTypeNotFound
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtErrorEntityWithTypeNotFound), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtErrorInfrastructureError
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtErrorInfrastructureError), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtErrorInternalExceptionWithMessage
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtErrorInternalExceptionWithMessage), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtErrorInternalExceptionWithMessageInfo
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtErrorInternalExceptionWithMessageInfo), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtErrorResourceTimeout
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtErrorResourceTimeout), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtErrorUserDoesntHaveAccessToConcreteResource
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtErrorUserDoesntHaveAccessToConcreteResource), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrFmtUnexpectedEnumValueExceptionMessage
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrFmtUnexpectedEnumValueExceptionMessage), Core.Properties.Resources.resourceCulture);
        }
    }

    public static string StrTargetAssemblyDoesNotSet
    {
        get
        {
            return Core.Properties.Resources.ResourceManager.GetString(nameof(StrTargetAssemblyDoesNotSet), Core.Properties.Resources.resourceCulture);
        }
    }
}
