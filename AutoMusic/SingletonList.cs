using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMusic
{
    public abstract class SingletonList<T>
    {
        protected string _Path = "";
        public string Path { get { return this._Path; } }
        protected List<T> _Items;
        public List<T> Items { get { return this._Items; } }
        public bool Saved { get { return (this.Path != ""); } }
        public bool Empty { get { return (this.Items.Count == 0); } }

        public SingletonList()
        {
            this._Items = new List<T>();
        }
        public virtual void Add(T Item)
        {
            if (Item == null) { throw new ArgumentNullException(); }
            this.Items.Add(Item);
        }
        public virtual void Add(SingletonList<T> List)
        {
            if (List == null) { throw new ArgumentNullException(); }
            this.AddRange(List.Items);
        }
        public virtual void AddRange(List<T> Items)
        {
            for (int i = 0; i < Items.Count; i++) { this.Add(Items[i]); }
        }
        public virtual void Insert(int Index, T Item)
        {
            if (Item == null) { throw new ArgumentNullException(); }
            this.Items.Insert(Index, Item);
        }
        public virtual void InsertRange(int Index, List<T> Items)
        {
            this.Items.InsertRange(Index, Items);
        }
        public virtual void Remove(T Item)
        {
            if (Item == null) { throw new ArgumentNullException(); }
            this.Items.Remove(Item);
        }
        public void Move(T Item, int Position)
        {
            if (Item == null) { throw new ArgumentNullException(); }
            this.Items.Remove(Item);
            this.Items.Insert(Position, Item);
        }
        public virtual void Clear()
        {
            List<T> Items = new List<T>();
            Items.AddRange(this._Items);
            foreach (T Item in Items)
            {
                this.Remove(Item);
            }
        }

        public virtual void Save() { this.Save(this.Path); }
        public virtual void Save(string Path)
        {
            this.Export(Path);
            this._Path = Path;
        }

        public abstract void Export(string Path);


        static public SingletonList<T> Active;

        static public SingletonList<T> Load(string File)
        {
            throw new NotImplementedException();
        }
        static public bool IsValid(string File)
        {
            throw new NotImplementedException();
        }
        static public List<T> GetItems(string File)
        {
            SingletonList<T> List = SingletonList<T>.Load(File);
            return List.Items;
        }
    }
}
