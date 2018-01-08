using System;
using UnityEngine;

namespace EnhancedUI
{
	// Token: 0x02000024 RID: 36
	public class SmallList<T>
	{
		// Token: 0x17000011 RID: 17
		public T this[int i]
		{
			get
			{
				return this.data[i];
			}
			set
			{
				this.data[i] = value;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008EF8 File Offset: 0x000072F8
		private void ResizeArray()
		{
			T[] array;
			if (this.data != null)
			{
				array = new T[Mathf.Max(this.data.Length << 1, 64)];
			}
			else
			{
				array = new T[64];
			}
			if (this.data != null && this.Count > 0)
			{
				this.data.CopyTo(array, 0);
			}
			this.data = array;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008F5F File Offset: 0x0000735F
		public void Clear()
		{
			this.Count = 0;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008F68 File Offset: 0x00007368
		public T First()
		{
			if (this.data == null || this.Count == 0)
			{
				return default(T);
			}
			return this.data[0];
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008FA4 File Offset: 0x000073A4
		public T Last()
		{
			if (this.data == null || this.Count == 0)
			{
				return default(T);
			}
			return this.data[this.Count - 1];
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008FE4 File Offset: 0x000073E4
		public void Add(T item)
		{
			if (this.data == null || this.Count == this.data.Length)
			{
				this.ResizeArray();
			}
			this.data[this.Count] = item;
			this.Count++;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009035 File Offset: 0x00007435
		public void AddStart(T item)
		{
			this.Insert(item, 0);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009040 File Offset: 0x00007440
		public void Insert(T item, int index)
		{
			if (this.data == null || this.Count == this.data.Length)
			{
				this.ResizeArray();
			}
			for (int i = this.Count; i > index; i--)
			{
				this.data[i] = this.data[i - 1];
			}
			this.data[index] = item;
			this.Count++;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000090BD File Offset: 0x000074BD
		public T RemoveStart()
		{
			return this.RemoveAt(0);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000090C8 File Offset: 0x000074C8
		public T RemoveAt(int index)
		{
			if (this.data != null && this.Count != 0)
			{
				T result = this.data[index];
				for (int i = index; i < this.Count - 1; i++)
				{
					this.data[i] = this.data[i + 1];
				}
				this.Count--;
				this.data[this.Count] = default(T);
				return result;
			}
			return default(T);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00009160 File Offset: 0x00007560
		public T Remove(T item)
		{
			if (this.data != null && this.Count != 0)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.data[i].Equals(item))
					{
						return this.RemoveAt(i);
					}
				}
			}
			return default(T);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000091D0 File Offset: 0x000075D0
		public T RemoveEnd()
		{
			if (this.data != null && this.Count != 0)
			{
				this.Count--;
				T result = this.data[this.Count];
				this.data[this.Count] = default(T);
				return result;
			}
			return default(T);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00009238 File Offset: 0x00007638
		public bool Contains(T item)
		{
			if (this.data == null)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (this.data[i].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000DA RID: 218
		public T[] data;

		// Token: 0x040000DB RID: 219
		public int Count;
	}
}
