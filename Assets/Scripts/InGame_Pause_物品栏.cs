using UnityEngine;

public class InGame_Pause_物品栏 : MonoBehaviour
{
	private InGame_物品栏_物品[] _List_物品;

	public void F_更新物品栏UI()
	{
		if (_List_物品 == null)
		{
			_List_物品 = GetComponentsInChildren<InGame_物品栏_物品>();
		}
		InGame_物品栏_物品[] list_物品 = _List_物品;
		for (int i = 0; i < list_物品.Length; i++)
		{
			list_物品[i].F_SetUI();
		}
	}
}
