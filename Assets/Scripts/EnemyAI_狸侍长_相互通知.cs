using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_狸侍长_相互通知 : MonoBehaviour
{
	public List<EnemyAI_狸侍长> _相互通知列表;

	private void Start()
	{
		foreach (EnemyAI_狸侍长 item in _相互通知列表)
		{
			item.F_注册相互通知(this);
		}
	}

	public void F_相互通知发现玩家()
	{
		foreach (EnemyAI_狸侍长 item in _相互通知列表)
		{
			item._死追玩家 = true;
		}
	}
}
