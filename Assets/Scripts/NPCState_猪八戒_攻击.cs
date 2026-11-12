using System.Collections;
using UnityEngine;

public class NPCState_猪八戒_攻击 : NPCState_同伴
{
	private Coroutine _Cor_动作中向前力;

	private Vector2 dir;

	private DataInfo_Player招式 横扫钉耙;

	private DataInfo_Player招式 拍头钉耙;

	public override NPCState_同伴 Init(NPCAI_同伴 mono)
	{
		base.Init(mono);
		横扫钉耙 = MyDataManager.F_Get_Player_招式系数(2011);
		拍头钉耙 = MyDataManager.F_Get_Player_招式系数(2012);
		return this;
	}

	public override void OnEnter()
	{
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

	private bool F_攻击方向(string aniName)
	{
		if (!_Mono_同伴._攻击范围有敌人)
		{
			_Mono_同伴.ChangeState(EnemyStateName_Type.站立);
			return false;
		}
		Vector2 vector = _Mono_同伴.transform.position;
		Vector2 vector2 = _Mono_同伴._Pos_追击目标位置;
		dir = (vector2 - vector).normalized;
		_Mono_同伴._朝向右边 = dir.x > 0f;
		_Mono_同伴.transform.localScale = new Vector3(_Mono_同伴._朝向右边 ? 1 : (-1), 1f, 1f);
		_Mono组件._Ani.Play(aniName, 0, 0f);
		return true;
	}

	private IEnumerator Cor_动作中向前力()
	{
		_Mono_同伴._Mono组件._Rb2D.velocity = Vector2.zero;
		if (!F_攻击方向("横扫钉耙"))
		{
			yield break;
		}
		_Mono_同伴._Mono组件._C_攻击判断._伤害 = _Mono_同伴._攻击力 * 横扫钉耙._伤害系数;
		_Mono_同伴._Mono组件._C_攻击判断._僵直伤害 = 横扫钉耙._僵直伤害;
		yield return new WaitForSeconds(0.15f);
		_Mono_同伴._Mono组件._Rb2D.velocity = dir * 15f;
		yield return new WaitForSeconds(0.5f);
		if (Random.value < 0.8f)
		{
			if (!F_攻击方向("横扫钉耙"))
			{
				yield break;
			}
			yield return new WaitForSeconds(0.15f);
			_Mono_同伴._Mono组件._Rb2D.velocity = dir * 15f;
			yield return new WaitForSeconds(0.5f);
		}
		if (F_攻击方向("拍头钉耙"))
		{
			_Mono_同伴._Mono组件._C_攻击判断._伤害 = _Mono_同伴._攻击力 * 拍头钉耙._伤害系数;
			_Mono_同伴._Mono组件._C_攻击判断._僵直伤害 = 拍头钉耙._僵直伤害;
			yield return new WaitForSeconds(0.2f);
			_Mono_同伴._Mono组件._Rb2D.velocity = dir * 15f;
			yield return new WaitForSeconds(0.55f);
			_Mono_同伴.ChangeState(EnemyStateName_Type.站立);
		}
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
	}
}
