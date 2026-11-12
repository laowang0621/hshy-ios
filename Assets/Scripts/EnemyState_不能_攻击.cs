using System.Collections;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class EnemyState_不能_攻击 : EnemyState_Boss
{
	private EnemyAI_不能 _Mono_不能;

	private DataInfo_Enemy招式数值 三连拳_1_2;

	private DataInfo_Enemy招式数值 上勾拳;

	private DataInfo_Enemy招式数值 二连掌_1;

	private DataInfo_Enemy招式数值 二连掌_2;

	private DataInfo_Enemy招式数值 二连翻滚;

	private DataInfo_Enemy招式数值 冲拳;

	private DataInfo_Enemy招式数值 回旋踢;

	private DataInfo_Enemy招式数值 膝顶;

	private DataInfo_Enemy招式数值 街舞;

	private DataInfo_Enemy招式数值 跳砸;

	private DataInfo_Enemy招式数值 攻击架势;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private float _EnterDrag;

	private int _攻击序号 = 1;

	private bool _可以转阶段_2 = true;

	private float _血量百分比_2 = 0.55f;

	private List<int> _出招序号表 = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_不能 = (EnemyAI_不能)data._Mono;
		三连拳_1_2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9001);
		上勾拳 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9002);
		二连掌_1 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9003);
		二连掌_2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9004);
		二连翻滚 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9005);
		冲拳 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9006);
		回旋踢 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9007);
		膝顶 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9008);
		街舞 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9009);
		跳砸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9010);
		攻击架势 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9011);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
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
		_Mono_不能._攻击架势 = false;
		_Mono_不能._无敌 = false;
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

	private bool F_判断是否是远距离(float 距离 = 13f)
	{
		F_更新攻击方向();
		if (Vector2.Distance(base._Mono.transform.position, _目标位置) < 距离)
		{
			return false;
		}
		return true;
	}

	private IEnumerator F_攻击()
	{
		if (_可以转阶段_2 && _Data._Curr生命 < _Data._MaxHP * _血量百分比_2)
		{
			_可以转阶段_2 = false;
			_出招序号表.Add(10);
			_出招序号表.Add(10);
			_出招序号表.Add(11);
			_出招序号表.Add(12);
			_出招序号表.Add(13);
			yield return _Data._Mono组件.StartCoroutine(Cor_转阶段_攻击架势());
			base._Mono.ChangeState(EnemyStateName_Type.站立);
			yield break;
		}
		_攻击序号 = _出招序号表.GetRandom();
		switch (_攻击序号)
		{
		case 1:
		{
			base._Ani.Play("攻击_二连翻滚");
			base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连翻滚._伤害系数;
			base._Mono组件._C_攻击判断._击退力度 = 二连翻滚._击退力度;
			float 起始速度2 = 24f;
			if (F_判断是否是远距离())
			{
				起始速度2 = 28f;
			}
			for (int tt2 = 0; tt2 < 2; tt2++)
			{
				F_更新攻击方向();
				Vector2 dir_2 = _攻击方向;
				for (int i = 0; i < 6; i++)
				{
					F_更新攻击方向();
					base._Rb2D.velocity = Vector2.Lerp(dir_2, _攻击方向, 0.22f) * (起始速度2 - (float)(i * 2));
					dir_2 = _攻击方向;
					yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
				}
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
			}
			if (!_可以转阶段_2 && Random.value < 0.66f)
			{
				if (Random.value < 0.4f)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
			}
			else if (Random.value < 0.5f)
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢());
			}
			else
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_膝顶());
			}
			break;
		}
		case 2:
		{
			base._Ani.Play("攻击_街舞");
			base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 街舞._伤害系数;
			base._Mono组件._C_攻击判断._击退力度 = 街舞._击退力度;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			float 起始速度2 = 18f;
			if (F_判断是否是远距离())
			{
				起始速度2 = 24f;
			}
			Vector2 dir_2 = _攻击方向;
			for (int tt2 = 0; tt2 < 12; tt2++)
			{
				F_更新攻击方向();
				base._Rb2D.velocity = Vector2.Lerp(dir_2, _攻击方向, 0.22f) * (起始速度2 - (float)tt2 * 0.8f);
				dir_2 = _攻击方向;
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			}
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
			if (!_可以转阶段_2 && Random.value < 0.66f)
			{
				if (Random.value < 0.4f)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_冲拳());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
			}
			else
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢_快());
				if (Random.value < 0.5f)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢());
					break;
				}
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_膝顶());
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢_快());
			}
			break;
		}
		case 3:
		{
			base._Ani.Play("攻击_翻滚跳踩");
			base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 跳砸._伤害系数;
			base._Mono组件._C_攻击判断._击退力度 = 跳砸._击退力度;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
			float 起始速度2 = 24f;
			if (F_判断是否是远距离())
			{
				起始速度2 = 28f;
			}
			Vector2 dir_2 = _攻击方向;
			for (int tt2 = 0; tt2 < 5; tt2++)
			{
				if (tt2 < 3)
				{
					F_更新攻击方向();
				}
				if (tt2 == 3)
				{
					_Mono_不能._无敌 = true;
				}
				base._Rb2D.velocity = Vector2.Lerp(dir_2, _攻击方向, 0.22f) * (起始速度2 - (float)(tt2 * 2));
				dir_2 = _攻击方向;
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			}
			_Mono_不能._无敌 = false;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
			if (!_可以转阶段_2 && Random.value < 0.66f)
			{
				if (Random.value < 0.3f)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_1());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
				else if (Random.value < 0.8f)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_跳砸());
				}
			}
			else
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢());
				if (Random.value < 0.5f)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢());
					break;
				}
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_膝顶());
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢_快());
			}
			break;
		}
		case 4:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢_快());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢_快());
			if (!_可以转阶段_2)
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢_快());
			}
			break;
		case 5:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_膝顶());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_膝顶());
			if (!_可以转阶段_2)
			{
				if (Random.value < 0.6f)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_冲拳());
				}
			}
			break;
		case 6:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢());
			if (!_可以转阶段_2)
			{
				if (Random.value < 0.4f)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_冲拳());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
			}
			break;
		case 7:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_膝顶());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_回旋踢());
			if (!_可以转阶段_2)
			{
				if (Random.value < 0.4f)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_冲拳());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
					yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
				}
			}
			break;
		case 10:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击架势());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_3());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_4());
			break;
		case 11:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_冲拳());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
			break;
		case 12:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_跳砸());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_跳砸());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_跳砸());
			if (Random.value < 0.5f)
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_跳砸());
			}
			break;
		case 13:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_跳砸());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_冲拳());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连拳_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_上勾拳());
			break;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_攻击_回旋踢()
	{
		base._Ani.Play("攻击_回旋踢", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 回旋踢._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 回旋踢._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		float 起始速度 = 26f;
		if (F_判断是否是远距离())
		{
			起始速度 = 35f;
		}
		_ = _攻击方向;
		base._Rb2D.velocity = _攻击方向 * 起始速度 * 0.65f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 起始速度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.08f));
		base._Rb2D.velocity = _攻击方向 * 起始速度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.52f));
	}

	private IEnumerator Cor_攻击_回旋踢_快()
	{
		base._Ani.Play("攻击_回旋踢_快", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 回旋踢._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 回旋踢._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
		float 起始速度 = 24f;
		if (F_判断是否是远距离())
		{
			起始速度 = 30f;
		}
		_ = _攻击方向;
		base._Rb2D.velocity = _攻击方向 * 起始速度 * 0.65f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 起始速度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.08f));
		base._Rb2D.velocity = _攻击方向 * 起始速度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private IEnumerator Cor_攻击_膝顶()
	{
		base._Ani.Play("攻击_膝顶", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 膝顶._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 膝顶._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		float num = 24f;
		if (F_判断是否是远距离())
		{
			num = 30f;
		}
		base._Rb2D.velocity = _攻击方向 * num;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.53f));
	}

	private IEnumerator Cor_攻击_冲拳()
	{
		base._Ani.Play("攻击_冲拳", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 冲拳._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 冲拳._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.18f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 45f, _攻击方向.y * 18f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.52f));
	}

	private IEnumerator Cor_攻击架势(float time = -1f)
	{
		base._Ani.Play("攻击架势", 0, 0f);
		_Mono_不能._攻击架势 = true;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 攻击架势._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 攻击架势._击退力度;
		if (time < 0.66f)
		{
			time = ((time != -1f) ? 0.66f : Random.Range(0.66f, 1.66f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
		_Mono_不能._攻击架势 = false;
	}

	private IEnumerator Cor_转阶段_攻击架势(float time = -1f)
	{
		base._Ani.Play("转阶段_攻击架势", 0, 0f);
		_Mono_不能._攻击架势 = true;
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 攻击架势._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 攻击架势._击退力度;
		if (time < 2f)
		{
			time = ((time != -1f) ? 2f : Random.Range(2f, 2.66f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
		_Mono_不能._攻击架势 = false;
	}

	private IEnumerator Cor_攻击_上勾拳()
	{
		base._Ani.Play("攻击_上勾拳", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 上勾拳._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 上勾拳._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.18f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 45f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.57f));
	}

	private IEnumerator Cor_攻击_三连拳()
	{
		base._Ani.Play("攻击_三连拳");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 三连拳_1_2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 三连拳_1_2._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 35f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 35f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2(((!(_攻击方向.x > 0f)) ? 1 : (-1)) * 18, 0f);
		if (Random.value < 0.6f)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.85f));
			yield break;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.05f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2(((!(_攻击方向.x > 0f)) ? 1 : (-1)) * 22, 0f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.85f));
	}

	private IEnumerator Cor_攻击_三连拳_1()
	{
		base._Ani.Play("攻击_三连拳_1", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 三连拳_1_2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 三连拳_1_2._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 35f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	private IEnumerator Cor_攻击_三连拳_2()
	{
		base._Ani.Play("攻击_三连拳_2", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 三连拳_1_2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 三连拳_1_2._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 35f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
	}

	private IEnumerator Cor_攻击_三连拳_3()
	{
		base._Ani.Play("攻击_三连拳_3");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连掌_1._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连掌_1._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = new Vector2(((!(_攻击方向.x > 0f)) ? 1 : (-1)) * 18, 0f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.95f));
	}

	private IEnumerator Cor_攻击_三连拳_4()
	{
		base._Ani.Play("攻击_三连拳_4");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连掌_2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连掌_2._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2(((!(_攻击方向.x > 0f)) ? 1 : (-1)) * 22, 0f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.85f));
	}

	private IEnumerator Cor_攻击_跳砸()
	{
		base._Ani.Play("攻击_跳砸", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 跳砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 跳砸._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		float 起始速度 = 28f;
		if (F_判断是否是远距离())
		{
			起始速度 = 36f;
		}
		Vector2 dir_1 = _攻击方向;
		for (int i = 0; i < 5; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.22f) * (起始速度 - (float)(i * 2));
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.51f));
	}
}
