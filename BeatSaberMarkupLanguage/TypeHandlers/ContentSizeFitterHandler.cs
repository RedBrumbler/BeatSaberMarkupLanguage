﻿using BS_Utils.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ContentSizeFitter;

namespace BeatSaberMarkupLanguage.TypeHandlers
{
    [ComponentHandler(typeof(ContentSizeFitter))]
    public class ContentSizeFitterHandler : TypeHandler
    {
        public override Dictionary<string, string[]> Props => new Dictionary<string, string[]>()
        {
            { "horizontalFit", new[]{ "horizontal-fit" } },
            { "verticalFit", new[]{ "vertical-fit "} }
        };

        public override void HandleType(Component obj, Dictionary<string, string> data, Dictionary<string, Action> actions)
        {
            foreach(KeyValuePair<string, string> pair in data)
            {
                obj.SetProperty(pair.Key, Enum.Parse(typeof(FitMode), pair.Value));
            }
        }
    }
}