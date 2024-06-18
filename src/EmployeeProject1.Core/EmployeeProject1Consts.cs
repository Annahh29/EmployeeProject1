using EmployeeProject1.Debugging;

namespace EmployeeProject1
{
    public class EmployeeProject1Consts
    {
        public const string LocalizationSourceName = "EmployeeProject1";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "f713042d247c4969ad307eb3278e7c3f";
    }
}
