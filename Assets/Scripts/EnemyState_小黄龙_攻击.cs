using System.Collections;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class EnemyState_小黄龙_攻击 : EnemyState_Boss
{
	private EnemyAI_小黄龙 _Mono_小黄龙;

	private DataInfo_Enemy招式数值 单手突刺;

	private DataInfo_Enemy招式数值 步进二连;

	private DataInfo_Enemy招式数值 转身后摆;

	private DataInfo_Enemy招式数值 横扫二连;

	private DataInfo_Enemy招式数值 反身低扫;

	private DataInfo_Enemy招式数值 反手横扫;

	private DataInfo_Enemy招式数值 上挑;

	private DataInfo_Enemy招式数值 地面龙车;

	private DataInfo_Enemy招式数值 单手旋风斩;

	private DataInfo_Enemy招式数值 双手旋风斩;

	private DataInfo_Enemy招式数值 插刀磁暴;

	private DataInfo_Enemy招式数值 空中龙车;

	private DataInfo_Enemy招式数值 黄龙钻;

	private DataInfo_Enemy招式数值 丙击;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private float _EnterDrag;

	private int _攻击序号 = 10;

	private List<int> _出招序号表 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

	private int _x次攻击内不会重复出升空;

	private float 剩余空中冲刺时间;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_小黄龙 = (EnemyAI_小黄龙)data._Mono;
		单手突刺 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14001);
		步进二连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14002);
		转身后摆 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14003);
		横扫二连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14004);
		反身低扫 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14005);
		反手横扫 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14006);
		上挑 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14007);
		地面龙车 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14008);
		单手旋风斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14009);
		双手旋风斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14010);
		插刀磁暴 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14011);
		空中龙车 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14012);
		黄龙钻 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14013);
		丙击 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(14014);
		_x次攻击内不会重复出升空 = 3;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		_Data._Mono组件.StopAllCoroutines();
		base._Mono组件.StartCoroutine(Cor_攻击());
	}

	public override void OnExit()
	{
		_Data._Mono组件.StopAllCoroutines();
		_Mono_小黄龙._冲刺中 = false;
		_Mono_小黄龙._空中 = false;
		base._Rb2D.drag = _EnterDrag;
		_Mono_小黄龙._TR_拖尾_1.emitting = false;
		_Mono_小黄龙._TR_拖尾_2.emitting = false;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	private IEnumerator Cor_攻击()
	{
		_攻击序号 = _出招序号表.GetRandom();
		if (_Mono_小黄龙._开启固定出招)
		{
			_攻击序号 = _Mono_小黄龙._固定出招序号;
		}
		if (_攻击序号 == 9)
		{
			if (_x次攻击内不会重复出升空 >= 0)
			{
				_攻击序号 = 10;
			}
			else
			{
				_x次攻击内不会重复出升空 = 3;
			}
		}
		_x次攻击内不会重复出升空--;
		switch (_攻击序号)
		{
		case 1:
			yield return base._Mono组件.StartCoroutine(Cor_单手突刺_步进二连_转身后摆());
			break;
		case 2:
			yield return base._Mono组件.StartCoroutine(Cor_横扫二连_反身低扫());
			break;
		case 3:
			yield return base._Mono组件.StartCoroutine(Cor_正手_反手横扫_上挑());
			break;
		case 4:
			yield return base._Mono组件.StartCoroutine(Cor_地面龙车());
			yield return base._Mono组件.StartCoroutine(Cor_地面龙车_接_反手横扫());
			break;
		case 5:
			yield return base._Mono组件.StartCoroutine(Cor_单手旋风斩_二连());
			break;
		case 6:
			yield return base._Mono组件.StartCoroutine(Cor_地面龙车());
			yield return base._Mono组件.StartCoroutine(Cor_地面龙车_接_双手旋风斩());
			break;
		case 7:
			yield return base._Mono组件.StartCoroutine(Cor_地面龙车());
			yield return base._Mono组件.StartCoroutine(Cor_地面龙车_接_双手旋风斩());
			yield return base._Mono组件.StartCoroutine(Cor_单手旋风斩_二连());
			break;
		case 8:
			yield return base._Mono组件.StartCoroutine(Cor_丙击());
			break;
		case 9:
			_Mono_小黄龙._冲刺中 = true;
			Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
			Physics2D.IgnoreLayerCollision(6, 10, ignore: true);
			yield return base._Mono组件.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_1_起手());
			yield return base._Mono组件.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_2_冲刺后升空(剩余空中冲刺时间));
			yield return base._Mono组件.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_3_空中冲刺());
			yield return base._Mono组件.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_2_冲刺后升空(剩余空中冲刺时间));
			yield return base._Mono组件.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_3_空中冲刺());
			yield return base._Mono组件.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_2_冲刺后升空(剩余空中冲刺时间));
			yield return base._Mono组件.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_3_空中冲刺());
			yield return base._Mono组件.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_2_冲刺后升空(剩余空中冲刺时间));
			Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
			Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
			_Mono_小黄龙._冲刺中 = false;
			yield return base._Mono组件.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_4_结束拍地());
			break;
		case 10:
			yield return base._Mono组件.StartCoroutine(Cor_地面龙车());
			yield return base._Mono组件.StartCoroutine(Cor_地面龙车_接_反手横扫());
			yield return base._Mono组件.StartCoroutine(Cor_黄龙钻_起手());
			yield return base._Mono组件.StartCoroutine(Cor_地面龙车_接_双手旋风斩());
			yield return base._Mono组件.StartCoroutine(Cor_单手旋风斩_二连());
			break;
		case 11:
			yield return base._Mono组件.StartCoroutine(Cor_黄龙钻_起手());
			break;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_单手突刺_步进二连_转身后摆()
	{
		base._Ani.Play("攻击_单手突刺_步进二连_转身后摆", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 单手突刺._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 单手突刺._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 步进二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 步进二连._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 21f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 转身后摆._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 转身后摆._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.05f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 38f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
	}

	private IEnumerator Cor_横扫二连_反身低扫()
	{
		base._Ani.Play("攻击_横扫二连_反身低扫", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 横扫二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 横扫二连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 22f;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 反身低扫._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 反身低扫._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.58f));
	}

	private IEnumerator Cor_正手_反手横扫_上挑()
	{
		base._Ani.Play("攻击_正手_反手横扫_上挑", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 反手横扫._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 反手横扫._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 24f;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 上挑._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 上挑._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.72f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
	}

	private IEnumerator Cor_单手旋风斩_二连()
	{
		base._Ani.Play("攻击_单手旋风斩_二连", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 单手旋风斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 单手旋风斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		float 起始速度2 = 28f;
		Vector2 dir_2 = _攻击方向;
		for (int j = 0; j < 5; j++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_2, _攻击方向, 0.1f) * (起始速度2 - (float)(j * 3));
			dir_2 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.08f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.325f));
		起始速度2 = 28f;
		dir_2 = _攻击方向;
		for (int j = 0; j < 5; j++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_2, _攻击方向, 0.1f) * (起始速度2 - (float)(j * 3));
			dir_2 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.08f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.325f));
	}

	private IEnumerator Cor_地面龙车()
	{
		base._Ani.Play("攻击_地面龙车", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 地面龙车._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 地面龙车._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.83f));
		F_更新攻击方向();
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		base._Rb2D.drag = 0f;
		float num = Vector2.Distance(base._Mono组件.transform.position, _目标位置 + _攻击方向 * 2f);
		float num2 = 65f;
		base._Rb2D.velocity = _攻击方向 * num2;
		float time = num / num2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		base._Rb2D.velocity = _攻击方向 * 12f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
	}

	private IEnumerator Cor_地面龙车_接_反手横扫()
	{
		base._Ani.Play("攻击_地面龙车_接_反手横扫", 0, 0f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	private IEnumerator Cor_地面龙车_接_双手旋风斩()
	{
		base._Ani.Play("攻击_地面龙车_接_双手旋风斩", 0, 0f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		float 起始速度 = 28f;
		Vector2 dir_1 = _攻击方向;
		for (int i = 0; i < 6; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.1f) * (起始速度 - (float)i * 2.75f);
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.12f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	private IEnumerator Cor_丙击()
	{
		base._Ani.Play("攻击_柄击", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 丙击._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 丙击._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
	}

	private IEnumerator Cor_插刀磁暴_空中龙车_四连_1_起手()
	{
		base._Ani.Play("攻击_插刀磁暴_空中龙车_四连_1_起手", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 插刀磁暴._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 插刀磁暴._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.5f));
		F_更新攻击方向();
		base._Rb2D.drag = 0f;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 空中龙车._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 空中龙车._击退力度;
		float num = Vector2.Distance(base._Mono组件.transform.position, _目标位置 + _攻击方向 * 5.5f);
		float num2 = 45f;
		base._Rb2D.velocity = _攻击方向 * num2;
		float num3 = num / num2;
		剩余空中冲刺时间 = num3 / 6f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(剩余空中冲刺时间 * 3f));
	}

	private IEnumerator Cor_插刀磁暴_空中龙车_四连_3_空中冲刺()
	{
		base._Ani.Play("攻击_插刀磁暴_空中龙车_四连_3_空中冲刺", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 空中龙车._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 空中龙车._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		_Mono_小黄龙._空中 = false;
		base._Rb2D.drag = 0f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		float num = Vector2.Distance(base._Mono组件.transform.position, _目标位置 + _攻击方向 * 5.5f);
		float num2 = 65f;
		base._Rb2D.drag = 0f;
		base._Rb2D.velocity = _攻击方向 * num2;
		float num3 = num / num2;
		剩余空中冲刺时间 = num3 / 6f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(剩余空中冲刺时间 * 3f));
	}

	private IEnumerator Cor_插刀磁暴_空中龙车_四连_2_冲刺后升空(float 冲刺剩余时间)
	{
		base._Rb2D.drag = 3f;
		base._Ani.Play("攻击_插刀磁暴_空中龙车_四连_2_冲刺后升空", 0, 0f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(冲刺剩余时间 * 2f));
		_Mono_小黄龙._空中 = true;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
	}

	private IEnumerator Cor_插刀磁暴_空中龙车_四连_4_结束拍地()
	{
		base._Ani.Play("攻击_插刀磁暴_空中龙车_四连_4_结束拍地", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 空中龙车._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 空中龙车._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
		F_更新攻击方向();
		base._Rb2D.drag = 0f;
		float num = Vector2.Distance(base._Mono组件.transform.position, _目标位置 + _攻击方向 * -1f);
		float num2 = 48f;
		base._Rb2D.velocity = _攻击方向 * num2;
		float time = num / num2;
		_Mono_小黄龙._空中 = false;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
		base._Rb2D.drag = _EnterDrag;
		base._Rb2D.velocity = _攻击方向 * 9f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
	}

	private IEnumerator Cor_黄龙钻_起手()
	{
		base._Ani.Play("攻击_黄龙钻", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 黄龙钻._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 黄龙钻._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		_Mono_小黄龙._TR_拖尾_1.emitting = true;
		_Mono_小黄龙._TR_拖尾_2.emitting = true;
		F_更新攻击方向();
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: true);
		base._Rb2D.drag = 0f;
		float num = Vector2.Distance(base._Mono组件.transform.position, _目标位置 + _攻击方向 * 5f);
		float num2 = 35f;
		base._Rb2D.velocity = _攻击方向 * num2;
		float time = num / num2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
		base._Rb2D.velocity = _攻击方向 * 15f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		_Mono_小黄龙._TR_拖尾_1.emitting = false;
		_Mono_小黄龙._TR_拖尾_2.emitting = false;
	}
}
