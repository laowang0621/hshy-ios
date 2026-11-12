using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_生成_金池_僵尸 : MonoBehaviour
{
	private List<Transform> _List_生成点位;

	public Enemy_金池_僵尸 _金池僵尸Prefab;

	public GameObject _GO_烟雾特效Prefab;

	private void Start()
	{
		_List_生成点位 = new List<Transform>();
		foreach (Transform item in base.transform)
		{
			_List_生成点位.Add(item);
		}
		base.transform.parent = null;
		base.transform.position = Vector3.zero;
	}

	public void F_生成一只僵尸(Transform tag, Action 死亡回调 = null)
	{
		int index = UnityEngine.Random.Range(0, _List_生成点位.Count);
		UnityEngine.Object.Instantiate(_GO_烟雾特效Prefab, _List_生成点位[index].position + new Vector3(0f, 1f, -3f), Quaternion.identity);
		UnityEngine.Object.Instantiate(_金池僵尸Prefab, _List_生成点位[index].position, Quaternion.identity).F_SetTaget(tag, 死亡回调);
	}
}
