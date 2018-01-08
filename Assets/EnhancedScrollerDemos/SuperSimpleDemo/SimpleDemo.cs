using System;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EnhancedScrollerDemos.SuperSimpleDemo
{
	// Token: 0x02000014 RID: 20
	public class SimpleDemo : MonoBehaviour, IEnhancedScrollerDelegate
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00005E4A File Offset: 0x0000424A
		private void Start()
		{
			this.scroller.Delegate = this;
			this.script = UnityEngine.Object.FindObjectOfType<HomeScript>();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005E63 File Offset: 0x00004263
		public void LoadLargeData()
		{
			this.scroller.ReloadData();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005E70 File Offset: 0x00004270
		private void LoadSmallData()
		{
			this._data = new SmallList<Data>();
			this._data.Add(new Data
			{
				someText = "A"
			});
			this._data.Add(new Data
			{
				someText = "B"
			});
			this._data.Add(new Data
			{
				someText = "C"
			});
			this.scroller.ReloadData();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005EEA File Offset: 0x000042EA
		public void LoadLargeDataButton_OnClick()
		{
			this.LoadLargeData();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005EF2 File Offset: 0x000042F2
		public void LoadSmallDataButton_OnClick()
		{
			this.LoadSmallData();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005EFA File Offset: 0x000042FA
		public int GetNumberOfCells(EnhancedScroller scroller)
		{
			return (HomeScript.images_Count + 4) / 2;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005F05 File Offset: 0x00004305
		public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
		{
			return 300f;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005F0C File Offset: 0x0000430C
		public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
		{
			CellView cellView = scroller.GetCellView(this.cellViewPrefab) as CellView;
			cellView.name = "Cell" + dataIndex.ToString();
			this.tempIndex = cellIndex * 2;
			if (this.tempIndex < 4)
			{
				this.setAppData(cellView, cellIndex);
			}
			else
			{
				this.setData(cellView, cellIndex);
			}
			return cellView;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005F74 File Offset: 0x00004374
		private void setAppData(CellView cell, int index)
		{
			index = index * 2 - 4;
			cell.Image1.name = index + string.Empty;
			cell.Image1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = this.script.appIcons[this.tempIndex];
			cell.Image1.transform.GetChild(1).gameObject.name = "lock" + index;
			cell.Image1.transform.GetChild(1).gameObject.SetActive(false);
			cell.Image1.GetComponent<Button>().onClick.RemoveAllListeners();
			cell.Image1.GetComponent<Button>().onClick.AddListener(new UnityAction(this.script.ImageSelection));
			index++;
			if (index < 0)
			{
				cell.Image2.name = index + string.Empty;
				cell.Image2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = this.script.appIcons[this.tempIndex + 1];
				cell.Image2.transform.GetChild(1).gameObject.name = "lock" + index;
				cell.Image2.transform.GetChild(1).gameObject.SetActive(false);
				cell.Image2.GetComponent<Button>().onClick.RemoveAllListeners();
				cell.Image2.GetComponent<Button>().onClick.AddListener(new UnityAction(this.script.ImageSelection));
			}
			else
			{
				cell.Image2.name = index + string.Empty;
				cell.Image2.name = index + string.Empty;
				cell.Image2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = HomeScript.images[index];
				cell.Image2.transform.GetChild(1).gameObject.name = "lock" + index;
				if (index >= 50)
				{
					if (PlayerPrefs.GetInt("Pic" + index) == 1)
					{
						cell.Image2.transform.GetChild(1).gameObject.SetActive(false);
					}
					else
					{
						cell.Image2.transform.GetChild(1).gameObject.SetActive(true);
					}
				}
				else
				{
					cell.Image2.transform.GetChild(1).gameObject.SetActive(false);
				}
				cell.Image2.GetComponent<Button>().onClick.RemoveAllListeners();
				cell.Image2.GetComponent<Button>().onClick.AddListener(new UnityAction(this.script.ImageSelection));
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006288 File Offset: 0x00004688
		private void setData(CellView cell, int index)
		{
			index = index * 2 - 4;
			cell.Image1.name = index + string.Empty;
			cell.Image1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = HomeScript.images[index];
			cell.Image1.transform.GetChild(1).gameObject.name = "lock" + index;
			if (index >= 50)
			{
				if (PlayerPrefs.GetInt("Pic" + index) == 1)
				{
					cell.Image1.transform.GetChild(1).gameObject.SetActive(false);
				}
				else
				{
					cell.Image1.transform.GetChild(1).gameObject.SetActive(true);
				}
			}
			else
			{
				cell.Image1.transform.GetChild(1).gameObject.SetActive(false);
			}
			cell.Image1.GetComponent<Button>().onClick.RemoveAllListeners();
			cell.Image1.GetComponent<Button>().onClick.AddListener(new UnityAction(this.script.ImageSelection));
			index++;
			cell.Image2.name = index + string.Empty;
			cell.Image2.name = index + string.Empty;
			cell.Image2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = HomeScript.images[index];
			cell.Image2.transform.GetChild(1).gameObject.name = "lock" + index;
			if (index >= 50)
			{
				if (PlayerPrefs.GetInt("Pic" + index) == 1)
				{
					cell.Image2.transform.GetChild(1).gameObject.SetActive(false);
				}
				else
				{
					cell.Image2.transform.GetChild(1).gameObject.SetActive(true);
				}
			}
			else
			{
				cell.Image2.transform.GetChild(1).gameObject.SetActive(false);
			}
			cell.Image2.GetComponent<Button>().onClick.RemoveAllListeners();
			cell.Image2.GetComponent<Button>().onClick.AddListener(new UnityAction(this.script.ImageSelection));
		}

		// Token: 0x0400006E RID: 110
		private SmallList<Data> _data;

		// Token: 0x0400006F RID: 111
		public EnhancedScroller scroller;

		// Token: 0x04000070 RID: 112
		public EnhancedScrollerCellView cellViewPrefab;

		// Token: 0x04000071 RID: 113
		private HomeScript script;

		// Token: 0x04000072 RID: 114
		private int tempIndex;
	}
}
