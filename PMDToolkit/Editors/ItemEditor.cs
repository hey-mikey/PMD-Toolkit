﻿/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using PMDToolkit.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PMDToolkit.Editors
{
    public partial class ItemEditor : Form
    {
        private const int TOTAL_PICS = 5;
        private int itemNum;
        private List<Image> items;
        private int chosenPic;

        public ItemEditor()
        {
            InitializeComponent();

            Array arr = Enum.GetValues(typeof(Enums.ItemType));
            for (int i = 0; i < arr.Length; i++)
            {
                cbItemType.Items.Add(arr.GetValue(i));
            }
            cbItemType.SelectedIndex = 0;

            nudRarity.Minimum = 0;
            nudRarity.Maximum = 10;
            nudPrice.Minimum = 0;
            nudPrice.Maximum = int.MaxValue;

            picSprite.BackColor = Color.Black;

            int count = 0;
            items = new List<Image>();
            while (File.Exists(Paths.ItemsPath + count + ".png"))
            {
                items.Add(new Bitmap(Paths.ItemsPath + count + ".png"));
                count++;
            }
            if (items.Count > TOTAL_PICS)
            {
                vsItemScroll.Maximum = items.Count - TOTAL_PICS;
            }
            else
            {
                vsItemScroll.Enabled = false;
            }

            nudRequirement.Minimum = -1;
            nudRequirement.Maximum = int.MaxValue;

            nudReqData1.Minimum = int.MinValue;
            nudReqData1.Maximum = int.MaxValue;
            nudReqData2.Minimum = int.MinValue;
            nudReqData2.Maximum = int.MaxValue;
            nudReqData3.Minimum = int.MinValue;
            nudReqData3.Maximum = int.MaxValue;
            nudReqData4.Minimum = int.MinValue;
            nudReqData4.Maximum = int.MaxValue;
            nudReqData5.Minimum = int.MinValue;
            nudReqData5.Maximum = int.MaxValue;

            nudEffect.Minimum = -1;
            nudEffect.Maximum = int.MaxValue;

            nudEffectData1.Minimum = int.MinValue;
            nudEffectData1.Maximum = int.MaxValue;
            nudEffectData2.Minimum = int.MinValue;
            nudEffectData2.Maximum = int.MaxValue;
            nudEffectData3.Minimum = int.MinValue;
            nudEffectData3.Maximum = int.MaxValue;

            nudThrowEffect.Minimum = -1;
            nudThrowEffect.Maximum = int.MaxValue;

            nudThrowData1.Minimum = int.MinValue;
            nudThrowData1.Maximum = int.MaxValue;
            nudThrowData2.Minimum = int.MinValue;
            nudThrowData2.Maximum = int.MaxValue;
            nudThrowData3.Minimum = int.MinValue;
            nudThrowData3.Maximum = int.MaxValue;
        }

        private void ItemEditor_Load(object sender, EventArgs e)
        {
        }

        public void LoadItem(int index)
        {
            itemNum = index;
            ItemEntry entry = GameData.ItemDex[index];

            txtName.Text = entry.Name;

            cbItemType.SelectedIndex = (int)entry.Type;
            nudRarity.Value = entry.Rarity;
            nudPrice.Value = entry.Price;
            txtDescription.Text = entry.Desc;

            chosenPic = entry.Sprite;
            RefreshPic();

            nudRequirement.Value = entry.Req;
            nudReqData1.Value = entry.Req1;
            nudReqData2.Value = entry.Req2;
            nudReqData3.Value = entry.Req3;
            nudReqData4.Value = entry.Req4;
            nudReqData5.Value = entry.Req5;

            nudEffect.Value = entry.Effect;
            nudEffectData1.Value = entry.Effect1;
            nudEffectData2.Value = entry.Effect2;
            nudEffectData3.Value = entry.Effect3;

            nudThrowEffect.Value = entry.ThrowEffect;
            nudThrowData1.Value = entry.Throw1;
            nudThrowData2.Value = entry.Throw2;
            nudThrowData3.Value = entry.Throw3;
        }

        public void SaveItem()
        {
            ItemEntry entry = new ItemEntry
            {
                Name = txtName.Text,

                Type = (Enums.ItemType)cbItemType.SelectedIndex,
                Rarity = (int)nudRarity.Value,
                Price = (int)nudPrice.Value,
                Desc = txtDescription.Text,

                Sprite = chosenPic,

                Req = (int)nudRequirement.Value,
                Req1 = (int)nudReqData1.Value,
                Req2 = (int)nudReqData2.Value,
                Req3 = (int)nudReqData3.Value,
                Req4 = (int)nudReqData4.Value,
                Req5 = (int)nudReqData5.Value,

                Effect = (int)nudEffect.Value,
                Effect1 = (int)nudEffectData1.Value,
                Effect2 = (int)nudEffectData2.Value,
                Effect3 = (int)nudEffectData3.Value,

                ThrowEffect = (int)nudThrowEffect.Value,
                Throw1 = (int)nudThrowData1.Value,
                Throw2 = (int)nudThrowData2.Value,
                Throw3 = (int)nudThrowData3.Value
            };

            GameData.ItemDex[itemNum] = entry;
            GameData.ItemDex[itemNum].Save(itemNum);
        }

        private void RefreshPic()
        {
            Image endImage = new Bitmap(picSprite.Width, picSprite.Height);
            using (var graphics = System.Drawing.Graphics.FromImage(endImage))
            {
                for (int i = 0; i <= TOTAL_PICS; i++)
                {
                    int picIndex = i + vsItemScroll.Value;
                    if (picIndex < items.Count)
                    {
                        //blit at given location
                        graphics.DrawImage(items[picIndex], new Point((picSprite.Width - items[picIndex].Width) / 2, i * Graphics.TextureManager.TILE_SIZE));

                        //draw red square
                        if (chosenPic == picIndex)
                        {
                            graphics.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(0 + 1, i * Graphics.TextureManager.TILE_SIZE + 1,
                                Graphics.TextureManager.TILE_SIZE - 2, Graphics.TextureManager.TILE_SIZE - 2));
                        }
                    }
                }
            }
            picSprite.Image = endImage;
        }

        private void PicSprite_Click(object sender, EventArgs e)
        {
            int picIndex = ((MouseEventArgs)e).Y / Graphics.TextureManager.TILE_SIZE + vsItemScroll.Value;
            if (picIndex < items.Count)
                chosenPic = picIndex;
            RefreshPic();
        }

        private void VsItemScroll_ValueChanged(object sender, EventArgs e)
        {
            RefreshPic();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            SaveItem();
            Close();
        }

        private void VsItemScroll_Scroll(object sender, ScrollEventArgs e)
        {
        }
    }
}