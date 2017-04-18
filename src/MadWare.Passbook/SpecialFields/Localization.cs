using System;
using System.Collections.Generic;
using System.Text;

namespace MadWare.Passbook.SpecialFields
{
    public class Localization
    {
        public string Language { get; private set; }

        public Dictionary<string, string> Translations { get; private set; }

        public Localization(string lang)
        {
            this.Translations = new Dictionary<string, string>();
        }

        public void AddTranslation(string key, string translation)
        {
            if (this.Translations.ContainsKey(key))
                this.Translations[key] = translation;
            else
                this.Translations.Add(key, translation);
        }

    }
}
