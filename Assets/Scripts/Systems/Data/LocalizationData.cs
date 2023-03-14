﻿using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

[CreateAssetMenu(menuName = "Data/LocalizationData")]
public class LocalizationData : ScriptableObject
{
    public LanguageReactiveProperty lang;
    public Dictionary<string, LocalizationItem> itemsDict = new Dictionary<string, LocalizationItem>();

    [Serializable]
    public class LocalizationItem
    {
        public Dictionary<Language, string> dict = new Dictionary<Language, string>()
        {
            {Language.English, ""},
            {Language.Russian, ""}
        };
    }
}

public enum Language
{
    English,
    Russian,
    Spanish,
    Portuguese,
    German,
    French
}

[Serializable]
public class LanguageReactiveProperty : ReactiveProperty<Language>
{
    public LanguageReactiveProperty()
    {
    }

    public LanguageReactiveProperty(Language initialValue)
        : base(initialValue)
    {
    }
}
