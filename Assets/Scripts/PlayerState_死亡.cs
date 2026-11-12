using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public class PlayerState_死亡 : Player_StateBase
{
	private Color _color_影子;

	private Coroutine _Cor_死亡;

	private DataInfo_Player招式 四姐名刀 = MyDataManager.F_Get_Player_招式系数(2016);

	public override void OnEnter()
	{
		if (_Data.F_可以触发四姐名刀())
		{
			_Data._已经触发四姐名刀 = true;
			_Data._名刀司命 = true;
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base._Mono.gameObject, 四姐名刀._气力消耗, delegate
			{
				_Data._名刀司命 = false;
			});
			base._Mono.StartCoroutine(Cor_名刀());
		}
		else
		{
			_Data._Is死亡 = true;
			GameManager._CanPlay = false;
			_color_影子 = base._Mono组件._SR_影子.color;
			_Cor_死亡 = base._Mono.StartCoroutine(Cor_死亡());
		}
	}

	private IEnumerator Cor_名刀()
	{
		base._Ani.Play("死亡前跳跃");
		base._Rb2D.velocity = 30f * new Vector2(base._Mono.transform.position.x - base._Mono._BeAttack_Pos.x, 0f).normalized;
		base._Mono.F_玩家转向_强制(base._Mono._BeAttack_Pos.x);
		base._Mono组件._SR_名刀.DOFade(0.8f, 0.25f);
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_名刀司命();
		yield return new WaitForSeconds(0.45f);
		float seconds = Mathf.Clamp(四姐名刀._气力消耗 - 0.45f - 1f, 0.1f, 四姐名刀._气力消耗);
		ChangeState(Player_StateName_Type.正常移动);
		yield return new WaitForSeconds(seconds);
		base._Mono组件._SR_名刀.DOFade(0f, 1f);
	}

	private IEnumerator Cor_死亡()
	{
		base._Ani.Play("死亡前跳跃");
		base._Rb2D.velocity = 30f * new Vector2(base._Mono.transform.position.x - base._Mono._BeAttack_Pos.x, 0f).normalized;
		base._Mono.F_玩家转向_强制(base._Mono._BeAttack_Pos.x);
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_死亡();
		yield return new WaitForSeconds(0.5f);
		base._Rb2D.bodyType = RigidbodyType2D.Kinematic;
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("倒地");
		yield return new WaitForSeconds(0.75f);
		WaitForSeconds t1 = new WaitForSeconds(0.1f);
		for (int i = 0; i < 5; i++)
		{
			base._Mono组件._SR_Model.SetAlpha(0f);
			base._Mono组件._SR_影子.SetAlpha(0f);
			yield return t1;
			base._Mono组件._SR_Model.SetAlpha(1f);
			base._Mono组件._SR_影子.SetAlpha(1f);
			yield return t1;
		}
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._SR_影子.SetAlpha(0f);
		yield return t1;
		_Cor_死亡 = null;
		DSB_MonoSingleton<GameManager>.Instance.F_重生();
	}

	public override void OnExit()
	{
		if (_Cor_死亡 != null)
		{
			base._Mono.StopCoroutine(_Cor_死亡);
		}
		base._Mono组件._打飞机云.SetActive(value: false);
		base._Mono组件._SR_Model.DOKill();
		base._Mono组件._SR_影子.DOKill();
		base._Rb2D.bodyType = RigidbodyType2D.Dynamic;
		base._Mono组件._SR_Model.color = new Color(1f, 1f, 1f, 1f);
		base._Mono组件._SR_影子.color = _color_影子;
	}

	public override void OnUpdate()
	{
	}
}
