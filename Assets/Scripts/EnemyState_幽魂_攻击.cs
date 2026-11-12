using System;
using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_幽魂_攻击 : EnemyState_Boss
{
	private DataInfo_Enemy招式数值 双峰贯耳;

	private DataInfo_Enemy招式数值 扫地;

	private DataInfo_Enemy招式数值 磕头;

	private DataInfo_Enemy招式数值 震荡波_一段;

	private DataInfo_Enemy招式数值 震荡波_四连;

	private DataInfo_Enemy招式数值 龟派气功_释放;

	private Coroutine _Cor_攻击;

	private int _连击次数;

	private int _Max连击次数;

	private Vector2Int _Ran连击次数 = new Vector2Int(3, 6);

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		双峰贯耳 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(1001);
		扫地 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(1002);
		磕头 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(1003);
		震荡波_一段 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(1004);
		震荡波_四连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(1005);
		龟派气功_释放 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(1006);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		F_攻击();
	}

	public override void OnExit()
	{
		if (_Cor_攻击 != null)
		{
			base._Mono.StopCoroutine(_Cor_攻击);
			_Cor_攻击 = null;
		}
	}

	public override void OnUpdate()
	{
	}

	private void F_攻击()
	{
		if (UnityEngine.Random.value < 0.15f)
		{
			_Cor_攻击 = base._Mono.StartCoroutine(Cor_震荡波四连());
			return;
		}
		_连击次数 = 0;
		_Max连击次数 = UnityEngine.Random.Range(_Ran连击次数.x, _Ran连击次数.y);
		F_连击();
	}

	private void F_连击()
	{
		_连击次数++;
		bool 有后续 = _连击次数 < _Max连击次数;
		base._Ani.Play("New State");
		_Cor_攻击 = UnityEngine.Random.Range(0, 5) switch
		{
			0 => base._Mono.StartCoroutine(Cor_龟派气功(有后续)), 
			1 => base._Mono.StartCoroutine(Cor_双峰贯耳(有后续)), 
			2 => base._Mono.StartCoroutine(Cor_扫地(有后续)), 
			3 => base._Mono.StartCoroutine(Cor_磕头(有后续)), 
			4 => base._Mono.StartCoroutine(Cor_震荡波_一段(有后续)), 
			_ => throw new Exception("为什么回报错？"), 
		};
	}

	private void F_判断是否有后续(bool 有后续)
	{
		if (有后续)
		{
			F_连击();
		}
		else
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}

	private void F_攻击朝向和冲力(float 冲力)
	{
		Vector2 vector = base._Mono.transform.position;
		Vector2 vector2 = _Data._Pos_追击目标位置;
		bool flag = vector2.x > vector.x;
		base._Rb2D.velocity = Vector2.zero;
		base._Rb2D.velocity = 4f * 冲力 * (vector2 - vector).normalized;
		base._Mono.transform.localScale = new Vector3(flag ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_震荡波四连()
	{
		yield return null;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 震荡波_四连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 震荡波_四连._击退力度;
		_Timer = _Data._Dict_Anis_name_time["震荡波_四连"];
		base._Ani.Play("震荡波_四连");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_Timer));
		if (UnityEngine.Random.value > 0.5f)
		{
			_Cor_攻击 = base._Mono.StartCoroutine(Cor_龟派气功(有后续: false));
		}
		else
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}

	private IEnumerator Cor_龟派气功(bool 有后续)
	{
		yield return null;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 龟派气功_释放._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 龟派气功_释放._击退力度;
		F_攻击朝向和冲力(0f);
		_Timer = UnityEngine.Random.Range(0.6f, 1.5f);
		base._Ani.Play("龟派气功_蓄力");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_Timer));
		_Timer = UnityEngine.Random.Range(0.8f, 1.2f);
		base._Ani.Play("龟派气功_释放");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_Timer));
		F_判断是否有后续(有后续);
	}

	private IEnumerator Cor_双峰贯耳(bool 有后续)
	{
		yield return null;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 双峰贯耳._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 双峰贯耳._击退力度;
		F_攻击朝向和冲力(10f);
		_Timer = _Data._Dict_Anis_name_time["双峰贯耳"];
		base._Ani.Play("双峰贯耳");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_Timer));
		F_判断是否有后续(有后续);
	}

	private IEnumerator Cor_扫地(bool 有后续)
	{
		yield return null;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 扫地._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 扫地._击退力度;
		F_攻击朝向和冲力(10f);
		_Timer = _Data._Dict_Anis_name_time["扫地"];
		base._Ani.Play("扫地");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_Timer));
		F_判断是否有后续(有后续);
	}

	private IEnumerator Cor_磕头(bool 有后续)
	{
		yield return null;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 磕头._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 磕头._击退力度;
		F_攻击朝向和冲力(10f);
		_Timer = _Data._Dict_Anis_name_time["磕头"];
		base._Ani.Play("磕头");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_Timer));
		F_判断是否有后续(有后续);
	}

	private IEnumerator Cor_震荡波_一段(bool 有后续)
	{
		yield return null;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 震荡波_一段._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 震荡波_一段._击退力度;
		F_攻击朝向和冲力(0f);
		_Timer = _Data._Dict_Anis_name_time["震荡波_一段"];
		base._Ani.Play("震荡波_一段");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_Timer));
		F_判断是否有后续(有后续);
	}
}
