using UnityEngine;

public class ItemUI_EnemyHP : ItemUIBase<ItemUI_EnemyHP>
{
	[SerializeField]
	private ItemUI_生命值 _ItemUI_生命值;

	public void F_SetVal(float currVal, float maxVal)
	{
		_ItemUI_生命值.F_SetVal(currVal, maxVal);
	}
}
