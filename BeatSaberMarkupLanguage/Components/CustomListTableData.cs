﻿using System.Collections.Generic;
using System.Linq;
using HMUI;
using TMPro;
using UnityEngine;
using LevelPackCell = AnnotatedBeatmapLevelCollectionCell; // This got renamed at a point, but old name is more clear so I'm using that

namespace BeatSaberMarkupLanguage.Components
{
    public class CustomListTableData : MonoBehaviour, TableView.IDataSource
    {
        public List<CustomCellInfo> data = new List<CustomCellInfo>();
        public float cellSize = 8.5f;
        public string reuseIdentifier = "BSMLListTableCell";
        public TableView tableView;

        public bool expandCell = false;

        private LevelListTableCell songListTableCellInstance;
        private LevelPackCell levelPackTableCellInstance;
        private SimpleTextTableCell simpleTextTableCellInstance;
        private ListStyle listStyle = ListStyle.List;

        public enum ListStyle
        {
            List,
            Box,
            Simple,
        }

        public ListStyle Style
        {
            get => listStyle;
            set
            {
                // Sets the default cell size for certain styles
                switch (value)
                {
                    case ListStyle.List:
                        cellSize = 8.5f;
                        break;
                    case ListStyle.Box:
                        cellSize = tableView.tableType == TableView.TableType.Horizontal ? 30f : 35f;
                        break;
                    case ListStyle.Simple:
                        cellSize = 8f;
                        break;
                }

                listStyle = value;
            }
        }

        public LevelListTableCell GetTableCell()
        {
            LevelListTableCell tableCell = (LevelListTableCell)tableView.DequeueReusableCellForIdentifier(reuseIdentifier);
            if (!tableCell)
            {
                if (songListTableCellInstance == null)
                {
                    songListTableCellInstance = Resources.FindObjectsOfTypeAll<LevelListTableCell>().First(x => (x.name == "LevelListTableCell"));
                }

                tableCell = Instantiate(songListTableCellInstance);
            }

            tableCell._notOwned = false;

            tableCell.reuseIdentifier = reuseIdentifier;
            return tableCell;
        }

        public BSMLBoxTableCell GetBoxTableCell()
        {
            BSMLBoxTableCell tableCell = (BSMLBoxTableCell)tableView.DequeueReusableCellForIdentifier(reuseIdentifier);
            if (!tableCell)
            {
                if (levelPackTableCellInstance == null)
                {
                    levelPackTableCellInstance = Resources.FindObjectsOfTypeAll<LevelPackCell>().First(x => x.name == "AnnotatedBeatmapLevelCollectionCell");
                }

                tableCell = InstantiateBoxTableCell(levelPackTableCellInstance);
            }

            tableCell.reuseIdentifier = reuseIdentifier;
            return tableCell;
        }

        public BSMLBoxTableCell InstantiateBoxTableCell(LevelPackCell levelPackTableCell)
        {
            levelPackTableCell = Instantiate(levelPackTableCell);
            ImageView coverImage = levelPackTableCell._coverImage;
            ImageView selectionImage = levelPackTableCell._selectionImage;

            foreach (Transform child in coverImage.transform)
            {
                Destroy(child.gameObject);
            }

            GameObject cellObject = levelPackTableCell.gameObject;
            Destroy(levelPackTableCell);
            BSMLBoxTableCell boxTableCell = cellObject.AddComponent<BSMLBoxTableCell>();
            boxTableCell.SetComponents(coverImage, selectionImage);
            return boxTableCell;
        }

        public SimpleTextTableCell GetSimpleTextTableCell()
        {
            SimpleTextTableCell tableCell = (SimpleTextTableCell)tableView.DequeueReusableCellForIdentifier(reuseIdentifier);
            if (!tableCell)
            {
                if (simpleTextTableCellInstance == null)
                {
                    simpleTextTableCellInstance = Resources.FindObjectsOfTypeAll<SimpleTextTableCell>().First(x => x.name == "SimpleTextTableCell");
                }

                tableCell = Instantiate(simpleTextTableCellInstance);
            }

            tableCell.reuseIdentifier = reuseIdentifier;
            return tableCell;
        }

        public virtual TableCell CellForIdx(TableView tableView, int idx)
        {
            switch (listStyle)
            {
                case ListStyle.List:
                    LevelListTableCell tableCell = GetTableCell();

                    TextMeshProUGUI nameText = tableCell._songNameText;
                    TextMeshProUGUI authorText = tableCell._songAuthorText;
                    tableCell._songBpmText.gameObject.SetActive(false);
                    tableCell._songDurationText.gameObject.SetActive(false);
                    tableCell._promoBadgeGo.SetActive(false);
                    tableCell._updatedBadgeGo.SetActive(false);
                    tableCell._favoritesBadgeImage.gameObject.SetActive(false);
                    tableCell.transform.Find("BpmIcon").gameObject.SetActive(false);
                    if (expandCell)
                    {
                        nameText.rectTransform.anchorMax = new Vector3(2, 0.5f, 0);
                        authorText.rectTransform.anchorMax = new Vector3(2, 0.5f, 0);
                    }

                    nameText.text = data[idx].text;
                    authorText.text = data[idx].subtext;
                    tableCell._coverImage.sprite = data[idx].icon == null ? Utilities.LoadSpriteFromTexture(Texture2D.blackTexture) : data[idx].icon;

                    return tableCell;
                case ListStyle.Box:
                    BSMLBoxTableCell cell = GetBoxTableCell();
                    cell.SetData(data[idx].icon == null ? Utilities.LoadSpriteFromTexture(Texture2D.blackTexture) : data[idx].icon);

                    return cell;
                case ListStyle.Simple:
                    SimpleTextTableCell simpleCell = GetSimpleTextTableCell();
                    simpleCell._text.richText = true;
                    simpleCell._text.enableWordWrapping = true;
                    simpleCell.text = data[idx].text;

                    return simpleCell;
            }

            return null;
        }

        public float CellSize()
        {
            return cellSize;
        }

        public int NumberOfCells()
        {
            return data.Count();
        }

        public class CustomCellInfo
        {
            public string text;
            public string subtext;
            public Sprite icon;

            public CustomCellInfo(string text, string subtext = null, Sprite icon = null)
            {
                this.text = text;
                this.subtext = subtext;
                this.icon = icon;
            }
        }
    }
}
