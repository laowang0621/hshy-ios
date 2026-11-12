using UnityEngine;

public class ItemUI_EnemyHP_带盾 : ItemUI_EnemyHP
{
	[SerializeField]
	private ItemUI_生命值 _ItemUI_盾值;

	public void F_SetVal_盾(float currVal, float maxVal)
	{
		_ItemUI_盾值.F_SetVal(currVal, maxVal);
	}
}
