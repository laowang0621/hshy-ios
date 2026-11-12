using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyState_赤尻马猴_攻击 : EnemyState_Boss
{
	private EnemyAI_赤尻马猴 _Mono_赤尻马猴;

	private DataInfo_Enemy招式数值 下劈;

	private DataInfo_Enemy招式数值 三连;

	private DataInfo_Enemy招式数值 前掏;

	private DataInfo_Enemy招式数值 后跳蹬;

	private DataInfo_Enemy招式数值 大风车;

	private DataInfo_Enemy招式数值 飞天一击;

	private DataInfo_Enemy招式数值 剑气;

	private DataInfo_Enemy招式数值 空中三连;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private float _EnterDrag;

	private bool _转阶段_2_Exit_需要判断;

	private float _血量百分比_2 = 0.55f;

	private List<int> _出招序号表 = new List<int> { 1, 2, 3, 4 };

	private List<int> _出招序号表2 = new List<int> { 6, 7, 8, 9, 10, 11 };

	private int _攻击序号 = 6;

	private List<C_弹道_赤尻马猴_黑边血色剑气> _List_All_延时剑气 = new List<C_弹道_赤尻马猴_黑边血色剑气>();

	private bool _成功激发所有剑气;

	private bool _前掏命中敌人;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		_Mono_赤尻马猴 = (EnemyAI_赤尻马猴)data._Mono;
		下劈 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(10001);
		三连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(10002);
		前掏 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(10003);
		后跳蹬 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(10004);
		大风车 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(10005);
		飞天一击 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(10006);
		剑气 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(10007);
		空中三连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(10008);
		_EnterDrag = base._Rb2D.drag;
		return this;
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		_前掏命中敌人 = false;
		_成功激发所有剑气 = false;
		base._Mono组件._C_攻击判断._伤害 = 5f;
		_Cor_攻击 = _Data._Mono组件.StartCoroutine(F_攻击());
	}

	public override void OnExit()
	{
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
			_Cor_攻击 = null;
		}
		_Mono_赤尻马猴._无敌 = false;
		if (_转阶段_2_Exit_需要判断)
		{
			_转阶段_2_Exit_需要判断 = false;
			if (!_Mono_赤尻马猴._可以转阶段_2 && !_Mono_赤尻马猴._是生翼状态)
			{
				_Mono_赤尻马猴._SR_黑影特效.DOKill();
				_Mono_赤尻马猴._SR_黑影特效.DOFade(0f, 0.5f).OnComplete(delegate
				{
					_Mono_赤尻马猴._SR_黑影特效.gameObject.SetActive(value: false);
				});
			}
		}
		if (!_成功激发所有剑气)
		{
			foreach (C_弹道_赤尻马猴_黑边血色剑气 item in _List_All_延时剑气)
			{
				item.F_销毁();
			}
		}
		_List_All_延时剑气.Clear();
		base._Mono组件._C_攻击判断._攻击命中回调 -= F_攻击_前掏_攻击命中回调;
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? num : (0f - num), num, num);
	}

	private IEnumerator F_攻击()
	{
		if (_Mono_赤尻马猴._可以转阶段_2 && _Data._Curr生命 < _Data._MaxHP * _血量百分比_2)
		{
			_攻击序号 = 5;
		}
		else if (_Mono_赤尻马猴._是生翼状态)
		{
			_攻击序号 = _出招序号表2[Random.Range(0, _出招序号表2.Count)];
		}
		else
		{
			_攻击序号 = _出招序号表[Random.Range(0, _出招序号表.Count)];
		}
		if (_Mono_赤尻马猴._生成时候蓄力攻击)
		{
			_Mono_赤尻马猴._生成时候蓄力攻击 = false;
			base._Mono组件._SR_Model.DOFade(1f, 2f);
			base._Mono组件._SR_影子.DOFade(1f, 2f);
			_攻击序号 = 5;
		}
		switch (_攻击序号)
		{
		case 1:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_函数分段_2());
			break;
		case 2:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_1_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_1_函数分段_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_2_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_2_函数分段_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_3_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_3_函数分段_2());
			break;
		case 3:
			if (F_判断能否接_前掏())
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏());
				if (_前掏命中敌人)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_后跳蹬());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_未命中());
				}
			}
			else
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_1_函数分段_1());
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_1_函数分段_2());
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_2_函数分段_1());
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_2_函数分段_2());
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_3_函数分段_1());
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_3_函数分段_2());
			}
			break;
		case 4:
			F_更新攻击方向();
			base._Ani.Play("攻击_大风车");
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.1f));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_大风车_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_大风车_函数分段_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_函数分段_2());
			break;
		case 5:
		{
			F_更新攻击方向();
			_转阶段_2_Exit_需要判断 = true;
			_Mono_赤尻马猴._可以转阶段_2 = false;
			base._Ani.Play("攻击_发生翼");
			_Mono_赤尻马猴._SR_黑影特效.SetAlpha(0f);
			_Mono_赤尻马猴._SR_黑影特效.gameObject.SetActive(value: true);
			_Mono_赤尻马猴._SR_黑影特效.DOFade(_Mono_赤尻马猴._生翼_透明度, 1f);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.4f));
			_Mono_赤尻马猴._是生翼状态 = true;
			_Mono_赤尻马猴._SR_刀光.color = _Mono_赤尻马猴._二阶段刀光颜色;
			F_更新攻击方向();
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			for (int i = 0; i < 3; i++)
			{
				base._Rb2D.velocity = _攻击方向 * (35 - i * 4);
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			}
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
			base._Rb2D.velocity = Vector2.zero;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
			break;
		}
		case 6:
		{
			F_更新攻击方向();
			base._Ani.Play("攻击_飞天一击");
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.65f));
			base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 飞天一击._伤害系数;
			base._Mono组件._C_攻击判断._击退力度 = 飞天一击._击退力度;
			Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
			_Mono_赤尻马猴._无敌 = true;
			base._Mono组件._Rb2D.drag = 2f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
			F_更新攻击方向();
			for (int i = 0; i < 5; i++)
			{
				base._Rb2D.velocity = _攻击方向 * (45f - (float)i * 0.8f);
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			}
			Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
			_Mono_赤尻马猴._无敌 = false;
			base._Mono组件._Rb2D.drag = _EnterDrag;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_Pro_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_Pro_函数分段_2());
			if (F_判断能否接_前掏())
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏());
				if (_前掏命中敌人)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_后跳蹬());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_未命中());
				}
			}
			break;
		}
		case 7:
			F_更新攻击方向();
			base._Ani.Play("攻击_空中三连");
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
			base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 空中三连._伤害系数;
			base._Mono组件._C_攻击判断._击退力度 = 空中三连._击退力度;
			Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
			_Mono_赤尻马猴._无敌 = true;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
			F_更新攻击方向();
			F_生成_幻影攻击();
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.8f));
			F_更新攻击方向();
			F_生成_幻影攻击();
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
			F_更新攻击方向();
			F_生成_幻影攻击();
			base._Ani.Play("攻击_空中三连_下劈");
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
			F_生成_剑气攻击_即时激发(_攻击方向);
			Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
			_Mono_赤尻马猴._无敌 = false;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
			if (F_判断能否接_前掏())
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏());
				if (_前掏命中敌人)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_后跳蹬());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_未命中());
				}
			}
			break;
		case 8:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_Pro_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_Pro_函数分段_2());
			if (F_判断能否接_前掏())
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏());
				if (_前掏命中敌人)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_后跳蹬());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_未命中());
				}
			}
			break;
		case 9:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_1_函数分段_1(isPro: true));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_1_函数分段_2(isPro: true));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_2_函数分段_1(isPro: true));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_2_函数分段_2(isPro: true));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_3_函数分段_1(isPro: true));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_3_函数分段_2(isPro: true));
			if (F_判断能否接_前掏())
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏());
				if (_前掏命中敌人)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_后跳蹬());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_未命中());
				}
			}
			break;
		case 10:
			F_更新攻击方向();
			base._Ani.Play("攻击_大风车");
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.1f));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_大风车_函数分段_1(isPro: true));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_大风车_函数分段_2(isPro: true));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_Pro_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_Pro_函数分段_2());
			if (F_判断能否接_前掏())
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏());
				if (_前掏命中敌人)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_后跳蹬());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_前掏_未命中());
				}
			}
			break;
		case 11:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_函数分段_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下劈_函数分段_2());
			break;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private bool F_判断能否接_前掏()
	{
		F_更新攻击方向();
		if (Random.value < 0.6f && Vector2.Distance(base._Mono组件.transform.position, _目标位置) < 15f)
		{
			return true;
		}
		return false;
	}

	private IEnumerator Cor_攻击_下劈_函数分段_1()
	{
		base._Ani.Play("攻击_下劈");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下劈._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
	}

	private IEnumerator Cor_攻击_下劈_函数分段_2()
	{
		for (int i = 0; i < 3; i++)
		{
			base._Rb2D.velocity = _攻击方向 * (35 - i * 4);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
	}

	private IEnumerator Cor_攻击_三连_1_函数分段_1(bool isPro = false)
	{
		base._Ani.Play("攻击_三连");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 三连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 三连._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = _攻击方向 * 28f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
	}

	private IEnumerator Cor_攻击_三连_1_函数分段_2(bool isPro = false)
	{
		if (isPro)
		{
			F_生成_剑气攻击_延时激发();
		}
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 24f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
	}

	private IEnumerator Cor_攻击_三连_2_函数分段_1(bool isPro = false)
	{
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = _攻击方向 * 32f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
	}

	private IEnumerator Cor_攻击_三连_2_函数分段_2(bool isPro = false)
	{
		if (isPro)
		{
			F_生成_剑气攻击_延时激发();
		}
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 28f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
	}

	private IEnumerator Cor_攻击_三连_3_函数分段_1(bool isPro = false)
	{
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = _攻击方向 * 36f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 32f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
	}

	private IEnumerator Cor_攻击_三连_3_函数分段_2(bool isPro = false)
	{
		base._Rb2D.velocity = _攻击方向 * 28f;
		if (isPro)
		{
			F_生成_剑气攻击_延时激发();
			F_激发所有剑气();
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	private void F_生成_剑气攻击_延时激发()
	{
		C_弹道_赤尻马猴_黑边血色剑气 c_弹道_赤尻马猴_黑边血色剑气 = Object.Instantiate(_Mono_赤尻马猴._C_弹道_赤尻马猴_黑边血色剑气, _Mono_赤尻马猴._Tsf_地上剑气.position, Quaternion.identity);
		c_弹道_赤尻马猴_黑边血色剑气.transform.localScale = base._Mono.transform.localScale;
		c_弹道_赤尻马猴_黑边血色剑气.F_原地保留(_攻击方向, base._Mono组件._C_攻击判断._伤害);
		_List_All_延时剑气.Add(c_弹道_赤尻马猴_黑边血色剑气);
	}

	private void F_激发所有剑气()
	{
		_成功激发所有剑气 = true;
		foreach (C_弹道_赤尻马猴_黑边血色剑气 item in _List_All_延时剑气)
		{
			item.F_激发();
		}
	}

	private IEnumerator Cor_攻击_前掏()
	{
		base._Ani.Play("攻击_前掏");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 前掏._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 前掏._击退力度;
		_前掏命中敌人 = false;
		base._Mono组件._C_攻击判断._攻击命中回调 += F_攻击_前掏_攻击命中回调;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 38f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
	}

	private void F_攻击_前掏_攻击命中回调()
	{
		_前掏命中敌人 = true;
	}

	private IEnumerator Cor_攻击_前掏_未命中()
	{
		base._Rb2D.velocity = _攻击方向 * 28f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private IEnumerator Cor_攻击_前掏_后跳蹬()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 后跳蹬._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 后跳蹬._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		Vector2 dir_1 = _攻击方向;
		for (int i = 0; i < 6; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.12f) * (24f - (float)i * 1.8f);
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.12f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	private IEnumerator Cor_攻击_大风车_函数分段_1(bool isPro = false)
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 大风车._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 大风车._击退力度;
		Vector2 dir_1 = _攻击方向;
		for (int i = 0; i < 5; i++)
		{
			F_更新攻击方向();
			if (isPro && (i & 1) == 1)
			{
				F_生成_剑气攻击_即时激发(Random.insideUnitCircle);
			}
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.12f) * (26f - (float)i * 1.8f);
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.08f));
		}
	}

	private IEnumerator Cor_攻击_大风车_函数分段_2(bool isPro = false)
	{
		Vector2 dir_1 = _攻击方向;
		for (int i = 0; i < 5; i++)
		{
			F_更新攻击方向();
			if (isPro && (i & 1) == 1)
			{
				F_生成_剑气攻击_即时激发(Random.insideUnitCircle);
			}
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.12f) * (26f - (float)i * 1.8f);
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.08f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
	}

	private void F_生成_幻影攻击()
	{
		Vector2 vector = _Mono_赤尻马猴._Tsf_幻影攻击.position;
		C_弹道_弓箭 c_弹道_弓箭 = Object.Instantiate(_Mono_赤尻马猴._弹道_弓箭_幻影攻击, vector, Quaternion.identity);
		c_弹道_弓箭._Tsf_Model.localScale = new Vector3(1f, base._Mono.transform.localScale.x, 1f);
		Vector2 normalized = (_目标位置 - vector).normalized;
		c_弹道_弓箭.F_Init(normalized, base._Mono._Data._Atk * 空中三连._伤害系数);
	}

	private void F_生成_剑气攻击_即时激发(Vector2 dir)
	{
		C_弹道_赤尻马猴_黑边血色剑气 c_弹道_赤尻马猴_黑边血色剑气 = Object.Instantiate(_Mono_赤尻马猴._C_弹道_赤尻马猴_黑边血色剑气, _Mono_赤尻马猴._Tsf_地上剑气.position, Quaternion.identity);
		c_弹道_赤尻马猴_黑边血色剑气.transform.localScale = new Vector3((dir.x > 0f) ? 1 : (-1), 1f, 1f);
		c_弹道_赤尻马猴_黑边血色剑气.F_Init(dir, base._Mono._Data._Atk * 剑气._伤害系数);
	}

	private IEnumerator Cor_攻击_下劈_Pro_函数分段_1()
	{
		base._Ani.Play("攻击_下劈_Pro");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下劈._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._SR_Model.DOFade(1f, 0.4f);
		Vector2 vector = base._Mono组件.transform.position;
		Vector2 vector2 = _Data._Pos_追击目标位置;
		Vector2 vector3 = ((vector.x > vector2.x) ? Vector2.left : Vector2.right);
		float num = Random.Range(3f, 6f);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2, vector3, num, 2048);
		if ((bool)raycastHit2D)
		{
			base._Mono组件.transform.position = raycastHit2D.point;
		}
		else
		{
			base._Mono组件.transform.position = vector2 + vector3 * num;
		}
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
	}

	private IEnumerator Cor_攻击_下劈_Pro_函数分段_2()
	{
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_生成_剑气攻击_即时激发(_攻击方向);
		for (int i = 0; i < 3; i++)
		{
			base._Rb2D.velocity = _攻击方向 * (35 - i * 4);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
	}
}
