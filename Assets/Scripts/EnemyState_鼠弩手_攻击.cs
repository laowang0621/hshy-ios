using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_鼠弩手_攻击 : EnemyState_狼弓手
{
	private Coroutine _Cor_攻击控制;

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
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((_Data._Pos_追击目标位置.x > base._Mono.transform.position.x) ? num : (0f - num), num, num);
		if (_Timer <= 0f)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}

	private IEnumerator Cor_攻击控制()
	{
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.85f));
		F_发射弓箭();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_发射弓箭();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_发射弓箭();
	}

	private void F_发射弓箭()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		Object.Instantiate(base._Mono组件._弓箭Prefab, base._Mono.transform.position, Quaternion.identity).F_Init((_Data._Pos_追击目标位置 - base._Mono.transform.position).normalized, base._Mono._Data._Atk);
	}
}
