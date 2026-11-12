using UnityEngine;

public class PlayerState_跳跃重击 : Player_StateBase
{
	private float _EnterDrag;

	public PlayerState_跳跃重击()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1014);
	}

	public override void OnEnter()
	{
		_Data._Is腾空 = true;
		_Data._释放蓄力状态 = true;
		F_玩家转向(MyInputManager.Horizontal);
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.SetTrigger("跳跃重击");
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 30, 0f);
		_Timer = _Data._Dict_AniName_time["跳跃重击"];
		_NextState = Player_StateName_Type.正常移动;
		base._Mono组件._C_攻击判断._击退力度 = 8f;
		F_创建攻击命中回调();
	}

	public override void OnExit()
	{
		_Data._Is腾空 = false;
		_Data._释放蓄力状态 = false;
		base._Mono组件._C_攻击判断._攻击命中回调 -= F_攻击命中回调;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}
}
