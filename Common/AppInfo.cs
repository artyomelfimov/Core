using Core.Properties;
using System.Reflection;
using System.Text;

namespace Core.Common;

public static class AppInfo
{
    public static Assembly TargetAssembly { get; set; } = Assembly.GetEntryAssembly();

    public static string AppName
    {
        get
        {
            return $"{GetInfoFromAttribute<AssemblyProductAttribute, string>(a => a.Product)}: {GetInfoFromAttribute<AssemblyTitleAttribute, string>(a => a.Title)}";
        }
    }

    public static string AssemblyName => GetAssemblyOrThrow().GetName().Name;

    public static string Configuration
    {
        get
        {
            return GetInfoFromAttribute<AssemblyConfigurationAttribute, string>(a => a.Configuration);
        }
    }

    public static string Description
    {
        get
        {
            return GetInfoFromAttribute<AssemblyDescriptionAttribute, string>(a => a.Description);
        }
    }

    public static string FullAppName => $"{AppName}-{Version}";

    public static string Product
    {
        get
        {
            return GetInfoFromAttribute<AssemblyProductAttribute, string>(a => a.Product);
        }
    }

    public static string Title
    {
        get
        {
            return GetInfoFromAttribute<AssemblyTitleAttribute, string>(a => a.Title);
        }
    }

    public static string Trademark
    {
        get
        {
            return GetInfoFromAttribute<AssemblyTrademarkAttribute, string>(a => a.Trademark);
        }
    }

    public static string Version
    {
        get
        {
            var appVersion = AppVersion;
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0}.{1}.{2}.{3}", appVersion.Major, appVersion.Minor, appVersion.Build, appVersion.Revision);
            if (!string.IsNullOrWhiteSpace(Configuration) && !Configuration.Equals("''", StringComparison.Ordinal))
                stringBuilder.AppendFormat("-{0}", Configuration);

            return stringBuilder.ToString();
        }
    }

    public static Version AppVersion => GetAssemblyOrThrow().GetName().Version;

    private static Assembly GetAssemblyOrThrow()
    {
        if (TargetAssembly == null)
        {
            throw new InvalidOperationException(string.Format(Resources.StrTargetAssemblyDoesNotSet, TargetAssembly, "GetEntryAssembly"));
        }

        return TargetAssembly;
    }

    private static TR GetInfoFromAttribute<TA, TR>(Func<TA, TR> func, Func<TR> def = null) where TA : Attribute
    {
        return GetAssemblyOrThrow().GetInfoFromAttribute(func, def);
    }
}
