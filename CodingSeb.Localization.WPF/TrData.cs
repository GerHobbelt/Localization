﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CodingSeb.Localization.WPF
{
    /// <summary>
    /// This class is used as viewModel to bind  to DependencyProperties
    /// Is use by Tr MarkupExtension to dynamically update the translation when current language changed
    /// </summary>
    public class TrData : INotifyPropertyChanged
    {
        public TrData()
        {
            WeakEventManager<Loc, CurrentLanguageChangedEventArgs>.AddHandler(Loc.Instance, nameof(Loc.Instance.CurrentLanguageChanged), CurrentLanguageChanged);
        }

        ~TrData()
        {
            WeakEventManager<Loc, CurrentLanguageChangedEventArgs>.RemoveHandler(Loc.Instance, nameof(Loc.Instance.CurrentLanguageChanged), CurrentLanguageChanged);
        }

        /// <summary>
        /// To force the use of a specific identifier
        /// </summary>
        public string TextId { get; set; }

        /// <summary>
        /// The text to return if no text correspond to textId in the current language
        /// </summary>
        public string DefaultText { get; set; }

        /// <summary>
        /// The language id in which to get the translation. To Specify if not CurrentLanguage
        /// </summary>
        public string LanguageId { get; set; }

        /// <summary>
        /// To provide a prefix to add at the begining of the translated text.
        /// </summary>
        public string Prefix { get; set; } = string.Empty;

        /// <summary>
        /// To provide a suffix to add at the end of the translated text.
        /// </summary>
        public string Suffix { get; set; } = string.Empty;

        /// <summary>
        /// An optional object use as data  that is represented by this translation
        /// (Example used for Enum values translation)
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// When the current Language changed update the binding (Call OnPropertyChanged)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentLanguageChanged(object sender, CurrentLanguageChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TranslatedText));
        }

        public string TranslatedText
        {
            get
            {
                return Prefix + Loc.Tr(TextId, DefaultText, LanguageId) + Suffix;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
