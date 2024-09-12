using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace ERPSystem.Localization
{
    public class LocalizationService
    {
        private readonly IStringLocalizer<LocalizationService> _localizer;

        public LocalizationService(IStringLocalizer<LocalizationService> localizer)
        {
            _localizer = localizer;
        }

        public string GetString(string key)
        {
            return _localizer[key];
        }

        public IDictionary<string, string> GetAllStrings()
        {
            var strings = new Dictionary<string, string>();
            foreach (var entry in _localizer.GetAllStrings())
            {
                strings[entry.Name] = entry.Value;
            }
            return strings;
        }
    }
}
