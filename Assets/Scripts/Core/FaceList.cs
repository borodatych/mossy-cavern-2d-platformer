using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class FaceList<TFaceItem> : ScriptableObject, IList<TFaceItem>, IDictionary<string, TFaceItem>, ISerializationCallbackReceiver
    where TFaceItem : FaceItem
	{
		[SerializeField] private TFaceItem[] _list;

		/// <summary>
		/// Cached dictionary of list by their IDs
		/// </summary>
		IDictionary<string, TFaceItem> m_ItemDictionary;

		public bool Remove(KeyValuePair<string, TFaceItem> item)
		{
			throw new NotImplementedException();
		}

		public int Count => _list.Length;

		public bool IsReadOnly => true;

		public TFaceItem this[int i]
		{
			get => _list[i];
			set => _list[i] = value;
		}

		public TFaceItem this[string key]
		{
			get { return m_ItemDictionary[key]; }
			set => m_ItemDictionary[key] = value;
		}

		public ICollection<string> Keys => m_ItemDictionary.Keys;

		public ICollection<TFaceItem> Values { get; }

		public int IndexOf(TFaceItem item)
		{
			if (item == null)
			{
				return -1;
			}

			for (int i = 0; i < _list.Length; ++i)
			{
				if (_list[i] == item)
				{
					return i;
				}
			}

			return -1;
		}

		public bool Contains(TFaceItem item)
		{
			return IndexOf(item) >= 0;
		}

		public void Add(string key, TFaceItem value)
		{
			throw new NotImplementedException();
		}

		public bool ContainsKey(string key)
		{
			return m_ItemDictionary.ContainsKey(key);
		}
		private bool InBounds (int index) 
		{
			return index >= 0 && index < Count;
		}

		public bool Remove(string key)
		{
			throw new NotImplementedException();
		}

		public bool TryGetValue(string key, out TFaceItem value)
		{
			return m_ItemDictionary.TryGetValue(key, out value);
		}

		public TFaceItem GetItemByCode(string code)
		{
			foreach (var item in _list)
			{
				if (item != null && item.code == code) {
					return item;
				}
			}

			return null;
		}

		public TFaceItem GetNextById(int i)
		{
			return _list[++i];
		}
		public TFaceItem GetNextByCode(string code)
		{
			TFaceItem item = GetItemByCode(code);
			int idx = IndexOf(item);
			//Debug.LogFormat($">> GetNextByCode | idx: {idx}. cnt: {Count}");
			return InBounds(idx + 1) ? GetNextById(idx) : null;
		}
		
		public void Insert(int index, TFaceItem item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public void Add(TFaceItem item)
		{
			throw new NotImplementedException();
		}

		public void Add(KeyValuePair<string, TFaceItem> item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(KeyValuePair<string, TFaceItem> item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(KeyValuePair<string, TFaceItem>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(TFaceItem[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(TFaceItem item)
		{
			throw new NotImplementedException();
		}

		IEnumerator<KeyValuePair<string, TFaceItem>> IEnumerable<KeyValuePair<string, TFaceItem>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public IEnumerator<TFaceItem> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}


		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
		}
	}
}