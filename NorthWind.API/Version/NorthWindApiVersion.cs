using Asp.Versioning;

namespace NorthWind.API.Common
{
    public readonly struct NorthWindApiVersion
    {
        public ApiVersion Version { get; }

        private NorthWindApiVersion(int major, int minor = 0)
        {
            Version = new ApiVersion(major, minor);
        }

        public static readonly NorthWindApiVersion V1 = new(1);
        public static readonly NorthWindApiVersion V2 = new(2);

        // Implicit conversion to ApiVersion
        public static implicit operator ApiVersion(NorthWindApiVersion v) => v.Version;
    }
}
