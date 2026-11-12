using System;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class C_AI_范围检测 : MonoBehaviour
{
	protected List<Transform> _List_范围内非玩家单位 = new List<Transform>();

	public bool _范围内有玩家;

	[Obsolete("这个变量暂时废弃,先不考虑NPS仇恨优先,")]
	public bool _范围内有NPC;

	protected Vector2 _m目标位置;

	public virtual Vector2 _目标位置
	{
		get
		{
			if (_范围内发现敌人)
			{
				if (_List_范围内非玩家单位.Count > 0)
				{
					if (_List_范围内非玩家单位[0] == null)
					{
						_List_范围内非玩家单位.Clear();
					}
					else
					{
						_m目标位置 = _List_范围内非玩家单位[0].position;
					}
				}
				else if (_范围内有玩家)
				{
					_m目标位置 = DSB_MonoSingleton<GameManager>.Instance._PC.transform.position;
				}
			}
			return _m目标位置;
		}
	}

	public virtual bool _范围内发现敌人
	{
		get
		{
			if (_List_范围内非玩家单位.Count > 0 || (_范围内有玩家 && DSB_MonoSingleton<GameManager>.Instance._PC._可以被敌人发现))
			{
				return true;
			}
			return false;
		}
	}

	protected void OnDisable()
	{
		Clear();
	}

	public void Clear()
	{
		_List_范围内非玩家单位.Clear();
		_m目标位置 = base.transform.position;
		_范围内有玩家 = false;
		_范围内有NPC = false;
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (collision.gameObject.layer == 3)
			{
				_范围内有玩家 = true;
			}
			else if (collision.gameObject.layer != 13 && !_List_范围内非玩家单位.Contains(collision.transform))
			{
				_List_范围内非玩家单位.Add(collision.transform);
			}
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		if (!(collision.tag == "Player"))
		{
			return;
		}
		if (collision.gameObject.layer == 13)
		{
			_范围内有NPC = false;
		}
		if (collision.gameObject.layer == 3)
		{
			_范围内有玩家 = false;
			if (_List_范围内非玩家单位.Count < 1)
			{
				_m目标位置 = collision.transform.position;
			}
		}
		else
		{
			if (_List_范围内非玩家单位.Count <= 1)
			{
				_m目标位置 = collision.transform.position;
			}
			_List_范围内非玩家单位.Remove(collision.transform);
		}
	}
}
