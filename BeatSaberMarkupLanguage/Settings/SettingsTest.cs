﻿using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Parser;
using UnityEngine;

namespace BeatSaberMarkupLanguage.Settings
{
    public class SettingsTest : NotifiableSingleton<SettingsTest>
    {
        [UIParams]
        private BSMLParserParams parserParams;

        private bool boolTest = true;

        [UIValue("slider-value")]
        private int sliderValue = 5;

        [UIValue("string-value")]
        private string testString = "Shazam";

        [UIValue("color-value")]
        private Color testColor = Color.yellow;

        [UIValue("list-options")]
        private List<object> options = new object[] { "1", "Something", "Kapow", "Yeet" }.ToList();

        [UIValue("list-choice")]
        private string listChoice = "Something";

        [UIValue("bool-test")]
        private bool BoolTest
        {
            get => boolTest;
            set
            {
                boolTest = value;
                NotifyPropertyChanged();
            }
        }

        [UIAction("#apply")]
        public void OnApply()
        {
            Logger.Log.Info($"{sliderValue}");
            Logger.Log.Info($"{testString}");
            Logger.Log.Info($"Bool Test: {boolTest}");
            Logger.Log.Info($"List Test: {listChoice}");
        }

        [UIAction("format")]
        public string Format(int number)
        {
            return number + "x";
        }

        [UIAction("change-bool")]
        public void ChangeBool()
        {
            BoolTest = !BoolTest;
        }
    }
}
