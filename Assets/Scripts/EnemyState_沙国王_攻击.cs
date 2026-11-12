using System.Collections;
using UnityEngine;

public class EnemyState_沙国王_攻击 : EnemyState_沙国王
{
	private Coroutine _Cor_攻击控制;

	private EnemyAI_沙国王 _Mono_沙国王;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		_Mono_沙国王 = (EnemyAI_沙国王)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		base._Ani.Play("攻击");
		base._Rb2D.velocity = Vector2.zero;
		_Timer = _Data._Dict_Anis_name_time["攻击"];
		_Cor_攻击控制 = base._Mono.StartCoroutine(Cor_攻击控制());
	}

	public override void OnExit()
	{
		if (_Cor_攻击控制 != null)
		{
			base._Mono.StopCoroutine(_Cor_攻击控制);
			_Cor_攻击控制 = null;
		}
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		base._Mono.transform.localScale = new Vector3((_Data._Pos_追击目标位置.x > base._Mono.transform.position.x) ? 1 : (-1), 1f, 1f);
		if (_Timer <= 0f)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}

	private IEnumerator Cor_攻击控制()
	{
		float time2;
		if (Random.value < 0.5f)
		{
			time2 = 0f;
			while (time2 < 0.6f)
			{
				if (!base._Mono._Is被定)
				{
					time2 += Time.deltaTime;
				}
				yield return null;
			}
			base._Mono组件.F_发射弹道(_Mono_沙国王._石头Prefab, base._Mono.transform.position, _Data._Pos_追击目标位置, base._Mono._Data._Atk, _Data._Mono);
			time2 = 0f;
			while (time2 < 0.12f)
			{
				if (!base._Mono._Is被定)
				{
					time2 += Time.deltaTime;
				}
				yield return null;
			}
			base._Mono组件.F_发射弹道(_Mono_沙国王._石头Prefab, base._Mono.transform.position, _Data._Pos_追击目标位置, base._Mono._Data._Atk, _Data._Mono);
			yield break;
		}
		time2 = 0f;
		while (time2 < 0.7f)
		{
			if (!base._Mono._Is被定)
			{
				time2 += Time.deltaTime;
			}
			yield return null;
		}
		base._Mono组件.F_发射弹道(_Mono_沙国王._石头Prefab, base._Mono.transform.position, _Data._Pos_追击目标位置, base._Mono._Data._Atk, _Data._Mono);
	}
}
