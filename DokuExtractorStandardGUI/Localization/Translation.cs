﻿using DokuExtractorCore.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuExtractorStandardGUI.Localization
{
    public static class Translation
    {
        public static LanguageStrings LanguageStrings { get; set; } = new LanguageStrings();

        public static void LoadLanguageFile(CultureInfo culture, string additionalCultureInfo, string languageFolderPath)
        {
            try
            {
                var languageFiles = Directory.GetFiles(languageFolderPath);
                foreach (var languageFile in languageFiles)
                {
                    var fileInfo = new FileInfo(languageFile);
                    if (fileInfo.Name.ToLower().Contains("_" + culture.Name.ToLower()))
                    {
                        if (string.IsNullOrWhiteSpace(additionalCultureInfo) == false && fileInfo.Name.ToLower().Contains("_" + additionalCultureInfo.ToLower()))
                        {
                            var languageJson = File.ReadAllText(fileInfo.FullName);
                            Translation.LanguageStrings = JsonConvert.DeserializeObject<LanguageStrings>(languageJson);
                            break;
                        }
                        else if (string.IsNullOrWhiteSpace(additionalCultureInfo) == true)
                        {
                            var languageJson = File.ReadAllText(fileInfo.FullName);
                            Translation.LanguageStrings = JsonConvert.DeserializeObject<LanguageStrings>(languageJson);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        public static List<LanguageStrings> LoadAllLanguageFiles(string languageFolderPath)
        {
            var retVal = new List<LanguageStrings>();

            try
            {
                var languageFiles = Directory.GetFiles(languageFolderPath);
                foreach (var languageFile in languageFiles)
                {
                    var fileInfo = new FileInfo(languageFile);
                    var languageJson = File.ReadAllText(fileInfo.FullName);
                    var languageObject = JsonConvert.DeserializeObject<LanguageStrings>(languageJson);
                    retVal.Add(languageObject);
                }
            }
            catch (Exception ex)
            { }

            return retVal;
        }

        public static void SaveAllLanguageFiles(List<LanguageStrings> languageFiles, string languageFolderPath)
        {
            foreach (var language in languageFiles)
            {
                var languageJson = JsonConvert.SerializeObject(language, Formatting.Indented);
                var filePath = string.Empty;

                if (string.IsNullOrWhiteSpace(language.AdditionalCultureInfo))
                    filePath = Path.Combine(languageFolderPath, "DokuExtractorLanguage_" + language.Culture.Name + ".json");
                else
                    filePath = Path.Combine(languageFolderPath, "DokuExtractorLanguage_" + language.Culture.Name + "_" + language.AdditionalCultureInfo + ".json");

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                File.WriteAllText(filePath, languageJson);
            }
        }

        public static string TranslateFieldTypeEnum(DataFieldTypes fieldType)
        {
            switch (fieldType)
            {
                case DataFieldTypes.Text:
                    return LanguageStrings.FieldTypeText;
                case DataFieldTypes.Date:
                    return LanguageStrings.FieldTypeDate;
                case DataFieldTypes.Currency:
                    return LanguageStrings.FieldTypeCurrency;
                case DataFieldTypes.IBAN:
                    return LanguageStrings.FieldTypeIban;
                case DataFieldTypes.AnchorLessIBAN:
                    return LanguageStrings.FieldTypeAnchorlessIban;
                case DataFieldTypes.VatId:
                    return LanguageStrings.FieldTypeVatId;
                case DataFieldTypes.Term:
                    return LanguageStrings.FieldTypeTerm;
                default:
                    return string.Empty;
            }
        }
    }
}
