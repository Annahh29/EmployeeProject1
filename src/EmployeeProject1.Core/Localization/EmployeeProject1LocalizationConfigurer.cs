using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace EmployeeProject1.Localization
{
    public static class EmployeeProject1LocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(EmployeeProject1Consts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(EmployeeProject1LocalizationConfigurer).GetAssembly(),
                        "EmployeeProject1.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
