using System.Collections;
using UnityEngine;

public class NPCState_四姐_回血 : NPCState_同伴
{
	private Coroutine _Cor_动作中向前力;

	private DataInfo_Player招式 回血招式参数;

	private NPCAI_四姐 _Mono同伴_四姐;

	public override NPCState_同伴 Init(NPCAI_同伴 mono)
	{
		base.Init(mono);
		_Mono同伴_四姐 = mono as NPCAI_四姐;
		回血招式参数 = MyDataManager.F_Get_Player_招式系数(2014);
		return this;
	}

	public override void OnEnter()
	{
		_Mono同伴_四姐._回血术Timer = 回血招式参数._气力消耗;
		if (_Cor_动作中向前力 != null)
		{
			_Mono_同伴.StopCoroutine(_Cor_动作中向前力);
		}
		_Cor_动作中向前力 = _Mono_同伴.StartCoroutine(Cor_动作中向前力());
	}

	public override void OnExit()
	{
		if (_Cor_动作中向前力 != null)
		{
			_Mono_同伴.StopCoroutine(_Cor_动作中向前力);
			_Cor_动作中向前力 = null;
		}
	}

	private IEnumerator Cor_动作中向前力()
	{
		_Mono组件._Rb2D.velocity = Vector2.zero;
		_Mono组件._Ani.Play("飞升", 0, 0f);
		yield return new WaitForSeconds(0.25f);
		Object.Instantiate(_Mono组件._C_Test_血包, _Mono组件._Tsf_血包.position, Quaternion.identity).F_设置回复量(回血招式参数._伤害系数);
		yield return new WaitForSeconds(0.35f);
		_Mono_同伴.ChangeState(EnemyStateName_Type.站立);
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
	}
}
