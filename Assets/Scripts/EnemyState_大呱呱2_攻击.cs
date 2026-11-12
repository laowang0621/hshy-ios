using System;
using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_大呱呱2_攻击 : EnemyState_大呱呱1_攻击
{
	private DataInfo_Enemy招式数值 口吐闪电;

	private DataInfo_Enemy招式数值 追踪闪电;

	private DataInfo_Enemy招式数值 落雷;

	private EnemyAI_大呱呱2 _Mono_大呱呱2;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		口吐闪电 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5101);
		追踪闪电 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5102);
		落雷 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5103);
		_Mono_大呱呱2 = (EnemyAI_大呱呱2)data._Mono;
		return this;
	}

	protected override void F_攻击()
	{
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		_ = (Vector2)base._Mono.transform.position;
		_目标位置 = _Data._Pos_追击目标位置;
		if (UnityEngine.Random.value < 0.4f)
		{
			float value = UnityEngine.Random.value;
			if (value < 0.4f)
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_追踪闪电());
			}
			else if (value < 0.8f)
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_口吐闪电());
			}
			else
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_落雷());
			}
		}
		else
		{
			F_大大呱呱1出招逻辑();
		}
	}

	protected IEnumerator Cor_口吐闪电()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 口吐闪电._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 口吐闪电._击退力度;
		base._Ani.Play("引导_类型_1");
		F_更新攻击方向();
		MyLog.LogError(">>>>Cor_口吐闪电");
		int num = 15;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_呱呱释放电球();
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_呱呱释放电球();
		for (int i = 0; i < num; i++)
		{
			C_弹道_弓箭 c_弹道_弓箭 = UnityEngine.Object.Instantiate(_Mono_大呱呱2._小闪电Prefab, base._Mono组件.transform.position, Quaternion.identity);
			float f = (float)(360 / (num + 1) * i) * (MathF.PI / 180f);
			Vector2 dir = new Vector2(Mathf.Sin(f), Mathf.Cos(f));
			c_弹道_弓箭.F_Init(dir, base._Mono组件._C_攻击判断._伤害);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected IEnumerator Cor_追踪闪电()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 追踪闪电._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 追踪闪电._击退力度;
		base._Ani.Play("引导_类型_2");
		F_更新攻击方向();
		MyLog.LogError(">>>>Cor_追踪闪电");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		int 小闪电数量 = (int)追踪闪电._击退力度;
		for (int i = 0; i < 小闪电数量; i++)
		{
			F_更新攻击方向();
			_目标位置 = _Data._Pos_追击目标位置;
			Vector2 vector = _Mono_大呱呱2._Tsf_嘴巴.position;
			_攻击方向 = (_目标位置 - vector).normalized;
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_呱呱释放电球();
			UnityEngine.Object.Instantiate(_Mono_大呱呱2._小闪电Prefab, _Mono_大呱呱2._Tsf_嘴巴.position, Quaternion.identity).F_Init(_攻击方向, base._Mono组件._C_攻击判断._伤害);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected IEnumerator Cor_落雷()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 落雷._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 落雷._击退力度;
		base._Ani.Play("引导_类型_1");
		F_更新攻击方向();
		MyLog.LogError(">>>>Cor_落雷");
		int 落雷数量 = (int)落雷._击退力度;
		for (int i = 0; i < 落雷数量; i++)
		{
			if ((i & 1) == 1)
			{
				DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_呱呱电流();
			}
			Vector2 vector = (Vector2)base._Mono组件.transform.position + new Vector2(UnityEngine.Random.Range(-18, 18), UnityEngine.Random.Range(-9, 9));
			UnityEngine.Object.Instantiate(_Mono_大呱呱2._落雷Prefab, vector, Quaternion.identity)._伤害 = base._Mono组件._C_攻击判断._伤害;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.06f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
