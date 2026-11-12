using System.Collections.Generic;
using DSB;
using UnityEngine;

public class Player_蓄力特效 : MonoBehaviour
{
	public List<GameObject> _List_蓄力阶段特效;

	public void F_Init(Player_StateData pc)
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_豆子_Change._EventID, F_豆子变化切换特效);
	}

	public void F_豆子变化切换特效(DSB_EventArgs args)
	{
		EventArgs_Player_豆子_Change eventArgs_Player_豆子_Change = (EventArgs_Player_豆子_Change)args;
		for (int i = 0; i < _List_蓄力阶段特效.Count; i++)
		{
			_List_蓄力阶段特效[i].SetActive(i == eventArgs_Player_豆子_Change._当前Val);
		}
	}
}
