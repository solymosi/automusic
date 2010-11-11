using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using AutoMusic;

namespace AutoMusic
{
    public class ReorderableListView : ListView
    {
        public delegate void ReorderDelegate(object sender, EventArgs e);
        public event ReorderDelegate ItemsReordered = delegate { };

        private const string REORDER = "Reorder";

        private bool allowRowReorder = true;
        public bool AllowRowReorder
        {
            get
            {
                return this.allowRowReorder;
            }
            set
            {
                this.allowRowReorder = value;
                base.AllowDrop = value;
            }
        }

        public new SortOrder Sorting
        {
            get
            {
                return SortOrder.None;
            }
            set
            {
                base.Sorting = SortOrder.None;
            }
        }

        public ReorderableListView()
            : base()
        {
            this.AllowRowReorder = true;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Point cp = base.PointToClient(new Point(e.X, e.Y));
                ListViewItem dragToItem = base.GetItemAt(cp.X, cp.Y);
                int InsertIndex = base.Items.Count;
                if (dragToItem != null) { InsertIndex = dragToItem.Index; }
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                List<Track> tracks = new List<Track>();
                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        if (Playlist.IsValid(files[i])) 
                        {
                            tracks.AddRange(Playlist.GetItems(files[i]));
                            continue;
                        }
                    }
                    catch { }
                    tracks.Add(new Track(files[i]));
                }
                Playlist.Active.InsertRange(InsertIndex, tracks);
            }
            else
            {
                if (!this.AllowRowReorder)
                {
                    return;
                }
                if (base.SelectedItems.Count == 0)
                {
                    return;
                }
                Point cp = base.PointToClient(new Point(e.X, e.Y));
                ListViewItem dragToItem = base.GetItemAt(cp.X, cp.Y);
                int dropIndex = base.Items.Count;
                if (dragToItem != null)
                {
                    dropIndex = dragToItem.Index;
                    if (dropIndex > base.SelectedItems[0].Index)
                    {
                        dropIndex++;
                    }
                }
                ArrayList insertItems =
                    new ArrayList(base.SelectedItems.Count);
                foreach (ListViewItem item in base.SelectedItems)
                {
                    insertItems.Add(item.Clone());
                }
                for (int i = insertItems.Count - 1; i >= 0; i--)
                {
                    ListViewItem insertItem =
                        (ListViewItem)insertItems[i];
                    base.Items.Insert(dropIndex, insertItem);
                }
                foreach (ListViewItem removeItem in base.SelectedItems)
                {
                    base.Items.Remove(removeItem);
                }
                this.ItemsReordered(this, new EventArgs());
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Point cp = base.PointToClient(new Point(e.X, e.Y));
                ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
                if (hoverItem == null)
                {
                    e.Effect = DragDropEffects.All;
                    return;
                }
                int LastItemIndex = -1;
                for (int i = 0; i < base.Items.Count; i++)
                {
                    Track T = (Track)base.Items[i].Tag;
                    if (T.State != TrackState.Queued && T.State != TrackState.Excluded && T.State != TrackState.Error)
                    {
                        LastItemIndex = base.Items[i].Index;
                    }
                }
                if (hoverItem.Index <= LastItemIndex)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                e.Effect = DragDropEffects.All;
                base.OnDragDrop(e);
            }
            else
            {
                if (!this.AllowRowReorder)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                if (!e.Data.GetDataPresent(DataFormats.Text))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                Point cp = base.PointToClient(new Point(e.X, e.Y));
                ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
                int TargetIndex = base.Items.Count;
                if (hoverItem != null)
                {
                    TargetIndex = hoverItem.Index;
                }
                int LastItemIndex = -1;
                for (int i = 0; i < base.Items.Count; i++)
                {
                    Track T = (Track)base.Items[i].Tag;
                    if (T.State != TrackState.Queued && T.State != TrackState.Excluded && T.State != TrackState.Error)
                    {
                        LastItemIndex = base.Items[i].Index;
                    }
                }
                if (TargetIndex <= LastItemIndex)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                foreach (ListViewItem moveItem in base.SelectedItems)
                {
                    Track T = (Track)moveItem.Tag;
                    if (moveItem.Index == TargetIndex || moveItem.Index <= LastItemIndex)
                    {
                        e.Effect = DragDropEffects.None;
                        if (hoverItem != null) { hoverItem.EnsureVisible(); }
                        return;
                    }
                }
                base.OnDragOver(e);
                String text = (String)e.Data.GetData(REORDER.GetType());
                if (text.CompareTo(REORDER) == 0)
                {
                    e.Effect = DragDropEffects.Move;
                    if (hoverItem != null) { hoverItem.EnsureVisible(); }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (!this.AllowRowReorder)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            base.OnDragEnter(e);
            String text = (String)e.Data.GetData(REORDER.GetType());
            if (text.CompareTo(REORDER) == 0)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);
            if (!this.AllowRowReorder)
            {
                return;
            }
            base.DoDragDrop(REORDER, DragDropEffects.Move);
        }
    }
}