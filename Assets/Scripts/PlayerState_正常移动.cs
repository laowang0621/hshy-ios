using System.Collections.Generic;
using DSB;
using UnityEngine;

public class PlayerState_正常移动 : Player_StateBase
{
	public override void OnEnter()
	{
		base._Ani.SetFloat("AK", 0f);
		_Timer = 0.3f;
		base._Mono._m开启反弹弹道 = false;
		base._Ani.speed = 1f;
		base._Rb2D.drag = 5f;
		base._Rb2D.gravityScale = 0f;
		base._Mono.gameObject.layer = 3;
		base._Mono组件._SR_影子.SetAlpha(1f);
		_Data._Mono组件._Co_自身碰撞器.enabled = true;
		base._Mono组件._Co_自身碰撞器.direction = CapsuleDirection2D.Horizontal;
		base._Mono组件._Co_自身碰撞器.offset = base._Mono组件._碰撞器_默认偏移;
		base._Mono组件._Co_自身碰撞器.size = base._Mono组件._碰撞器_默认大小;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		if (F_PlayerMove(GameManager._10倍移速 ? 40 : 10))
		{
			base._Ani.Play("跑步");
		}
		else
		{
			base._Ani.Play("站立");
		}
	}

	public override void F_喝酒()
	{
		if (_Data._Curr酒 > 0)
		{
			ChangeState(Player_StateName_Type.喝酒);
		}
	}

	public override void F_棍花Down()
	{
		ChangeState(Player_StateName_Type.棍花);
	}

	public override void F_棍花Up()
	{
	}

	public override void F_跳跃Down()
	{
		ChangeState(Player_StateName_Type.跳跃);
	}

	public override void F_轻攻击Down()
	{
		if (_Data._State_攻击连段_判定 == Player_StateName_Type.轻棍4)
		{
			_Data._State_攻击连段_判定 = Player_StateName_Type.轻棍1;
		}
		ChangeToState(_Data._State_攻击连段_判定);
	}

	public override void F_拿起爆竹()
	{
		ChangeToState(Player_StateName_Type.爆竹);
	}

	public override void F_重攻击Down()
	{
		if (_Data._Curr豆子 >= 1 && _Data._重攻击衔接切手技)
		{
			ChangeToState(Player_StateName_Type.破棍式);
			_Data._Curr豆子--;
		}
		else
		{
			ChangeState(Player_StateName_Type.蓄力);
		}
	}

	public override void F_重攻击Up()
	{
	}

	public override void F_翻滚()
	{
		ChangeState(Player_StateName_Type.翻滚);
	}

	public override void F_定身术()
	{
		if (_Data._IsOk_定身术)
		{
			Collider2D[] array = Physics2D.OverlapBoxAll(base._Mono.transform.position, new Vector2(30f, 12f), 0f, LayerMask.GetMask("Enemy"));
			if (array.Length != 0)
			{
				_Data._List_定身目标 = new List<I定身术>();
				Collider2D[] array2 = array;
				foreach (Collider2D collider2D in array2)
				{
					if (collider2D.TryGetComponent<I定身术>(out var component))
					{
						_Data._List_定身目标.Add(component);
						MyLog.Log($"F_定身术>>{collider2D.name}>>|>{component}<|");
					}
				}
				Vector2 a = base._Mono.transform.position;
				Collider2D collider2D2 = array[0];
				float num = Vector2.Distance(a, collider2D2.transform.position);
				for (int j = 1; j < array.Length; j++)
				{
					float num2 = Vector2.Distance(a, array[j].transform.position);
					if (num2 < num)
					{
						num = num2;
						collider2D2 = array[j];
					}
				}
				_Data._定身目标 = collider2D2.GetComponentInParent<I定身术>();
				MyLog.Log($"F_定身术>>{collider2D2.name}>>|>{_Data._定身目标}<|");
				ChangeState(Player_StateName_Type.定身术);
			}
			else
			{
				MyLog.LogWarning("范围内无目标");
			}
		}
		else
		{
			MyLog.LogWarning("定身术在冷却 或 未解锁");
		}
	}

	public override void F_出租云()
	{
		MyLog.Log("尝试释放>>>>>出租云");
		if (_Data._IsOk_出租云)
		{
			ChangeToState(Player_StateName_Type.出租云);
		}
		else
		{
			MyLog.LogWarning("出租云 在冷却 或 未解锁");
		}
	}

	public override void F_隐身()
	{
		MyLog.Log("尝试释放>>>>>隐身");
		if (_Data._IsOk_聚形散气)
		{
			ChangeState(Player_StateName_Type.聚形散气);
		}
		else
		{
			MyLog.LogWarning("聚形散气 在冷却 或 未解锁");
		}
	}

	public override void F_猴子猴孙()
	{
		MyLog.Log("尝试释放>>>>>猴子猴孙");
		if (_Data._IsOk_身外身法)
		{
			ChangeState(Player_StateName_Type.猴子猴孙);
		}
		else
		{
			MyLog.LogWarning("身外身法 在冷却 或 未解锁");
		}
	}

	public override void F_化形()
	{
		MyLog.Log("尝试释放>>>>>广智救我");
		if (_Data._IsOk_化形)
		{
			ChangeState(Player_StateName_Type.化形);
		}
		else
		{
			MyLog.LogWarning("化形 在冷却 或 未解锁");
		}
	}

	public override void F_精魄()
	{
		MyLog.Log("尝试释放>>>>>精魄");
		if (_Data._IsOk_精魄)
		{
			ChangeState(Player_StateName_Type.精魄);
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_精魄();
		}
		else
		{
			MyLog.LogWarning("精魄 在冷却 或 未解锁");
		}
	}

	public override void F_装备AK()
	{
		MyLog.Log("尝试释放>>>>>出租云");
		if (_Data._IsOk_AK)
		{
			ChangeToState(Player_StateName_Type.AK);
		}
		else
		{
			MyLog.LogWarning("AK 在冷却 或 未解锁");
		}
	}
}
