using UnityEngine;

public class PlayerState_化形_寅虎_格挡 : PlayerState_化形_Base
{
	private DataInfo_Player招式 格挡;

	public PlayerState_化形_寅虎_格挡()
	{
		格挡 = MyDataManager.F_Get_Player_招式系数(200204);
	}

	public override void OnEnter()
	{
		_Data._Rb2D.mass = 999f;
		_Data._Mono组件_化形._Ani.Play("Player_振刀", 0, 0f);
	}

	public override void OnUpdate()
	{
		_Data._Curr神力 -= 格挡._气力消耗 * Time.deltaTime;
		if (_Data._Curr神力 <= 0f)
		{
			_Data.F_退出化形();
		}
	}

	public override void OnExit()
	{
		_Data._Rb2D.mass = 5f;
	}

	public override void F_跳跃Up()
	{
		F_化形_ChangeState(TypeName_化形.正常移动);
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (attackInfo.atk_有僵直)
		{
			_Data._MonoPlayer.F_玩家转向(attackInfo.atk_Pos.x - _Data._MonoPlayer.transform.position.x);
			_Data._Mono组件_化形._Ani.Play("Player_振刀", 0, 0f);
			_Data._Curr蓄力 += 格挡._棍势值;
		}
		else
		{
			base.BeAttack(attackInfo);
		}
	}
}
