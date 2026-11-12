using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_蛇司药_攻击 : EnemyState_狼剑客
{
	private EnemyAI_蛇司药 _Mono蛇司药;

	private DataInfo_Enemy招式数值 毒液;

	private DataInfo_Enemy招式数值 毒液其他参数;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		_Mono蛇司药 = (EnemyAI_蛇司药)data._Mono;
		毒液 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5305);
		毒液其他参数 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5302);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk;
		float value = Random.value;
		_ = (Vector2)base._Mono.transform.position;
		if (_Data._攻击范围有敌人)
		{
			if (value < 0.5f)
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_磕头());
			}
			else
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_毒());
			}
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_毒());
		}
	}

	public override void OnExit()
	{
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
			_Cor_攻击 = null;
		}
		base._Rb2D.drag = _Data._StartDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}

	private protected void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? num : (0f - num), num, num);
	}

	public override void OnUpdate()
	{
	}

	private void F_生成一个毒液(Vector2 目标位置)
	{
		C_蛇司药毒瓶 c_蛇司药毒瓶 = Object.Instantiate(_Mono蛇司药._蛇司药毒瓶, _Mono蛇司药._Tsf_手.position, Quaternion.identity);
		Vector2 vector = Random.insideUnitCircle * 1.5f;
		vector.y *= 0.5f;
		c_蛇司药毒瓶.F_Init(目标位置 + vector, base._Mono._Data._Atk * 毒液._伤害系数, 毒液._击退力度, Random.Range(毒液其他参数._伤害系数, 毒液其他参数._击退力度), 2);
	}

	private IEnumerator Cor_磕头()
	{
		for (int i = 0; i < 3; i++)
		{
			F_更新攻击方向();
			base._Ani.Play("攻击_磕头_1");
			float time = Random.Range(0.2f, 0.4f);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
			base._Ani.Play("攻击_磕头_2");
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 20f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.375f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_毒()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_毒");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		Vector2 pivot = base._Mono.transform.position;
		F_生成一个毒液(_目标位置.Rotate(35f, pivot));
		F_生成一个毒液(_目标位置);
		F_生成一个毒液(_目标位置.Rotate(-35f, pivot));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.53f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
