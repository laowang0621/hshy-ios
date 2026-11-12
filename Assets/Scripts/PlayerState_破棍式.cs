using UnityEngine;

public class PlayerState_破棍式 : Player_StateBase
{
	public PlayerState_破棍式()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1015);
	}

	public override void OnEnter()
	{
		base._Rb2D.mass = 9999f;
		base._Mono组件._C_攻击判断._击退力度 = 14f;
		base._Rb2D.velocity = Vector2.zero;
		F_玩家转向(MyInputManager.Horizontal);
		base._Rb2D.velocity = new Vector2(((!_Data._朝向右边) ? 1 : (-1)) * 10, 0f);
		_Data._可以识破 = true;
		base._Ani.Play("破棍式", 0, 0f);
		_Timer = _Data._Dict_AniName_time["破棍式"];
		_NextState = Player_StateName_Type.正常移动;
		_Data._识破_打中人 = false;
		base._Mono组件._C_攻击判断._击退力度 = 6f;
		_Data._State_攻击连段_判定Timer = _Timer + 0.3f;
		_Data._State_攻击连段_判定 = Player_StateName_Type.轻棍1;
		_Data._重攻击衔接切手技 = false;
		F_创建攻击命中回调();
	}

	public override void F_攻击命中回调()
	{
		base.F_攻击命中回调();
		_Data._识破_打中人 = true;
		if (_Data._成功识破)
		{
			_Data._Curr豆子++;
		}
	}

	public override void OnExit()
	{
		base._Rb2D.mass = 5f;
		_Data._可以识破 = false;
		base._Mono组件._C_攻击判断._攻击命中回调 -= F_攻击命中回调;
		_Data._成功识破 = false;
		base._Rb2D.drag = 5f;
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0.1f)
		{
			if (_Data._识破_打中人 && _NextState == Player_StateName_Type.斩棍式 && _Data._Curr豆子 >= 1)
			{
				ChangeToState(Player_StateName_Type.斩棍式);
				_Data._Curr豆子--;
			}
			else if (_NextState == Player_StateName_Type.轻棍1)
			{
				ChangeState(Player_StateName_Type.轻棍1);
			}
		}
		if (_Timer <= 0f)
		{
			ChangeState(Player_StateName_Type.正常移动);
		}
	}

	public override void F_重攻击Down()
	{
		_NextState = Player_StateName_Type.斩棍式;
	}

	public override void F_轻攻击Down()
	{
		_NextState = Player_StateName_Type.轻棍1;
	}
}
