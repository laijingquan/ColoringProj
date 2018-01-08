using System;

namespace EnhancedUI.EnhancedScroller
{
	// Token: 0x02000023 RID: 35
	public interface IEnhancedScrollerDelegate
	{
		// Token: 0x060000EB RID: 235
		int GetNumberOfCells(EnhancedScroller scroller);

		// Token: 0x060000EC RID: 236
		float GetCellViewSize(EnhancedScroller scroller, int dataIndex);

		// Token: 0x060000ED RID: 237
		EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex);
	}
}
