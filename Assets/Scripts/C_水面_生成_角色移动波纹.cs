using System.Collections.Generic;
using UnityEngine;

public class C_水面_生成_角色移动波纹 : MonoBehaviour
{
	public GameObject _GO_Prefab_波纹;

	public List<C_水面_生成_角色移动波纹_单个角色信息> _List_水面上的角色;

	public List<C_水面_生成_角色移动波纹_单个角色信息> _List_移除列表;

	public float _间隔 = 0.2f;

	private float timer;

	private void OnEnable()
	{
		_List_水面上的角色 = new List<C_水面_生成_角色移动波纹_单个角色信息>();
		_List_移除列表 = new List<C_水面_生成_角色移动波纹_单个角色信息>();
		timer = _间隔;
	}

	private void OnDisable()
	{
		_List_水面上的角色 = new List<C_水面_生成_角色移动波纹_单个角色信息>();
		_List_移除列表 = new List<C_水面_生成_角色移动波纹_单个角色信息>();
		timer = _间隔;
	}

	private void Update()
	{
		if (timer >= 0f)
		{
			timer -= Time.deltaTime;
			return;
		}
		timer = _间隔;
		if (_List_移除列表.Count > 0)
		{
			foreach (C_水面_生成_角色移动波纹_单个角色信息 item in _List_移除列表)
			{
				_List_水面上的角色.Remove(item);
			}
			_List_移除列表.Clear();
		}
		if (_List_水面上的角色.Count <= 0)
		{
			return;
		}
		foreach (C_水面_生成_角色移动波纹_单个角色信息 item2 in _List_水面上的角色)
		{
			if (item2.F_轮询())
			{
				Object.Instantiate(_GO_Prefab_波纹, item2._Tsf.position, Quaternion.identity).transform.localScale = item2._Tsf.localScale;
			}
		}
	}

	private void F_移除(Transform tsf)
	{
		foreach (C_水面_生成_角色移动波纹_单个角色信息 item in _List_水面上的角色)
		{
			if ((bool)(item._Tsf = tsf))
			{
				_List_移除列表.Add(item);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "水面上的脚" || collision.tag == "Player")
		{
			_List_水面上的角色.Add(new C_水面_生成_角色移动波纹_单个角色信息(collision.transform));
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "水面上的脚" || collision.tag == "Player")
		{
			F_移除(collision.transform);
		}
	}
}
