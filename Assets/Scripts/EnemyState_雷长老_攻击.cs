using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_雷长老_攻击 : EnemyState_狼弓手
{
	private EnemyAI_雷长老 _Mono_雷长老;

	private Coroutine _Cor_攻击控制;

	protected Vector2 _目标位置;

	protected Vector2 _攻击方向;

	public override EnemyState_狼弓手 Init(Enemy_StateData_狼弓手 data)
	{
		base.Init(data);
		_Mono_雷长老 = base._Mono as EnemyAI_雷长老;
		return this;
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
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
	}

	private protected void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? num : (0f - num), num, num);
	}

	private IEnumerator Cor_攻击控制()
	{
		if (Random.value < 0.5f)
		{
			base._Ani.Play("攻击_1");
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
			for (int j = 0; j < 3; j++)
			{
				F_更新攻击方向();
				_目标位置 = _Data._Pos_追击目标位置;
				Vector2 vector = _Mono_雷长老._Tsf_法球.position;
				_攻击方向 = (_目标位置 - vector).normalized;
				DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_呱呱释放电球();
				Object.Instantiate(_Mono_雷长老._雷电球Prefab, vector, Quaternion.identity).F_Init(_攻击方向, base._Mono组件._C_攻击判断._伤害);
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
			}
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		}
		else
		{
			base._Ani.Play("攻击_2");
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
			for (int j = 0; j < 4; j++)
			{
				F_更新攻击方向();
				DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_呱呱电流();
				Object.Instantiate(_Mono_雷长老._落雷Prefab, _目标位置, Quaternion.identity)._伤害 = base._Mono组件._C_攻击判断._伤害;
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
			}
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
