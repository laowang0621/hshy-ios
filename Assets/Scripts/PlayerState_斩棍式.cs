using System.Collections;
using UnityEngine;

public class PlayerState_斩棍式 : Player_StateBase
{
	private Coroutine _Cor_动作中向前力;

	public PlayerState_斩棍式()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1016);
	}

	public override void OnEnter()
	{
		_Data._重攻击衔接切手技 = false;
		F_玩家转向(MyInputManager.Horizontal);
		base._Ani.Play("斩棍式");
		_Data._释放蓄力状态 = true;
		base._Rb2D.velocity = Vector2.zero;
		base._Rb2D.mass = 9999f;
		_Cor_动作中向前力 = base._Mono.StartCoroutine(Cor_动作中向前力());
		_Timer = _Data._Dict_AniName_time["斩棍式"];
		_NextState = Player_StateName_Type.正常移动;
		base._Mono组件._C_攻击判断._击退力度 = 6f;
		F_创建攻击命中回调();
	}

	public override void F_攻击命中回调()
	{
	}

	public override void OnExit()
	{
		base._Rb2D.mass = 5f;
		base._Mono组件._C_攻击判断._攻击命中回调 -= F_攻击命中回调;
		if (_Cor_动作中向前力 != null)
		{
			base._Mono.StopCoroutine(_Cor_动作中向前力);
			_Cor_动作中向前力 = null;
		}
		_Data._释放蓄力状态 = false;
		base._Rb2D.drag = 5f;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}

	private IEnumerator Cor_动作中向前力_Old()
	{
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 6, 0f);
		yield return new WaitForSeconds(0.3f);
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 8, 0f);
		yield return new WaitForSeconds(0.1f);
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 4, 0f);
	}

	private IEnumerator Cor_动作中向前力()
	{
		base._Rb2D.velocity = Vector2.zero;
		for (int i = 0; i < 4; i++)
		{
			base._Rb2D.velocity = new Vector2((float)(_Data._朝向右边 ? 1 : (-1)) * (14f - (float)i * 1.25f), 0f);
			yield return new WaitForSeconds(0.075f);
		}
		yield return new WaitForSeconds(0.6f);
	}
}
