using System;
using System.Collections;
using System.Collections.Generic;


/*
WARNING! TO USE THE COLLECTION FOR YOUR OWN CLASSES AND OBJECTS REMOVE THE CollectionType in the code. It is used just as placeholder.

public static void Main()
{
    Collection collection = new Collection(); //use this collection like you use List<T>;
}
    
*/


namespace Helpers
{
    /// <summary>
    /// The type of the collection.
    /// </summary>
    class CollectionType
    {

    }

    /// <summary>
    /// This program represents basic implementation of a collection, specified for just ONE type.
    /// </summary>
    class Collection : IEnumerable, IEnumerable<CollectionType>, IList, IList<CollectionType>, ICollection, ICloneable
    {
        private readonly List<CollectionType> items;

        #region Implementations

        public object this[int index] { get => items[index]; set => items[index] = value as CollectionType; }
        CollectionType IList<CollectionType>.this[int index] { get => items[index]; set => items[index] = value as CollectionType; }

        public bool IsReadOnly => false;
        public bool IsFixedSize => false;

        public int Count => items.Count;

        public object SyncRoot => false;

        public bool IsSynchronized => false;

        public int Add(object value)
        {
            items.Add(value as CollectionType);
            return 1;
        }
        public void Add(CollectionType item) => items.Add(item);

        public void Clear() => items.Clear();

        public object Clone() => items;

        public bool Contains(object value) => items.Contains(value as CollectionType);

        public bool Contains(CollectionType item) => items.Contains(item);

        public void CopyTo(Array array, int index) => items.CopyTo(array as CollectionType[], index);

        public void CopyTo(CollectionType[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

        public IEnumerator GetEnumerator() => items.GetEnumerator();

        public int IndexOf(object value) => items.IndexOf(value as CollectionType);

        public int IndexOf(CollectionType item) => items.IndexOf(item);

        public void Insert(int index, object value) => items.Insert(index, value as CollectionType);

        public void Insert(int index, CollectionType item) => items.Insert(index, item);

        public void Remove(object value) => items.Remove(value as CollectionType);

        public bool Remove(CollectionType item) => items.Remove(item);

        public void RemoveAt(int index) => items.RemoveAt(index);

        IEnumerator<CollectionType> IEnumerable<CollectionType>.GetEnumerator() => items.GetEnumerator();

        #endregion

        public Collection()
        {
            items = new List<CollectionType>();
        }

        public Collection(int len)
        {
            items = new List<CollectionType>(len);
        }

        public Collection(Collection coll)
        {
            items = coll.Clone() as List<CollectionType>;
        }
    }
}
