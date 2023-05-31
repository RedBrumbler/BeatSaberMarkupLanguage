﻿using System;
using System.Collections.Generic;
using BeatSaberMarkupLanguage.Components;

namespace BeatSaberMarkupLanguage.TypeHandlers
{
    [ComponentHandler(typeof(ButtonArtworkImage))]
    public class ButtonArtworkHandler : TypeHandler<ButtonArtworkImage>
    {
        public override Dictionary<string, string[]> Props => new Dictionary<string, string[]>()
        {
            { "artwork", new[] { "artwork", "art", "bg-artwork", "bg-art" } },
        };

        public override Dictionary<string, Action<ButtonArtworkImage, string>> Setters => new Dictionary<string, Action<ButtonArtworkImage, string>>()
        {
            { "artwork", new Action<ButtonArtworkImage, string>((images, artPath) => images.SetArtwork(artPath)) },
        };
    }
}
