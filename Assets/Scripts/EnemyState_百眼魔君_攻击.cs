using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyState_百眼魔君_攻击 : EnemyState_Boss
{
	private EnemyAI_百眼魔君 _Mono_百眼魔君;

	private DataInfo_Enemy招式数值 大落雷;

	private DataInfo_Enemy招式数值 雷球;

	private DataInfo_Enemy招式数值 二连;

	private DataInfo_Enemy招式数值 下劈;

	private DataInfo_Enemy招式数值 上挑;

	private DataInfo_Enemy招式数值 下踩;

	private DataInfo_Enemy招式数值 回身斩;

	private DataInfo_Enemy招式数值 雷劈;

	private DataInfo_Enemy招式数值 符纸二连;

	private DataInfo_Enemy招式数值 蓄力上挑;

	private DataInfo_Enemy招式数值 引雷三连;

	private DataInfo_Enemy招式数值 引雷三连_剑气;

	private DataInfo_Enemy招式数值 插地剑;

	private DataInfo_Enemy招式数值 掀地;

	private DataInfo_Enemy招式数值 电光炮;

	private DataInfo_Enemy招式数值 砸地;

	private DataInfo_Enemy招式数值 眼爆;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private float _EnterDrag;

	private bool _跟随移动;

	private Vector2 _MoveDirSpeed;

	private bool _是2阶段;

	private float _血量百分比_2 = 0.5f;

	private bool _是2阶段第一次攻击;

	private int _攻击序号 = 10;

	private List<int> _出招序号表 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

	private List<int> _出招序号表_2 = new List<int> { 101, 102, 103, 104 };

	private List<int> _出招序号表_3 = new List<int> { 101, 102, 103 };

	private int _x次攻击内不会重复出升空 = -1;

	private List<EnemyAI_百眼魔君_眼球> _List_眼球;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_百眼魔君 = (EnemyAI_百眼魔君)data._Mono;
		大落雷 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7005);
		雷球 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7003);
		二连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17001);
		下劈 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17002);
		上挑 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17003);
		下踩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17004);
		回身斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17005);
		雷劈 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17006);
		符纸二连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17007);
		蓄力上挑 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17008);
		引雷三连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17009);
		引雷三连_剑气 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17010);
		插地剑 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17011);
		掀地 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17012);
		电光炮 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17013);
		砸地 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17014);
		眼爆 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(17015);
		_x次攻击内不会重复出升空 = -1;
		return base.Init(data);
	}

	public override void OnUpdate()
	{
		if (_跟随移动)
		{
			base._Rb2D.velocity = Vector2.Lerp(base._Rb2D.velocity, _MoveDirSpeed, Time.deltaTime * 10.9f);
		}
	}

	public override void OnEnter()
	{
		_跟随移动 = false;
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		_Data._Mono组件.StopAllCoroutines();
		base._Mono组件.StartCoroutine(Cor_攻击());
	}

	public override void OnExit()
	{
		_Data._Mono组件.StopAllCoroutines();
		_跟随移动 = false;
		F_清空眼球();
		base._Mono组件._SR_Model.SetAlpha(1f);
		base._Mono组件._SR_影子.SetAlpha(1f);
		_Mono_百眼魔君._SR_尾巴.SetAlpha(1f);
		base._Rb2D.bodyType = RigidbodyType2D.Dynamic;
		_Mono_百眼魔君._Co2D_尾巴.enabled = true;
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
		base._Rb2D.drag = _EnterDrag;
		foreach (EnemyAI_百眼魔君_符纸 item in _Mono_百眼魔君._List_符纸)
		{
			item.F_归位();
		}
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
	}

	private void F_发射弓箭(C_弹道 prefab)
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		float num = Mathf.Abs(_攻击方向.x);
		float num2 = Mathf.Abs(_攻击方向.y);
		Vector2 dir = ((num2 < 0.33f * num) ? _攻击方向 : new Vector2(_攻击方向.x, _攻击方向.y / num2 * 0.33f * num).normalized);
		Object.Instantiate(prefab, base._Mono.transform.position, Quaternion.identity).F_Init(dir, 引雷三连_剑气._伤害系数 * _Data._Atk);
	}

	private void F_生成大落雷(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_百眼魔君._C_大落雷_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 插地剑._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 插地剑._击退力度;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_大落雷();
	}

	private void F_生成大落雷_眼爆(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_百眼魔君._C_大落雷_Prefab, pos, Quaternion.identity);
		c_攻击判断.GetComponent<Animator>().Play("百眼魔君_大落雷_眼爆", 0, 0f);
		c_攻击判断._伤害 = 眼爆._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 眼爆._击退力度;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_大落雷();
	}

	private void F_生成雷球(Vector2 pos, Vector2 dir)
	{
		C_弹道 c_弹道 = Object.Instantiate(_Mono_百眼魔君._C_弹道_雷球_Prefab, pos, Quaternion.identity);
		c_弹道._伤害 = 电光炮._伤害系数 * _Data._Atk;
		c_弹道._击退力度 = 电光炮._击退力度;
		c_弹道.F_Init(dir);
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小电流();
	}

	private IEnumerator Cor_攻击()
	{
		if (!_是2阶段 && _Data._Curr生命 < _Data._MaxHP * _血量百分比_2)
		{
			_Mono_百眼魔君.F_触发吞剑();
			_是2阶段 = true;
			base._Ani.Play("攻击_吞剑", 0, 0f);
			base._Rb2D.velocity = Vector2.zero;
			F_更新攻击方向();
			_是2阶段第一次攻击 = true;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.5f));
			DSB_MonoSingleton<GameManager>.Instance._PC.F_临时_设置气力上限百分比(0.4f);
			base._Mono.ChangeState(EnemyStateName_Type.站立);
			yield break;
		}
		if (_是2阶段)
		{
			_攻击序号 = _出招序号表_2.GetRandom();
			if (_是2阶段第一次攻击)
			{
				_是2阶段第一次攻击 = false;
				_攻击序号 = 104;
			}
		}
		else
		{
			_攻击序号 = _出招序号表.GetRandom();
		}
		if (_攻击序号 == 104)
		{
			if (_x次攻击内不会重复出升空 >= 0)
			{
				_攻击序号 = _出招序号表_3.GetRandom();
			}
			else
			{
				_x次攻击内不会重复出升空 = 3;
			}
		}
		_x次攻击内不会重复出升空--;
		if (_Mono_百眼魔君._开启固定出招)
		{
			_攻击序号 = _Mono_百眼魔君._固定出招序号;
		}
		switch (_攻击序号)
		{
		case 1:
			yield return base._Mono组件.StartCoroutine(Cor_二连接下劈());
			break;
		case 2:
			yield return base._Mono组件.StartCoroutine(Cor_二连接下劈接上挑接下踩());
			break;
		case 3:
			yield return base._Mono组件.StartCoroutine(Cor_冲刺三连接下劈());
			break;
		case 4:
			yield return base._Mono组件.StartCoroutine(Cor_下踩接二连接回身斩());
			break;
		case 5:
			yield return base._Mono组件.StartCoroutine(Cor_下踩接右劈接雷劈接抓());
			break;
		case 6:
			yield return base._Mono组件.StartCoroutine(Cor_符纸二连接回身斩接双手蓄力上挑());
			break;
		case 7:
			yield return base._Mono组件.StartCoroutine(Cor_引雷三连());
			break;
		case 8:
			yield return base._Mono组件.StartCoroutine(Cor_插地剑());
			break;
		case 101:
			yield return base._Mono组件.StartCoroutine(Cor_空手掀地板());
			break;
		case 102:
			yield return base._Mono组件.StartCoroutine(Cor_空手砸地板());
			break;
		case 103:
			yield return base._Mono组件.StartCoroutine(Cor_电光炮());
			break;
		case 104:
			yield return base._Mono组件.StartCoroutine(Cor_眼爆());
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

	private IEnumerator Cor_二连接下劈()
	{
		base._Ani.Play("攻击_二连接下劈", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.63f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下劈._击退力度;
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.72f));
	}

	private IEnumerator Cor_二连接下劈接上挑接下踩()
	{
		base._Ani.Play("攻击_二连接下劈接上挑接下踩", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.63f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下劈._击退力度;
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.72f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 上挑._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 上挑._击退力度;
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下踩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下踩._击退力度;
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.04f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.77f));
	}

	private IEnumerator Cor_冲刺三连接下劈()
	{
		base._Ani.Play("攻击_冲刺三连接下劈", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.84f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下劈._击退力度;
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.58f));
	}

	private IEnumerator Cor_下踩接二连接回身斩()
	{
		base._Ani.Play("攻击_下踩接二连接回身斩", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下踩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下踩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 32f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.92f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连._击退力度;
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.67f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.99f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 回身斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 回身斩._击退力度;
		base._Rb2D.velocity = _攻击方向 * 36f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.59f));
	}

	private IEnumerator Cor_下踩接右劈接雷劈接抓()
	{
		base._Ani.Play("攻击_下踩接右劈接雷劈接抓", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下踩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下踩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 32f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.83f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连._击退力度;
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.84f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 雷劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 雷劈._击退力度;
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 符纸二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 符纸二连._击退力度;
		base._Rb2D.velocity = _攻击方向 * 36f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.58f));
	}

	private IEnumerator Cor_符纸二连接回身斩接双手蓄力上挑()
	{
		base._Ani.Play("攻击_符纸二连接回身斩接双手蓄力上挑", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 符纸二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 符纸二连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 32f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.92f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 回身斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 回身斩._击退力度;
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.08f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 蓄力上挑._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 蓄力上挑._击退力度;
		base._Rb2D.velocity = _攻击方向 * 36f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.92f));
	}

	private IEnumerator Cor_引雷三连()
	{
		base._Ani.Play("攻击_引雷三连", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 引雷三连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 引雷三连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.13f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 32f;
		F_发射弓箭(_Data._Mono组件._C_弹道_剑气Prefab);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		F_发射弓箭(_Data._Mono组件._C_弹道_剑气Prefab);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		F_发射弓箭(_Data._Mono组件._C_弹道_剑气Prefab);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.53f));
	}

	private IEnumerator Cor_插地剑()
	{
		base._Ani.Play("攻击_插地剑", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 插地剑._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 插地剑._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.36f));
		F_更新攻击方向();
		Vector2 pos_自己 = _Mono_百眼魔君._Tsf_插地剑.position;
		Vector2 vector = _Data._Pos_追击目标位置;
		Vector2 dir = (vector - pos_自己).normalized;
		float num = Vector2.Distance(pos_自己, vector);
		int count;
		float time_间隔;
		float dis_间隔2;
		if (num < 8f)
		{
			count = 5;
			time_间隔 = 0.16f;
			dis_间隔2 = num / 2f;
		}
		else
		{
			count = 8;
			time_间隔 = 0.12f;
			dis_间隔2 = num / 3f;
		}
		dis_间隔2 = Mathf.Clamp(dis_间隔2, 1.5f, 8f);
		for (int i = 0; i < count; i++)
		{
			F_生成大落雷(pos_自己 + i * dir * dis_间隔2);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time_间隔));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
	}

	private IEnumerator Cor_空手掀地板()
	{
		base._Ani.Play("攻击_空手掀地板", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 掀地._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 掀地._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		_跟随移动 = true;
		for (int i = 0; i < 4; i++)
		{
			F_更新攻击方向();
			_MoveDirSpeed = _攻击方向 * 24f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
		_跟随移动 = false;
		base._Rb2D.velocity = _攻击方向 * 34f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.63f));
	}

	private IEnumerator Cor_空手砸地板()
	{
		base._Ani.Play("攻击_空手砸地板", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 砸地._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 砸地._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		_跟随移动 = true;
		for (int j = 0; j < 3; j++)
		{
			F_更新攻击方向();
			_MoveDirSpeed = _攻击方向 * 26f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
		_跟随移动 = false;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.18f));
		Vector2 pos_自己 = _Mono_百眼魔君._Tsf_插地剑.position;
		Vector2 b = _Data._Pos_追击目标位置;
		Vector2 dir = _攻击方向;
		float num = Vector2.Distance(pos_自己, b);
		int count = 5;
		float time_间隔 = 0.12f;
		float dis_间隔2 = num / 3f;
		dis_间隔2 = Mathf.Clamp(dis_间隔2, 2.5f, 8f);
		for (int j = 1; j <= count; j++)
		{
			F_生成大落雷(pos_自己 + j * dir * dis_间隔2);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time_间隔));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.48f));
	}

	private IEnumerator Cor_电光炮()
	{
		base._Ani.Play("攻击_电光炮", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 电光炮._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 电光炮._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.87f));
		F_生成雷球(_Mono_百眼魔君._Tsf_电光炮.position, ((Vector2)(_Data._Pos_追击目标位置 - _Mono_百眼魔君._Tsf_电光炮.position)).normalized);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	private IEnumerator Cor_眼爆()
	{
		base._Ani.Play("站立_无刀", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 眼爆._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 眼爆._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		base._Mono组件._SR_Model.DOFade(0f, 1f);
		base._Mono组件._SR_影子.DOFade(0f, 1f);
		_Mono_百眼魔君._SR_尾巴.DOFade(0f, 1f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		foreach (EnemyAI_百眼魔君_符纸 item in _Mono_百眼魔君._List_符纸)
		{
			item.F_隐藏();
		}
		int num = Random.Range(24, 36);
		_List_眼球 = new List<EnemyAI_百眼魔君_眼球>(num);
		Vector2 vector = _Mono_百眼魔君._Tsf_眼球生成范围.localScale / 2f;
		for (int j = 0; j < num; j++)
		{
			Vector2 vector2 = (Vector2)_Mono_百眼魔君._Tsf_眼球生成范围.position + new Vector2(Random.Range(0f - vector.x, vector.x), Random.Range(0f - vector.y, vector.y));
			EnemyAI_百眼魔君_眼球 enemyAI_百眼魔君_眼球 = Object.Instantiate(_Mono_百眼魔君._C_眼球_Prefab, vector2, Quaternion.identity);
			_List_眼球.Add(enemyAI_百眼魔君_眼球);
			enemyAI_百眼魔君_眼球.F_Init();
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		_Mono_百眼魔君._Co2D_尾巴.enabled = false;
		int 攻击次数 = Random.Range(8, 15);
		for (int i = 0; i < 攻击次数; i++)
		{
			Vector2 目标位置 = _Data._Pos_追击目标位置;
			Vector2 vector3 = _List_眼球.GetRandom()._Model_眼白.position;
			Transform tsf_眼球 = Object.Instantiate(_Mono_百眼魔君._Tsf_眼球_攻击指示拖尾_Prefab, vector3, Quaternion.identity).transform;
			tsf_眼球.DOMove(目标位置, 0.15f);
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(tsf_眼球.gameObject, 1f, delegate
			{
				Object.Destroy(tsf_眼球.gameObject);
			});
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
			F_生成大落雷_眼爆(目标位置);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.42f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.85f));
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
		_Mono_百眼魔君._Co2D_尾巴.enabled = true;
		base._Mono组件.transform.position = _Mono_百眼魔君._初始位置;
		F_更新攻击方向();
		base._Mono组件._SR_Model.DOFade(1f, 1.5f);
		base._Mono组件._SR_影子.DOFade(1f, 1.5f);
		_Mono_百眼魔君._SR_尾巴.DOFade(1f, 1.5f);
		F_清空眼球();
		foreach (EnemyAI_百眼魔君_符纸 item2 in _Mono_百眼魔君._List_符纸)
		{
			item2.F_归位();
		}
	}

	private void F_清空眼球()
	{
		if (_List_眼球 == null)
		{
			return;
		}
		foreach (EnemyAI_百眼魔君_眼球 item in _List_眼球)
		{
			if (item != null && item.gameObject != null)
			{
				item.F_销毁();
			}
		}
		_List_眼球.Clear();
		_List_眼球 = null;
	}
}
