﻿using HMUI;

namespace BeatSaberMarkupLanguage.Components
{
    public class BSMLScrollView : ScrollView
    {
        /*
        public override void Setup()
        {
            if (_contentRectTransform == null) return;
            _contentHeight = (_contentRectTransform.GetChild(0).transform as RectTransform).rect.height;
            _scrollPageHeight = _viewport.rect.height;
            bool active = _contentHeight > _viewport.rect.height;
            _pageUpButton.gameObject.SetActive(active);
            _pageDownButton.gameObject.SetActive(active);
            if (_verticalScrollIndicator != null)
            {
                _verticalScrollIndicator.gameObject.SetActive(active);
                _verticalScrollIndicator.normalizedPageHeight = _viewport.rect.height / _contentHeight;
            }
            ComputeScrollFocusPosY();
            //_verticalScrollIndicator.RefreshHandle();
            RectTransform handle = _verticalScrollIndicator._handle;
            handle.sizeDelta = new Vector2(handle.sizeDelta.x, Math.Abs(handle.sizeDelta.y));
        }*/
    }
}
