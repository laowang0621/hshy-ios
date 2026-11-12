using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_大呱呱4_攻击 : EnemyState_大呱呱1_攻击
{
	private DataInfo_Enemy招式数值 毒液;

	private DataInfo_Enemy招式数值 毒液其他参数;

	private DataInfo_Enemy招式数值 毒液其他参数2;

	private DataInfo_Enemy招式数值 激光;

	private EnemyAI_大呱呱4 _Mono_大呱呱4;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		毒液 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5301);
		毒液其他参数 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5302);
		激光 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5303);
		毒液其他参数2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5304);
		_Mono_大呱呱4 = (EnemyAI_大呱呱4)data._Mono;
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
		if (Random.value < 0.5f)
		{
			if (Random.value < 0.4f)
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_毒液());
			}
			else
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_激光());
			}
		}
		else
		{
			F_大大呱呱1出招逻辑();
		}
	}

	private void F_生成一个毒液()
	{
		Vector2 vector = Random.insideUnitCircle * 6f;
		vector.y *= 0.5f;
		Object.Instantiate(_Mono_大呱呱4._蛇司药毒瓶, _目标位置 + new Vector2(0f, 10f) + vector, Quaternion.identity).F_Init(_目标位置 + vector, base._Mono._Data._Atk * 毒液._伤害系数, 毒液._击退力度, Random.Range(毒液其他参数._伤害系数, 毒液其他参数._击退力度), 2);
	}

	protected IEnumerator Cor_毒液()
	{
		base._Ani.Play("引导_类型_1");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		int 毒液数量 = 1 + Mathf.CeilToInt(Random.Range(毒液其他参数2._伤害系数, 毒液其他参数2._击退力度));
		for (int i = 0; i < 毒液数量; i++)
		{
			F_生成一个毒液();
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected IEnumerator Cor_激光()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 激光._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 激光._击退力度;
		base._Ani.Play("引导_类型_2");
		F_更新攻击方向();
		int 激光数量 = (int)激光._击退力度;
		for (int i = 0; i < 激光数量; i++)
		{
			_目标位置 = _Data._Pos_追击目标位置;
			Vector2 vector = _Mono_大呱呱4._Tsf_嘴巴.position;
			_攻击方向 = (_目标位置 - vector).normalized;
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_呱呱释放电球();
			Object.Instantiate(_Mono_大呱呱4._激光_Prefab, _Mono_大呱呱4._Tsf_嘴巴.position, Quaternion.identity).F_Init(_攻击方向, base._Mono组件._C_攻击判断._伤害);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
