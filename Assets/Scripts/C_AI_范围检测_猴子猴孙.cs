using DSB;
using UnityEngine;

public class C_AI_范围检测_猴子猴孙 : C_AI_范围检测
{
	[HideInInspector]
	public int _敌人层级 = 6;

	public override Vector2 _目标位置
	{
		get
		{
			if (_范围内发现敌人)
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
			else
			{
				_m目标位置 = DSB_MonoSingleton<GameManager>.Instance._PC.transform.position;
			}
			return _m目标位置;
		}
	}

	public override bool _范围内发现敌人
	{
		get
		{
			if (_List_范围内非玩家单位.Count > 0)
			{
				return true;
			}
			return false;
		}
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == _敌人层级 && !_List_范围内非玩家单位.Contains(collision.transform))
		{
			_List_范围内非玩家单位.Add(collision.transform);
		}
	}

	protected override void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == _敌人层级)
		{
			_List_范围内非玩家单位.Remove(collision.transform);
		}
	}
}
