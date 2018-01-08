using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EnhancedUI.EnhancedScroller
{
	// Token: 0x0200001C RID: 28
	[RequireComponent(typeof(ScrollRect))]
	public class EnhancedScroller : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00006566 File Offset: 0x00004966
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000656E File Offset: 0x0000496E
		public IEnhancedScrollerDelegate Delegate
		{
			get
			{
				return this._delegate;
			}
			set
			{
				this._delegate = value;
				this._reloadData = true;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000657E File Offset: 0x0000497E
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00006588 File Offset: 0x00004988
		public float ScrollPosition
		{
			get
			{
				return this._scrollPosition;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, this.GetScrollPositionForCellViewIndex(this._cellViewSizeArray.Count - 1, EnhancedScroller.CellViewPositionEnum.Before));
				if (this._scrollPosition != value)
				{
					this._scrollPosition = value;
					if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
					{
						this._scrollRect.verticalNormalizedPosition = 1f - this._scrollPosition / this._ScrollSize;
					}
					else
					{
						this._scrollRect.horizontalNormalizedPosition = this._scrollPosition / this._ScrollSize;
					}
					this._refreshActive = true;
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00006616 File Offset: 0x00004A16
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00006620 File Offset: 0x00004A20
		public bool Loop
		{
			get
			{
				return this.loop;
			}
			set
			{
				if (this.loop != value)
				{
					float scrollPosition = this._scrollPosition;
					this.loop = value;
					this._Resize(false);
					if (this.loop)
					{
						this.ScrollPosition = this._loopFirstScrollPosition + scrollPosition;
					}
					else
					{
						this.ScrollPosition = scrollPosition - this._loopFirstScrollPosition;
					}
					this.ScrollbarVisibility = this.scrollbarVisibility;
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00006686 File Offset: 0x00004A86
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00006690 File Offset: 0x00004A90
		public EnhancedScroller.ScrollbarVisibilityEnum ScrollbarVisibility
		{
			get
			{
				return this.scrollbarVisibility;
			}
			set
			{
				this.scrollbarVisibility = value;
				if (this._scrollbar != null && this._cellViewOffsetArray != null && this._cellViewOffsetArray.Count > 0)
				{
					if (this._cellViewOffsetArray.Last() < this.ScrollRectSize || this.loop)
					{
						this._scrollbar.gameObject.SetActive(this.scrollbarVisibility == EnhancedScroller.ScrollbarVisibilityEnum.Always);
					}
					else
					{
						this._scrollbar.gameObject.SetActive(this.scrollbarVisibility != EnhancedScroller.ScrollbarVisibilityEnum.Never);
					}
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000672C File Offset: 0x00004B2C
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00006739 File Offset: 0x00004B39
		public Vector2 Velocity
		{
			get
			{
				return this._scrollRect.velocity;
			}
			set
			{
				this._scrollRect.velocity = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00006748 File Offset: 0x00004B48
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000678B File Offset: 0x00004B8B
		public float LinearVelocity
		{
			get
			{
				return (this.scrollDirection != EnhancedScroller.ScrollDirectionEnum.Vertical) ? this._scrollRect.velocity.x : this._scrollRect.velocity.y;
			}
			set
			{
				if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
				{
					this._scrollRect.velocity = new Vector2(0f, value);
				}
				else
				{
					this._scrollRect.velocity = new Vector2(value, 0f);
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000067C9 File Offset: 0x00004BC9
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000067D1 File Offset: 0x00004BD1
		public bool IsScrolling { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000067DA File Offset: 0x00004BDA
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000067E2 File Offset: 0x00004BE2
		public bool IsTweening { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000067EB File Offset: 0x00004BEB
		public int StartCellViewIndex
		{
			get
			{
				return this._activeCellViewsStartIndex;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000067F3 File Offset: 0x00004BF3
		public int EndCellViewIndex
		{
			get
			{
				return this._activeCellViewsEndIndex;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000067FB File Offset: 0x00004BFB
		public int StartDataIndex
		{
			get
			{
				return this._activeCellViewsStartIndex % this.NumberOfCells;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000680A File Offset: 0x00004C0A
		public int EndDataIndex
		{
			get
			{
				return this._activeCellViewsEndIndex % this.NumberOfCells;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00006819 File Offset: 0x00004C19
		public int NumberOfCells
		{
			get
			{
				return (this._delegate == null) ? 0 : this._delegate.GetNumberOfCells(this);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00006838 File Offset: 0x00004C38
		public ScrollRect ScrollRect
		{
			get
			{
				return this._scrollRect;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00006840 File Offset: 0x00004C40
		public float ScrollRectSize
		{
			get
			{
				if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
				{
					return this._scrollRectTransform.rect.height;
				}
				return this._scrollRectTransform.rect.width;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006880 File Offset: 0x00004C80
		public EnhancedScrollerCellView GetCellView(EnhancedScrollerCellView cellPrefab)
		{
			EnhancedScrollerCellView enhancedScrollerCellView = this._GetRecycledCellView(cellPrefab);
			if (enhancedScrollerCellView == null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(cellPrefab.gameObject);
				enhancedScrollerCellView = gameObject.GetComponent<EnhancedScrollerCellView>();
				enhancedScrollerCellView.transform.SetParent(this._container);
			}
			return enhancedScrollerCellView;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000068C6 File Offset: 0x00004CC6
		public void ReloadData()
		{
			this._reloadData = false;
			this._scrollPosition = 0f;
			this._RecycleAllCells();
			if (this._delegate != null)
			{
				this._Resize(false);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000068F4 File Offset: 0x00004CF4
		public void RefreshActiveCellViews()
		{
			for (int i = 0; i < this._activeCellViews.Count; i++)
			{
				this._activeCellViews[i].RefreshCellView();
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006930 File Offset: 0x00004D30
		public void ClearRecycled()
		{
			for (int i = 0; i < this._recycledCellViews.Count; i++)
			{
				UnityEngine.Object.DestroyImmediate(this._recycledCellViews[i].gameObject);
			}
			this._recycledCellViews.Clear();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000697A File Offset: 0x00004D7A
		public void ToggleLoop()
		{
			this.Loop = !this.loop;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000698C File Offset: 0x00004D8C
		public void JumpToDataIndex(int dataIndex, float scrollerOffset = 0f, float cellOffset = 0f, bool useSpacing = true, EnhancedScroller.TweenType tweenType = EnhancedScroller.TweenType.immediate, float tweenTime = 0f, Action jumpComplete = null)
		{
			float num = 0f;
			if (cellOffset != 0f)
			{
				float num2 = (this._delegate == null) ? 0f : this._delegate.GetCellViewSize(this, dataIndex);
				if (useSpacing)
				{
					num2 += this.spacing;
					if (dataIndex > 0 && dataIndex < this.NumberOfCells - 1)
					{
						num2 += this.spacing;
					}
				}
				num = num2 * cellOffset;
			}
			float num3 = -(scrollerOffset * this.ScrollRectSize) + num;
			float num10;
			if (this.loop)
			{
				float num4 = this.GetScrollPositionForCellViewIndex(dataIndex, EnhancedScroller.CellViewPositionEnum.Before) + num3;
				float num5 = this.GetScrollPositionForCellViewIndex(dataIndex + this.NumberOfCells, EnhancedScroller.CellViewPositionEnum.Before) + num3;
				float num6 = this.GetScrollPositionForCellViewIndex(dataIndex + this.NumberOfCells * 2, EnhancedScroller.CellViewPositionEnum.Before) + num3;
				float num7 = Mathf.Abs(this._scrollPosition - num4);
				float num8 = Mathf.Abs(this._scrollPosition - num5);
				float num9 = Mathf.Abs(this._scrollPosition - num6);
				if (num7 < num8)
				{
					if (num7 < num9)
					{
						num10 = num4;
					}
					else
					{
						num10 = num6;
					}
				}
				else if (num8 < num9)
				{
					num10 = num5;
				}
				else
				{
					num10 = num6;
				}
			}
			else
			{
				num10 = this.GetScrollPositionForDataIndex(dataIndex, EnhancedScroller.CellViewPositionEnum.Before) + num3;
			}
			num10 = Mathf.Clamp(num10, 0f, this.GetScrollPositionForCellViewIndex(this._cellViewSizeArray.Count - 1, EnhancedScroller.CellViewPositionEnum.Before));
			if (useSpacing)
			{
				num10 = Mathf.Clamp(num10 - this.spacing, 0f, this.GetScrollPositionForCellViewIndex(this._cellViewSizeArray.Count - 1, EnhancedScroller.CellViewPositionEnum.Before));
			}
			base.StartCoroutine(this.TweenPosition(tweenType, tweenTime, this.ScrollPosition, num10, jumpComplete));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006B30 File Offset: 0x00004F30
		[Obsolete("This is an obsolete method, please use the version of this function with a cell offset.")]
		public void JumpToDataIndex(int dataIndex, EnhancedScroller.CellViewPositionEnum position = EnhancedScroller.CellViewPositionEnum.Before, bool useSpacing = true)
		{
			this.ScrollPosition = this.GetScrollPositionForDataIndex(dataIndex, position);
			if (useSpacing)
			{
				if (position == EnhancedScroller.CellViewPositionEnum.Before)
				{
					this.ScrollPosition = this._scrollPosition - this.spacing;
				}
				else
				{
					this.ScrollPosition = this._scrollPosition + this.spacing;
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006B84 File Offset: 0x00004F84
		public void Snap()
		{
			if (this.NumberOfCells == 0)
			{
				return;
			}
			this._snapJumping = true;
			this.LinearVelocity = 0f;
			this._snapInertia = this._scrollRect.inertia;
			this._scrollRect.inertia = false;
			float position = this.ScrollPosition + this.ScrollRectSize * Mathf.Clamp01(this.snapWatchOffset);
			this._snapCellViewIndex = this.GetCellViewIndexAtPosition(position);
			this._snapDataIndex = this._snapCellViewIndex % this.NumberOfCells;
			this.JumpToDataIndex(this._snapDataIndex, this.snapJumpToOffset, this.snapCellCenterOffset, this.snapUseCellSpacing, this.snapTweenType, this.snapTweenTime, new Action(this.SnapJumpComplete));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006C3C File Offset: 0x0000503C
		public float GetScrollPositionForCellViewIndex(int cellViewIndex, EnhancedScroller.CellViewPositionEnum insertPosition)
		{
			if (this.NumberOfCells == 0)
			{
				return 0f;
			}
			if (cellViewIndex == 0 && insertPosition == EnhancedScroller.CellViewPositionEnum.Before)
			{
				return 0f;
			}
			if (cellViewIndex >= this._cellViewOffsetArray.Count)
			{
				return this._cellViewOffsetArray[this._cellViewOffsetArray.Count - 2];
			}
			if (insertPosition == EnhancedScroller.CellViewPositionEnum.Before)
			{
				return this._cellViewOffsetArray[cellViewIndex - 1] + this.spacing + (float)((this.scrollDirection != EnhancedScroller.ScrollDirectionEnum.Vertical) ? this.padding.left : this.padding.top);
			}
			return this._cellViewOffsetArray[cellViewIndex] + (float)((this.scrollDirection != EnhancedScroller.ScrollDirectionEnum.Vertical) ? this.padding.left : this.padding.top);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006D0E File Offset: 0x0000510E
		public float GetScrollPositionForDataIndex(int dataIndex, EnhancedScroller.CellViewPositionEnum insertPosition)
		{
			return this.GetScrollPositionForCellViewIndex((!this.loop) ? dataIndex : (this._delegate.GetNumberOfCells(this) + dataIndex), insertPosition);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006D36 File Offset: 0x00005136
		public int GetCellViewIndexAtPosition(float position)
		{
			return this._GetCellIndexAtPosition(position, 0, this._cellViewOffsetArray.Count - 1);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00006D50 File Offset: 0x00005150
		private float _ScrollSize
		{
			get
			{
				if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
				{
					return this._container.rect.height - this._scrollRectTransform.rect.height;
				}
				return this._container.rect.width - this._scrollRectTransform.rect.width;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006DB8 File Offset: 0x000051B8
		private void _Resize(bool keepPosition)
		{
			float scrollPosition = this._scrollPosition;
			this._cellViewSizeArray.Clear();
			float num = this._AddCellViewSizes();
			if (this.loop)
			{
				if (num < this.ScrollRectSize)
				{
					int numberOfTimes = Mathf.CeilToInt(this.ScrollRectSize / num);
					this._DuplicateCellViewSizes(numberOfTimes, this._cellViewSizeArray.Count);
				}
				this._loopFirstCellIndex = this._cellViewSizeArray.Count;
				this._loopLastCellIndex = this._loopFirstCellIndex + this._cellViewSizeArray.Count - 1;
				this._DuplicateCellViewSizes(2, this._cellViewSizeArray.Count);
			}
			this._CalculateCellViewOffsets();
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._container.sizeDelta = new Vector2(this._container.sizeDelta.x, this._cellViewOffsetArray.Last() + (float)this.padding.top + (float)this.padding.bottom);
			}
			else
			{
				this._container.sizeDelta = new Vector2(this._cellViewOffsetArray.Last() + (float)this.padding.left + (float)this.padding.right, this._container.sizeDelta.y);
			}
			if (this.loop)
			{
				this._loopFirstScrollPosition = this.GetScrollPositionForCellViewIndex(this._loopFirstCellIndex, EnhancedScroller.CellViewPositionEnum.Before) + this.spacing * 0.5f;
				this._loopLastScrollPosition = this.GetScrollPositionForCellViewIndex(this._loopLastCellIndex, EnhancedScroller.CellViewPositionEnum.After) - this.ScrollRectSize + this.spacing * 0.5f;
				this._loopFirstJumpTrigger = this._loopFirstScrollPosition - this.ScrollRectSize;
				this._loopLastJumpTrigger = this._loopLastScrollPosition + this.ScrollRectSize;
			}
			this._ResetVisibleCellViews();
			if (keepPosition)
			{
				this.ScrollPosition = scrollPosition;
			}
			else if (this.loop)
			{
				this.ScrollPosition = this._loopFirstScrollPosition;
			}
			else
			{
				this.ScrollPosition = 0f;
			}
			this.ScrollbarVisibility = this.scrollbarVisibility;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006FBC File Offset: 0x000053BC
		private float _AddCellViewSizes()
		{
			float num = 0f;
			for (int i = 0; i < this.NumberOfCells; i++)
			{
				this._cellViewSizeArray.Add(this._delegate.GetCellViewSize(this, i) + ((i != 0) ? this._layoutGroup.spacing : 0f));
				num += this._cellViewSizeArray[this._cellViewSizeArray.Count - 1];
			}
			return num;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007038 File Offset: 0x00005438
		private void _DuplicateCellViewSizes(int numberOfTimes, int cellCount)
		{
			for (int i = 0; i < numberOfTimes; i++)
			{
				for (int j = 0; j < cellCount; j++)
				{
					this._cellViewSizeArray.Add(this._cellViewSizeArray[j] + ((j != 0) ? 0f : this._layoutGroup.spacing));
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000709C File Offset: 0x0000549C
		private void _CalculateCellViewOffsets()
		{
			this._cellViewOffsetArray.Clear();
			float num = 0f;
			for (int i = 0; i < this._cellViewSizeArray.Count; i++)
			{
				num += this._cellViewSizeArray[i];
				this._cellViewOffsetArray.Add(num);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000070F4 File Offset: 0x000054F4
		private EnhancedScrollerCellView _GetRecycledCellView(EnhancedScrollerCellView cellPrefab)
		{
			for (int i = 0; i < this._recycledCellViews.Count; i++)
			{
				if (this._recycledCellViews[i].cellIdentifier == cellPrefab.cellIdentifier)
				{
					return this._recycledCellViews.RemoveAt(i);
				}
			}
			return null;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007150 File Offset: 0x00005550
		private void _ResetVisibleCellViews()
		{
			int num;
			int num2;
			this._CalculateCurrentActiveCellRange(out num, out num2);
			int i = 0;
			SmallList<int> smallList = new SmallList<int>();
			while (i < this._activeCellViews.Count)
			{
				if (this._activeCellViews[i].cellIndex < num || this._activeCellViews[i].cellIndex > num2)
				{
					this._RecycleCell(this._activeCellViews[i]);
				}
				else
				{
					smallList.Add(this._activeCellViews[i].cellIndex);
					i++;
				}
			}
			if (smallList.Count == 0)
			{
				for (i = num; i <= num2; i++)
				{
					this._AddCellView(i, EnhancedScroller.ListPositionEnum.Last);
				}
			}
			else
			{
				for (i = num2; i >= num; i--)
				{
					if (i < smallList.First())
					{
						this._AddCellView(i, EnhancedScroller.ListPositionEnum.First);
					}
				}
				for (i = num; i <= num2; i++)
				{
					if (i > smallList.Last())
					{
						this._AddCellView(i, EnhancedScroller.ListPositionEnum.Last);
					}
				}
			}
			this._activeCellViewsStartIndex = num;
			this._activeCellViewsEndIndex = num2;
			this._SetPadders();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000726F File Offset: 0x0000566F
		private void _RecycleAllCells()
		{
			while (this._activeCellViews.Count > 0)
			{
				this._RecycleCell(this._activeCellViews[0]);
			}
			this._activeCellViewsStartIndex = 0;
			this._activeCellViewsEndIndex = 0;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000072A8 File Offset: 0x000056A8
		private void _RecycleCell(EnhancedScrollerCellView cellView)
		{
			this._activeCellViews.Remove(cellView);
			this._recycledCellViews.Add(cellView);
			cellView.transform.SetParent(this._recycledCellViewContainer);
			cellView.dataIndex = 0;
			cellView.cellIndex = 0;
			cellView.active = false;
			if (this.cellViewVisibilityChanged != null)
			{
				this.cellViewVisibilityChanged(cellView);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000730C File Offset: 0x0000570C
		private void _AddCellView(int cellIndex, EnhancedScroller.ListPositionEnum listPosition)
		{
			if (this.NumberOfCells == 0)
			{
				return;
			}
			int dataIndex = cellIndex % this.NumberOfCells;
			EnhancedScrollerCellView cellView = this._delegate.GetCellView(this, dataIndex, cellIndex);
			cellView.cellIndex = cellIndex;
			cellView.dataIndex = dataIndex;
			cellView.active = true;
			cellView.transform.SetParent(this._container, false);
			cellView.transform.localScale = Vector3.one;
			LayoutElement layoutElement = cellView.GetComponent<LayoutElement>();
			if (layoutElement == null)
			{
				layoutElement = cellView.gameObject.AddComponent<LayoutElement>();
			}
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				layoutElement.minHeight = this._cellViewSizeArray[cellIndex] - ((cellIndex <= 0) ? 0f : this._layoutGroup.spacing);
			}
			else
			{
				layoutElement.minWidth = this._cellViewSizeArray[cellIndex] - ((cellIndex <= 0) ? 0f : this._layoutGroup.spacing);
			}
			if (listPosition == EnhancedScroller.ListPositionEnum.First)
			{
				this._activeCellViews.AddStart(cellView);
			}
			else
			{
				this._activeCellViews.Add(cellView);
			}
			if (listPosition == EnhancedScroller.ListPositionEnum.Last)
			{
				cellView.transform.SetSiblingIndex(this._container.childCount - 2);
			}
			else if (listPosition == EnhancedScroller.ListPositionEnum.First)
			{
				cellView.transform.SetSiblingIndex(1);
			}
			if (this.cellViewVisibilityChanged != null)
			{
				this.cellViewVisibilityChanged(cellView);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007474 File Offset: 0x00005874
		private void _SetPadders()
		{
			if (this.NumberOfCells == 0)
			{
				return;
			}
			float num = this._cellViewOffsetArray[this._activeCellViewsStartIndex] - this._cellViewSizeArray[this._activeCellViewsStartIndex];
			float num2 = this._cellViewOffsetArray.Last() - this._cellViewOffsetArray[this._activeCellViewsEndIndex];
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._firstPadder.minHeight = num;
				this._firstPadder.gameObject.SetActive(this._firstPadder.minHeight > 0f);
				this._lastPadder.minHeight = num2;
				this._lastPadder.gameObject.SetActive(this._lastPadder.minHeight > 0f);
			}
			else
			{
				this._firstPadder.minWidth = num;
				this._firstPadder.gameObject.SetActive(this._firstPadder.minWidth > 0f);
				this._lastPadder.minWidth = num2;
				this._lastPadder.gameObject.SetActive(this._lastPadder.minWidth > 0f);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007598 File Offset: 0x00005998
		private void _RefreshActive()
		{
			this._refreshActive = false;
			Vector2 velocity = Vector2.zero;
			if (this.loop)
			{
				if (this._scrollPosition < this._loopFirstJumpTrigger)
				{
					velocity = this._scrollRect.velocity;
					this.ScrollPosition = this._loopLastScrollPosition - (this._loopFirstJumpTrigger - this._scrollPosition);
					this._scrollRect.velocity = velocity;
				}
				else if (this._scrollPosition > this._loopLastJumpTrigger)
				{
					velocity = this._scrollRect.velocity;
					this.ScrollPosition = this._loopFirstScrollPosition + (this._scrollPosition - this._loopLastJumpTrigger);
					this._scrollRect.velocity = velocity;
				}
			}
			int num;
			int num2;
			this._CalculateCurrentActiveCellRange(out num, out num2);
			if (num == this._activeCellViewsStartIndex && num2 == this._activeCellViewsEndIndex)
			{
				return;
			}
			this._ResetVisibleCellViews();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007674 File Offset: 0x00005A74
		private void _CalculateCurrentActiveCellRange(out int startIndex, out int endIndex)
		{
			startIndex = 0;
			endIndex = 0;
			float scrollPosition = this._scrollPosition;
			float position = this._scrollPosition + ((this.scrollDirection != EnhancedScroller.ScrollDirectionEnum.Vertical) ? this._scrollRectTransform.rect.width : this._scrollRectTransform.rect.height);
			startIndex = this.GetCellViewIndexAtPosition(scrollPosition);
			endIndex = this.GetCellViewIndexAtPosition(position);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000076E0 File Offset: 0x00005AE0
		private int _GetCellIndexAtPosition(float position, int startIndex, int endIndex)
		{
			if (startIndex >= endIndex)
			{
				return startIndex;
			}
			int num = (startIndex + endIndex) / 2;
			if (this._cellViewOffsetArray[num] + (float)((this.scrollDirection != EnhancedScroller.ScrollDirectionEnum.Vertical) ? this.padding.left : this.padding.top) >= position)
			{
				return this._GetCellIndexAtPosition(position, startIndex, num);
			}
			return this._GetCellIndexAtPosition(position, num + 1, endIndex);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000774C File Offset: 0x00005B4C
		private void Awake()
		{
			this._scrollRect = base.GetComponent<ScrollRect>();
			this._scrollRectTransform = this._scrollRect.GetComponent<RectTransform>();
			if (this._scrollRect.content != null)
			{
				UnityEngine.Object.DestroyImmediate(this._scrollRect.content.gameObject);
			}
			GameObject gameObject = new GameObject("Container", new Type[]
			{
				typeof(RectTransform)
			});
			gameObject.transform.SetParent(this._scrollRectTransform);
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				gameObject.AddComponent<VerticalLayoutGroup>();
			}
			else
			{
				gameObject.AddComponent<HorizontalLayoutGroup>();
			}
			this._container = gameObject.GetComponent<RectTransform>();
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._container.anchorMin = new Vector2(0f, 1f);
				this._container.anchorMax = Vector2.one;
				this._container.pivot = new Vector2(0.5f, 1f);
			}
			else
			{
				this._container.anchorMin = Vector2.zero;
				this._container.anchorMax = new Vector2(0f, 1f);
				this._container.pivot = new Vector2(0f, 0.5f);
			}
			this._container.offsetMax = Vector2.zero;
			this._container.offsetMin = Vector2.zero;
			this._container.localScale = Vector3.one;
			this._scrollRect.content = this._container;
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._scrollbar = this._scrollRect.verticalScrollbar;
			}
			else
			{
				this._scrollbar = this._scrollRect.horizontalScrollbar;
			}
			this._layoutGroup = this._container.GetComponent<HorizontalOrVerticalLayoutGroup>();
			this._layoutGroup.spacing = this.spacing;
			this._layoutGroup.padding = this.padding;
			this._layoutGroup.childAlignment = TextAnchor.UpperLeft;
			this._layoutGroup.childForceExpandHeight = true;
			this._layoutGroup.childForceExpandWidth = true;
			this._scrollRect.horizontal = (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Horizontal);
			this._scrollRect.vertical = (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical);
			gameObject = new GameObject("First Padder", new Type[]
			{
				typeof(RectTransform),
				typeof(LayoutElement)
			});
			gameObject.transform.SetParent(this._container, false);
			this._firstPadder = gameObject.GetComponent<LayoutElement>();
			gameObject = new GameObject("Last Padder", new Type[]
			{
				typeof(RectTransform),
				typeof(LayoutElement)
			});
			gameObject.transform.SetParent(this._container, false);
			this._lastPadder = gameObject.GetComponent<LayoutElement>();
			gameObject = new GameObject("Recycled Cells", new Type[]
			{
				typeof(RectTransform)
			});
			gameObject.transform.SetParent(this._scrollRect.transform, false);
			this._recycledCellViewContainer = gameObject.GetComponent<RectTransform>();
			this._recycledCellViewContainer.gameObject.SetActive(false);
			this._lastScrollRectSize = this.ScrollRectSize;
			this._lastLoop = this.loop;
			this._lastScrollbarVisibility = this.scrollbarVisibility;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007A8C File Offset: 0x00005E8C
		private void Update()
		{
			if (this._reloadData)
			{
				this.ReloadData();
			}
			if ((this.loop && this._lastScrollRectSize != this.ScrollRectSize) || this.loop != this._lastLoop)
			{
				this._Resize(true);
				this._lastScrollRectSize = this.ScrollRectSize;
				this._lastLoop = this.loop;
			}
			if (this._lastScrollbarVisibility != this.scrollbarVisibility)
			{
				this.ScrollbarVisibility = this.scrollbarVisibility;
				this._lastScrollbarVisibility = this.scrollbarVisibility;
			}
			if (this.LinearVelocity != 0f && !this.IsScrolling)
			{
				this.IsScrolling = true;
				if (this.scrollerScrollingChanged != null)
				{
					this.scrollerScrollingChanged(this, true);
				}
			}
			else if (this.LinearVelocity == 0f && this.IsScrolling)
			{
				this.IsScrolling = false;
				if (this.scrollerScrollingChanged != null)
				{
					this.scrollerScrollingChanged(this, false);
				}
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007B98 File Offset: 0x00005F98
		private void LateUpdate()
		{
			if (this._refreshActive)
			{
				this._RefreshActive();
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007BAB File Offset: 0x00005FAB
		private void OnEnable()
		{
			this._scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this._ScrollRect_OnValueChanged));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007BC9 File Offset: 0x00005FC9
		private void OnDisable()
		{
			this._scrollRect.onValueChanged.RemoveListener(new UnityAction<Vector2>(this._ScrollRect_OnValueChanged));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007BE8 File Offset: 0x00005FE8
		private void _ScrollRect_OnValueChanged(Vector2 val)
		{
			if (this.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical)
			{
				this._scrollPosition = (1f - val.y) * this._ScrollSize;
			}
			else
			{
				this._scrollPosition = val.x * this._ScrollSize;
			}
			this._refreshActive = true;
			if (this.scrollerScrolled != null)
			{
				this.scrollerScrolled(this, val, this._scrollPosition);
			}
			if (this.snapping && !this._snapJumping && Mathf.Abs(this.LinearVelocity) <= this.snapVelocityThreshold)
			{
				this.Snap();
			}
			this._RefreshActive();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007C90 File Offset: 0x00006090
		private void SnapJumpComplete()
		{
			this._snapJumping = false;
			this._scrollRect.inertia = this._snapInertia;
			if (this.scrollerSnapped != null)
			{
				this.scrollerSnapped(this, this._snapCellViewIndex, this._snapDataIndex);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007CD0 File Offset: 0x000060D0
		private IEnumerator TweenPosition(EnhancedScroller.TweenType tweenType, float time, float start, float end, Action tweenComplete)
		{
			if (tweenType == EnhancedScroller.TweenType.immediate || time == 0f)
			{
				this.ScrollPosition = end;
			}
			else
			{
				this._scrollRect.velocity = Vector2.zero;
				this.IsTweening = true;
				if (this.scrollerTweeningChanged != null)
				{
					this.scrollerTweeningChanged(this, true);
				}
				this._tweenTimeLeft = 0f;
				float newPosition = 0f;
				while (this._tweenTimeLeft < time)
				{
					switch (tweenType)
					{
					case EnhancedScroller.TweenType.linear:
						newPosition = this.linear(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.spring:
						newPosition = EnhancedScroller.spring(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInQuad:
						newPosition = EnhancedScroller.easeInQuad(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutQuad:
						newPosition = EnhancedScroller.easeOutQuad(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutQuad:
						newPosition = EnhancedScroller.easeInOutQuad(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInCubic:
						newPosition = EnhancedScroller.easeInCubic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutCubic:
						newPosition = EnhancedScroller.easeOutCubic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutCubic:
						newPosition = EnhancedScroller.easeInOutCubic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInQuart:
						newPosition = EnhancedScroller.easeInQuart(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutQuart:
						newPosition = EnhancedScroller.easeOutQuart(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutQuart:
						newPosition = EnhancedScroller.easeInOutQuart(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInQuint:
						newPosition = EnhancedScroller.easeInQuint(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutQuint:
						newPosition = EnhancedScroller.easeOutQuint(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutQuint:
						newPosition = EnhancedScroller.easeInOutQuint(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInSine:
						newPosition = EnhancedScroller.easeInSine(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutSine:
						newPosition = EnhancedScroller.easeOutSine(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutSine:
						newPosition = EnhancedScroller.easeInOutSine(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInExpo:
						newPosition = EnhancedScroller.easeInExpo(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutExpo:
						newPosition = EnhancedScroller.easeOutExpo(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutExpo:
						newPosition = EnhancedScroller.easeInOutExpo(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInCirc:
						newPosition = EnhancedScroller.easeInCirc(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutCirc:
						newPosition = EnhancedScroller.easeOutCirc(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutCirc:
						newPosition = EnhancedScroller.easeInOutCirc(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInBounce:
						newPosition = EnhancedScroller.easeInBounce(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutBounce:
						newPosition = EnhancedScroller.easeOutBounce(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutBounce:
						newPosition = EnhancedScroller.easeInOutBounce(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInBack:
						newPosition = EnhancedScroller.easeInBack(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutBack:
						newPosition = EnhancedScroller.easeOutBack(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutBack:
						newPosition = EnhancedScroller.easeInOutBack(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInElastic:
						newPosition = EnhancedScroller.easeInElastic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeOutElastic:
						newPosition = EnhancedScroller.easeOutElastic(start, end, this._tweenTimeLeft / time);
						break;
					case EnhancedScroller.TweenType.easeInOutElastic:
						newPosition = EnhancedScroller.easeInOutElastic(start, end, this._tweenTimeLeft / time);
						break;
					}
					if (this.loop)
					{
						if (end > start && newPosition > this._loopLastJumpTrigger)
						{
							newPosition = this._loopFirstScrollPosition + (newPosition - this._loopLastJumpTrigger);
						}
						else if (start > end && newPosition < this._loopFirstJumpTrigger)
						{
							newPosition = this._loopLastScrollPosition - (this._loopFirstJumpTrigger - newPosition);
						}
					}
					this.ScrollPosition = newPosition;
					this._tweenTimeLeft += Time.unscaledDeltaTime;
					yield return null;
				}
				this.ScrollPosition = end;
			}
			if (tweenComplete != null)
			{
				tweenComplete();
			}
			this.IsTweening = false;
			if (this.scrollerTweeningChanged != null)
			{
				this.scrollerTweeningChanged(this, false);
			}
			yield break;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007D10 File Offset: 0x00006110
		private float linear(float start, float end, float val)
		{
			return Mathf.Lerp(start, end, val);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007D1C File Offset: 0x0000611C
		private static float spring(float start, float end, float val)
		{
			val = Mathf.Clamp01(val);
			val = (Mathf.Sin(val * 3.14159274f * (0.2f + 2.5f * val * val * val)) * Mathf.Pow(1f - val, 2.2f) + val) * (1f + 1.2f * (1f - val));
			return start + (end - start) * val;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007D80 File Offset: 0x00006180
		private static float easeInQuad(float start, float end, float val)
		{
			end -= start;
			return end * val * val + start;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007D8E File Offset: 0x0000618E
		private static float easeOutQuad(float start, float end, float val)
		{
			end -= start;
			return -end * val * (val - 2f) + start;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007DA4 File Offset: 0x000061A4
		private static float easeInOutQuad(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * val * val + start;
			}
			val -= 1f;
			return -end / 2f * (val * (val - 2f) - 1f) + start;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007DFB File Offset: 0x000061FB
		private static float easeInCubic(float start, float end, float val)
		{
			end -= start;
			return end * val * val * val + start;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007E0B File Offset: 0x0000620B
		private static float easeOutCubic(float start, float end, float val)
		{
			val -= 1f;
			end -= start;
			return end * (val * val * val + 1f) + start;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007E2C File Offset: 0x0000622C
		private static float easeInOutCubic(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * val * val * val + start;
			}
			val -= 2f;
			return end / 2f * (val * val * val + 2f) + start;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007E80 File Offset: 0x00006280
		private static float easeInQuart(float start, float end, float val)
		{
			end -= start;
			return end * val * val * val * val + start;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007E92 File Offset: 0x00006292
		private static float easeOutQuart(float start, float end, float val)
		{
			val -= 1f;
			end -= start;
			return -end * (val * val * val * val - 1f) + start;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007EB4 File Offset: 0x000062B4
		private static float easeInOutQuart(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * val * val * val * val + start;
			}
			val -= 2f;
			return -end / 2f * (val * val * val * val - 2f) + start;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007F0D File Offset: 0x0000630D
		private static float easeInQuint(float start, float end, float val)
		{
			end -= start;
			return end * val * val * val * val * val + start;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007F21 File Offset: 0x00006321
		private static float easeOutQuint(float start, float end, float val)
		{
			val -= 1f;
			end -= start;
			return end * (val * val * val * val * val + 1f) + start;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007F44 File Offset: 0x00006344
		private static float easeInOutQuint(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * val * val * val * val * val + start;
			}
			val -= 2f;
			return end / 2f * (val * val * val * val * val + 2f) + start;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007FA0 File Offset: 0x000063A0
		private static float easeInSine(float start, float end, float val)
		{
			end -= start;
			return -end * Mathf.Cos(val / 1f * 1.57079637f) + end + start;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007FC0 File Offset: 0x000063C0
		private static float easeOutSine(float start, float end, float val)
		{
			end -= start;
			return end * Mathf.Sin(val / 1f * 1.57079637f) + start;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007FDD File Offset: 0x000063DD
		private static float easeInOutSine(float start, float end, float val)
		{
			end -= start;
			return -end / 2f * (Mathf.Cos(3.14159274f * val / 1f) - 1f) + start;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00008007 File Offset: 0x00006407
		private static float easeInExpo(float start, float end, float val)
		{
			end -= start;
			return end * Mathf.Pow(2f, 10f * (val / 1f - 1f)) + start;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000802F File Offset: 0x0000642F
		private static float easeOutExpo(float start, float end, float val)
		{
			end -= start;
			return end * (-Mathf.Pow(2f, -10f * val / 1f) + 1f) + start;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00008058 File Offset: 0x00006458
		private static float easeInOutExpo(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return end / 2f * Mathf.Pow(2f, 10f * (val - 1f)) + start;
			}
			val -= 1f;
			return end / 2f * (-Mathf.Pow(2f, -10f * val) + 2f) + start;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000080CB File Offset: 0x000064CB
		private static float easeInCirc(float start, float end, float val)
		{
			end -= start;
			return -end * (Mathf.Sqrt(1f - val * val) - 1f) + start;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000080EB File Offset: 0x000064EB
		private static float easeOutCirc(float start, float end, float val)
		{
			val -= 1f;
			end -= start;
			return end * Mathf.Sqrt(1f - val * val) + start;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008110 File Offset: 0x00006510
		private static float easeInOutCirc(float start, float end, float val)
		{
			val /= 0.5f;
			end -= start;
			if (val < 1f)
			{
				return -end / 2f * (Mathf.Sqrt(1f - val * val) - 1f) + start;
			}
			val -= 2f;
			return end / 2f * (Mathf.Sqrt(1f - val * val) + 1f) + start;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008180 File Offset: 0x00006580
		private static float easeInBounce(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			return end - EnhancedScroller.easeOutBounce(0f, end, num - val) + start;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000081AC File Offset: 0x000065AC
		private static float easeOutBounce(float start, float end, float val)
		{
			val /= 1f;
			end -= start;
			if (val < 0.363636374f)
			{
				return end * (7.5625f * val * val) + start;
			}
			if (val < 0.727272749f)
			{
				val -= 0.545454562f;
				return end * (7.5625f * val * val + 0.75f) + start;
			}
			if ((double)val < 0.90909090909090906)
			{
				val -= 0.8181818f;
				return end * (7.5625f * val * val + 0.9375f) + start;
			}
			val -= 0.954545438f;
			return end * (7.5625f * val * val + 0.984375f) + start;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008254 File Offset: 0x00006654
		private static float easeInOutBounce(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			if (val < num / 2f)
			{
				return EnhancedScroller.easeInBounce(0f, end, val * 2f) * 0.5f + start;
			}
			return EnhancedScroller.easeOutBounce(0f, end, val * 2f - num) * 0.5f + end * 0.5f + start;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000082B8 File Offset: 0x000066B8
		private static float easeInBack(float start, float end, float val)
		{
			end -= start;
			val /= 1f;
			float num = 1.70158f;
			return end * val * val * ((num + 1f) * val - num) + start;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000082EC File Offset: 0x000066EC
		private static float easeOutBack(float start, float end, float val)
		{
			float num = 1.70158f;
			end -= start;
			val = val / 1f - 1f;
			return end * (val * val * ((num + 1f) * val + num) + 1f) + start;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000832C File Offset: 0x0000672C
		private static float easeInOutBack(float start, float end, float val)
		{
			float num = 1.70158f;
			end -= start;
			val /= 0.5f;
			if (val < 1f)
			{
				num *= 1.525f;
				return end / 2f * (val * val * ((num + 1f) * val - num)) + start;
			}
			val -= 2f;
			num *= 1.525f;
			return end / 2f * (val * val * ((num + 1f) * val + num) + 2f) + start;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000083AC File Offset: 0x000067AC
		private static float easeInElastic(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (val == 0f)
			{
				return start;
			}
			val /= num;
			if (val == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
			}
			val -= 1f;
			return -(num3 * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val * num - num4) * 6.28318548f / num2)) + start;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008464 File Offset: 0x00006864
		private static float easeOutElastic(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (val == 0f)
			{
				return start;
			}
			val /= num;
			if (val == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
			}
			return num3 * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val * num - num4) * 6.28318548f / num2) + end + start;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008514 File Offset: 0x00006914
		private static float easeInOutElastic(float start, float end, float val)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (val == 0f)
			{
				return start;
			}
			val /= num / 2f;
			if (val == 2f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
			}
			if (val < 1f)
			{
				val -= 1f;
				return -0.5f * (num3 * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val * num - num4) * 6.28318548f / num2)) + start;
			}
			val -= 1f;
			return num3 * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val * num - num4) * 6.28318548f / num2) * 0.5f + end + start;
		}

		// Token: 0x04000073 RID: 115
		public EnhancedScroller.ScrollDirectionEnum scrollDirection;

		// Token: 0x04000074 RID: 116
		public float spacing;

		// Token: 0x04000075 RID: 117
		public RectOffset padding;

		// Token: 0x04000076 RID: 118
		[SerializeField]
		private bool loop;

		// Token: 0x04000077 RID: 119
		[SerializeField]
		private EnhancedScroller.ScrollbarVisibilityEnum scrollbarVisibility;

		// Token: 0x04000078 RID: 120
		public bool snapping;

		// Token: 0x04000079 RID: 121
		public float snapVelocityThreshold;

		// Token: 0x0400007A RID: 122
		public float snapWatchOffset;

		// Token: 0x0400007B RID: 123
		public float snapJumpToOffset;

		// Token: 0x0400007C RID: 124
		public float snapCellCenterOffset;

		// Token: 0x0400007D RID: 125
		public bool snapUseCellSpacing;

		// Token: 0x0400007E RID: 126
		public EnhancedScroller.TweenType snapTweenType;

		// Token: 0x0400007F RID: 127
		public float snapTweenTime;

		// Token: 0x04000080 RID: 128
		public CellViewVisibilityChangedDelegate cellViewVisibilityChanged;

		// Token: 0x04000081 RID: 129
		public ScrollerScrolledDelegate scrollerScrolled;

		// Token: 0x04000082 RID: 130
		public ScrollerSnappedDelegate scrollerSnapped;

		// Token: 0x04000083 RID: 131
		public ScrollerScrollingChangedDelegate scrollerScrollingChanged;

		// Token: 0x04000084 RID: 132
		public ScrollerTweeningChangedDelegate scrollerTweeningChanged;

		// Token: 0x04000087 RID: 135
		private ScrollRect _scrollRect;

		// Token: 0x04000088 RID: 136
		private RectTransform _scrollRectTransform;

		// Token: 0x04000089 RID: 137
		private Scrollbar _scrollbar;

		// Token: 0x0400008A RID: 138
		private RectTransform _container;

		// Token: 0x0400008B RID: 139
		private HorizontalOrVerticalLayoutGroup _layoutGroup;

		// Token: 0x0400008C RID: 140
		private IEnhancedScrollerDelegate _delegate;

		// Token: 0x0400008D RID: 141
		private bool _reloadData;

		// Token: 0x0400008E RID: 142
		private bool _refreshActive;

		// Token: 0x0400008F RID: 143
		private SmallList<EnhancedScrollerCellView> _recycledCellViews = new SmallList<EnhancedScrollerCellView>();

		// Token: 0x04000090 RID: 144
		private LayoutElement _firstPadder;

		// Token: 0x04000091 RID: 145
		private LayoutElement _lastPadder;

		// Token: 0x04000092 RID: 146
		private RectTransform _recycledCellViewContainer;

		// Token: 0x04000093 RID: 147
		private SmallList<float> _cellViewSizeArray = new SmallList<float>();

		// Token: 0x04000094 RID: 148
		private SmallList<float> _cellViewOffsetArray = new SmallList<float>();

		// Token: 0x04000095 RID: 149
		private float _scrollPosition;

		// Token: 0x04000096 RID: 150
		private SmallList<EnhancedScrollerCellView> _activeCellViews = new SmallList<EnhancedScrollerCellView>();

		// Token: 0x04000097 RID: 151
		private int _activeCellViewsStartIndex;

		// Token: 0x04000098 RID: 152
		private int _activeCellViewsEndIndex;

		// Token: 0x04000099 RID: 153
		private int _loopFirstCellIndex;

		// Token: 0x0400009A RID: 154
		private int _loopLastCellIndex;

		// Token: 0x0400009B RID: 155
		private float _loopFirstScrollPosition;

		// Token: 0x0400009C RID: 156
		private float _loopLastScrollPosition;

		// Token: 0x0400009D RID: 157
		private float _loopFirstJumpTrigger;

		// Token: 0x0400009E RID: 158
		private float _loopLastJumpTrigger;

		// Token: 0x0400009F RID: 159
		private float _lastScrollRectSize;

		// Token: 0x040000A0 RID: 160
		private bool _lastLoop;

		// Token: 0x040000A1 RID: 161
		private int _snapCellViewIndex;

		// Token: 0x040000A2 RID: 162
		private int _snapDataIndex;

		// Token: 0x040000A3 RID: 163
		private bool _snapJumping;

		// Token: 0x040000A4 RID: 164
		private bool _snapInertia;

		// Token: 0x040000A5 RID: 165
		private EnhancedScroller.ScrollbarVisibilityEnum _lastScrollbarVisibility;

		// Token: 0x040000A6 RID: 166
		private float _tweenTimeLeft;

		// Token: 0x0200001D RID: 29
		public enum ScrollDirectionEnum
		{
			// Token: 0x040000A8 RID: 168
			Vertical,
			// Token: 0x040000A9 RID: 169
			Horizontal
		}

		// Token: 0x0200001E RID: 30
		public enum CellViewPositionEnum
		{
			// Token: 0x040000AB RID: 171
			Before,
			// Token: 0x040000AC RID: 172
			After
		}

		// Token: 0x0200001F RID: 31
		public enum ScrollbarVisibilityEnum
		{
			// Token: 0x040000AE RID: 174
			OnlyIfNeeded,
			// Token: 0x040000AF RID: 175
			Always,
			// Token: 0x040000B0 RID: 176
			Never
		}

		// Token: 0x02000020 RID: 32
		private enum ListPositionEnum
		{
			// Token: 0x040000B2 RID: 178
			First,
			// Token: 0x040000B3 RID: 179
			Last
		}

		// Token: 0x02000021 RID: 33
		public enum TweenType
		{
			// Token: 0x040000B5 RID: 181
			immediate,
			// Token: 0x040000B6 RID: 182
			linear,
			// Token: 0x040000B7 RID: 183
			spring,
			// Token: 0x040000B8 RID: 184
			easeInQuad,
			// Token: 0x040000B9 RID: 185
			easeOutQuad,
			// Token: 0x040000BA RID: 186
			easeInOutQuad,
			// Token: 0x040000BB RID: 187
			easeInCubic,
			// Token: 0x040000BC RID: 188
			easeOutCubic,
			// Token: 0x040000BD RID: 189
			easeInOutCubic,
			// Token: 0x040000BE RID: 190
			easeInQuart,
			// Token: 0x040000BF RID: 191
			easeOutQuart,
			// Token: 0x040000C0 RID: 192
			easeInOutQuart,
			// Token: 0x040000C1 RID: 193
			easeInQuint,
			// Token: 0x040000C2 RID: 194
			easeOutQuint,
			// Token: 0x040000C3 RID: 195
			easeInOutQuint,
			// Token: 0x040000C4 RID: 196
			easeInSine,
			// Token: 0x040000C5 RID: 197
			easeOutSine,
			// Token: 0x040000C6 RID: 198
			easeInOutSine,
			// Token: 0x040000C7 RID: 199
			easeInExpo,
			// Token: 0x040000C8 RID: 200
			easeOutExpo,
			// Token: 0x040000C9 RID: 201
			easeInOutExpo,
			// Token: 0x040000CA RID: 202
			easeInCirc,
			// Token: 0x040000CB RID: 203
			easeOutCirc,
			// Token: 0x040000CC RID: 204
			easeInOutCirc,
			// Token: 0x040000CD RID: 205
			easeInBounce,
			// Token: 0x040000CE RID: 206
			easeOutBounce,
			// Token: 0x040000CF RID: 207
			easeInOutBounce,
			// Token: 0x040000D0 RID: 208
			easeInBack,
			// Token: 0x040000D1 RID: 209
			easeOutBack,
			// Token: 0x040000D2 RID: 210
			easeInOutBack,
			// Token: 0x040000D3 RID: 211
			easeInElastic,
			// Token: 0x040000D4 RID: 212
			easeOutElastic,
			// Token: 0x040000D5 RID: 213
			easeInOutElastic
		}
	}
}
