using System.Collections;
using UnityEngine;

public class EnemyState_猴子猴孙_攻击 : EnemyState_猴子猴孙
{
	private Coroutine _Cor_动作中向前力;

	private Vector2 dir;

	public override void OnEnter()
	{
		if (_Cor_动作中向前力 != null)
		{
			base._Mono.StopCoroutine(_Cor_动作中向前力);
		}
		_Cor_动作中向前力 = base._Mono.StartCoroutine(Cor_动作中向前力());
	}

	public override void OnExit()
	{
		if (_Cor_动作中向前力 != null)
		{
			base._Mono.StopCoroutine(_Cor_动作中向前力);
			_Cor_动作中向前力 = null;
		}
	}

	private bool F_攻击方向(string aniName)
	{
		if (!_Data._攻击范围有敌人)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
			return false;
		}
		Vector2 vector = base._Mono.transform.position;
		Vector2 vector2 = _Data._Pos_追击目标位置;
		_Data._朝向右边 = vector2.x > vector.x;
		dir = (vector2 - vector).normalized;
		base._Ani.Play(aniName);
		_Timer = _Data._Dict_Anis_name_time[aniName];
		return true;
	}

	private IEnumerator Cor_动作中向前力()
	{
		base._Rb2D.velocity = Vector2.zero;
		if (!F_攻击方向("轻攻击1"))
		{
			yield break;
		}
		float time3 = 0f;
		while (time3 < 0.11f)
		{
			if (!base._Mono._Is被定)
			{
				time3 += Time.deltaTime;
			}
			yield return null;
		}
		base._Rb2D.velocity = dir * 6f;
		float size = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3(_Data._朝向右边 ? size : (0f - size), size, size);
		while (_Timer > 0f)
		{
			yield return null;
		}
		if (!F_攻击方向("轻攻击2"))
		{
			yield break;
		}
		time3 = 0f;
		while (time3 < 0.11f)
		{
			if (!base._Mono._Is被定)
			{
				time3 += Time.deltaTime;
			}
			yield return null;
		}
		base._Rb2D.velocity = dir * 6f;
		base._Mono.transform.localScale = new Vector3(_Data._朝向右边 ? size : (0f - size), size, size);
		while (_Timer > 0f)
		{
			yield return null;
		}
		if (!F_攻击方向("轻攻击3"))
		{
			yield break;
		}
		time3 = 0f;
		while (time3 < 0.11f)
		{
			if (!base._Mono._Is被定)
			{
				time3 += Time.deltaTime;
			}
			yield return null;
		}
		base._Rb2D.velocity = dir * 6f;
		base._Mono.transform.localScale = new Vector3(_Data._朝向右边 ? size : (0f - size), size, size);
		while (_Timer > 0f)
		{
			yield return null;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
	}
}
