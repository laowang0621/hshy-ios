using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_御剑道士_同步舞剑控制器 : MonoBehaviour
{
	public List<EnemyAI_御剑道士> _List_御剑道士;

	private void OnEnable()
	{
		StartCoroutine(Cor_同步舞剑控制器());
	}

	private IEnumerator Cor_同步舞剑控制器()
	{
		while (true)
		{
			float seconds = Random.Range(1.5f, 3.5f);
			yield return new WaitForSeconds(seconds);
			F_尝试发送舞剑信号();
		}
	}

	private void F_尝试发送舞剑信号()
	{
		MyLog.LogWarning("F_尝试发送舞剑信号");
		bool flag = false;
		foreach (EnemyAI_御剑道士 item in _List_御剑道士)
		{
			if (item.F_是否回归原点())
			{
				item._开启音效 = false;
				if (!flag)
				{
					flag = true;
					item._开启音效 = true;
				}
				item.F_舞剑(1);
			}
		}
		MyLog.LogWarning("F_尝试发送舞剑信号66666666666666");
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
}
