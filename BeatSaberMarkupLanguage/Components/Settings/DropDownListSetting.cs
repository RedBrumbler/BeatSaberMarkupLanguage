﻿using System.Collections.Generic;
using System.Linq;
using HMUI;

namespace BeatSaberMarkupLanguage.Components.Settings
{
    public class DropDownListSetting : GenericInteractableSetting
    {
        public List<object> values;
        public SimpleTextDropdown dropdown;

        private int index;

        public object Value
        {
            get
            {
                ValidateRange();
                return values[index];
            }

            set
            {
                index = values.IndexOf(value);
                if (index < 0)
                {
                    index = 0;
                }

                dropdown.SelectCellWithIdx(index);

                UpdateState();
            }
        }

        public override bool interactable
        {
            get => dropdown._button.interactable;
            set => dropdown._button.interactable = value;
        }

        public override void Setup()
        {
            dropdown.didSelectCellWithIdxEvent += OnSelectIndex;
            ReceiveValue();
            UpdateChoices();
            gameObject.SetActive(true);
        }

        public void UpdateChoices()
        {
            dropdown.SetTexts(values.Select(x => formatter == null ? x.ToString() : (formatter.Invoke(x) as string)).ToList());
        }

        public override void ApplyValue()
        {
            associatedValue?.SetValue(Value);
        }

        public override void ReceiveValue()
        {
            if (associatedValue != null)
            {
                Value = associatedValue.GetValue();
            }
        }

        private void OnSelectIndex(DropdownWithTableView tableView, int index)
        {
            this.index = index;
            UpdateState();
            onChange?.Invoke(Value);

            if (updateOnChange)
            {
                ApplyValue();
            }
        }

        private void ValidateRange()
        {
            if (index >= values.Count)
            {
                index = values.Count - 1;
            }

            if (index < 0)
            {
                index = 0;
            }
        }

        private void UpdateState()
        {
            dropdown._text.text = formatter == null ? Value.ToString() : (formatter.Invoke(Value) as string);
        }
    }
}
