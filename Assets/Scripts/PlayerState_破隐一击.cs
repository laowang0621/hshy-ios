using DSB;
using UnityEngine;

public class PlayerState_破隐一击 : Player_StateBase
{
	public PlayerState_破隐一击()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1017);
	}

	public override void OnEnter()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_破隐一击();
		Time.timeScale = 0.8f;
		base._Ani.Play("破影一击");
		base._Rb2D.velocity = Vector2.zero;
		F_玩家转向(MyInputManager.Horizontal);
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 50, 0f);
		_Timer = 0.33f;
		DSB_MonoSingleton<CameraController>.Instance.F_屏幕缩放_闪一下(_Timer);
		_NextState = Player_StateName_Type.正常移动;
		_Data._重攻击衔接切手技 = false;
		base._Mono组件._C_攻击判断._击退力度 = 12f;
		F_创建攻击命中回调();
	}

	public override void F_创建攻击命中回调(DataInfo_Player招式 Data_Player招式_系数, bool 是强破坏力 = false)
	{
		base._Mono组件._C_攻击判断._伤害 = _Data._攻击力 * Data_Player招式_系数._伤害系数 + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1701);
		_Data._Curr气力 -= Data_Player招式_系数._气力消耗;
		base._Mono组件._C_攻击判断._攻击命中回调 += F_攻击命中回调;
		base._Mono组件._C_攻击判断._僵直伤害 = Data_Player招式_系数._僵直伤害;
	}

	public override void F_攻击命中回调()
	{
		base.F_攻击命中回调();
		DSB_MonoSingleton<CameraController>.Instance.F_屏幕震动();
	}

	public override void OnExit()
	{
		Time.timeScale = 1f;
		base._Mono组件._C_攻击判断._攻击命中回调 -= F_攻击命中回调;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}

	public override void F_翻滚()
	{
		ChangeState(Player_StateName_Type.翻滚);
	}
}
