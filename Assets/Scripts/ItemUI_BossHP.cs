using UnityEngine;
using UnityEngine.UI;

public class ItemUI_BossHP : ItemUIBase<ItemUI_BossHP>
{
	[SerializeField]
	private Text _Text_Name;

	[SerializeField]
	private ItemUI_生命值 _ItemUI_生命值;

	public void F_SetVal(float currVal, float maxVal)
	{
		_ItemUI_生命值.F_SetVal(currVal, maxVal);
	}

	public void F_设置名字(string str)
	{
		_Text_Name.text = str.Split('_')[0];
	}
}
