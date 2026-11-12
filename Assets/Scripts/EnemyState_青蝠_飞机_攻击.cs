using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_青蝠_飞机_攻击 : EnemyState_狼剑客
{
	private Coroutine _Cor_攻击控制;

	public override void OnEnter()
	{
		base._Ani.Play("远程攻击");
		base._Rb2D.velocity = Vector2.zero;
		_Timer = _Data._Dict_Anis_name_time["远程攻击"];
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
		base._Mono.transform.localScale = new Vector3((DSB_MonoSingleton<GameManager>.Instance._PC.transform.position.x > base._Mono.transform.position.x) ? 1 : (-1), 1f, 1f);
		if (_Timer <= 0f)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}

	private IEnumerator Cor_攻击控制()
	{
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		F_发射弓箭();
	}

	private void F_发射弓箭()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_投掷();
		Object.Instantiate(base._Mono组件._弓箭Prefab, base._Mono.transform.position, Quaternion.identity).F_Init((DSB_MonoSingleton<GameManager>.Instance._PC.transform.position - base._Mono.transform.position).normalized, base._Mono._Data._Atk);
	}
}
