using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyState_寅虎_攻击 : EnemyState_Boss
{
	private EnemyAI_寅虎 _Mono_寅虎;

	private DataInfo_Enemy招式数值 下劈;

	private DataInfo_Enemy招式数值 横砍;

	private DataInfo_Enemy招式数值 旋风劈;

	private DataInfo_Enemy招式数值 咆哮;

	private DataInfo_Enemy招式数值 跳劈;

	private DataInfo_Enemy招式数值 逆回旋;

	private DataInfo_Enemy招式数值 铁山靠;

	private DataInfo_Enemy招式数值 袈裟斩;

	private DataInfo_Enemy招式数值 蓄力下劈;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private float _EnterDrag;

	private int _攻击序号;

	private List<int> _出招序号表 = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

	private List<int> _出招序号表_无隐身 = new List<int> { 1, 2, 3, 4, 5, 6 };

	private int _磐石1 = 2;

	private int _磐石2 = 4;

	private int _磐石3 = 6;

	private float _磐石_时间 = 0.65f;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_寅虎 = (EnemyAI_寅虎)data._Mono;
		下劈 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(12001);
		横砍 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(12002);
		旋风劈 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(12003);
		咆哮 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(12004);
		跳劈 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(12005);
		逆回旋 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(12006);
		铁山靠 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(12007);
		袈裟斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(12008);
		蓄力下劈 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(12009);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		MyLog.LogError("寅虎攻击?>>");
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
		if (!_Mono_寅虎._触发磐石)
		{
			_Mono_寅虎._可以磐石 = false;
		}
		base._Mono组件._SR_影子.DOKill();
		base._Mono组件._SR_影子.SetAlpha(1f);
		base._Rb2D.drag = _EnterDrag;
		F_取消不可被攻击();
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
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private bool F_判断是否是远距离(float 距离 = 15f)
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
		if (_Mono_寅虎._非受击死亡状态_定身中被攻击)
		{
			_Mono_寅虎._非受击死亡状态_定身中被攻击 = false;
			_攻击序号 = 7;
		}
		else
		{
			if (_Mono_寅虎._触发磐石)
			{
				_Mono_寅虎._可以磐石 = true;
				_Mono_寅虎._触发磐石 = false;
				int 能量2 = _Mono_寅虎._磐石能量;
				if (能量2 < _磐石3)
				{
					base._Ani.Play("攻击_振刀", 0, 0f);
					base._Rb2D.velocity = Vector2.zero;
					F_更新攻击方向();
					if (能量2 == 0 || 能量2 == _磐石1 || 能量2 == _磐石2)
					{
						Object.Instantiate(_Mono_寅虎._GO_磐石特效_Prefab, base._Mono组件.transform);
					}
					_Mono_寅虎._磐石能量++;
					yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(_磐石_时间));
				}
				else
				{
					Object.Instantiate(_Mono_寅虎._GO_蓄力特效_Prefab, base._Mono组件.transform);
				}
				_Mono_寅虎._可以磐石 = false;
				_Mono_寅虎._磐石能量 = 0;
				if (能量2 < _磐石1)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_下劈());
				}
				else if (能量2 < _磐石2)
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_红刀_蓄力下劈(0.2f));
					yield return _Data._Mono组件.StartCoroutine(Cor_红刀_蓄力下劈());
				}
				else
				{
					yield return _Data._Mono组件.StartCoroutine(Cor_红刀_蓄力下劈(0.2f));
					yield return _Data._Mono组件.StartCoroutine(Cor_红刀_蓄力下劈(0.2f));
					yield return _Data._Mono组件.StartCoroutine(Cor_红刀_蓄力下劈());
				}
				base._Mono.ChangeState(EnemyStateName_Type.站立);
				yield break;
			}
			_攻击序号 = _出招序号表[Random.Range(0, _出招序号表.Count)];
		}
		switch (_攻击序号)
		{
		case 1:
			yield return _Data._Mono组件.StartCoroutine(Cor_下劈());
			yield return _Data._Mono组件.StartCoroutine(Cor_横砍());
			break;
		case 2:
			yield return _Data._Mono组件.StartCoroutine(Cor_冲刺());
			yield return _Data._Mono组件.StartCoroutine(Cor_旋风劈_1(需要速度归零: false));
			yield return _Data._Mono组件.StartCoroutine(Cor_旋风劈_2(需要速度归零: false));
			break;
		case 3:
			yield return _Data._Mono组件.StartCoroutine(Cor_咆哮());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击前摇(0.2f));
			yield return _Data._Mono组件.StartCoroutine(Cor_跳劈());
			break;
		case 4:
			yield return _Data._Mono组件.StartCoroutine(Cor_逆回旋());
			yield return _Data._Mono组件.StartCoroutine(Cor_铁山靠());
			break;
		case 5:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击前摇(0.2f));
			yield return _Data._Mono组件.StartCoroutine(Cor_旋风劈_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_旋风劈_1(需要速度归零: false));
			yield return _Data._Mono组件.StartCoroutine(Cor_旋风劈_2(需要速度归零: false));
			break;
		case 6:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击前摇(0.2f));
			yield return _Data._Mono组件.StartCoroutine(Cor_袈裟斩_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_袈裟斩_2());
			break;
		case 7:
		{
			_Mono_寅虎.F_触发隐身();
			if (Random.value < 0.5f)
			{
				Cor_叶影();
				Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.5f));
				yield return _Data._Mono组件.StartCoroutine(Cor_影_下劈_影());
				Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1f));
				yield return _Data._Mono组件.StartCoroutine(Cor_影_转_影());
				Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.5f));
				yield return _Data._Mono组件.StartCoroutine(Cor_影_转_转());
				Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
				break;
			}
			Cor_叶影();
			Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.5f));
			yield return _Data._Mono组件.StartCoroutine(Cor_影_下劈_影());
			Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1f));
			yield return _Data._Mono组件.StartCoroutine(Cor_影_转_影());
			Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.5f));
			yield return _Data._Mono组件.StartCoroutine(Cor_影_蓄力());
			Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
			for (int 能量2 = 0; 能量2 < 3; 能量2++)
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
				Object.Instantiate(_Mono_寅虎._GO_蓄力特效_Prefab, base._Mono组件.transform);
			}
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
			yield return _Data._Mono组件.StartCoroutine(Cor_红刀_蓄力下劈(0.3f));
			yield return _Data._Mono组件.StartCoroutine(Cor_红刀_蓄力下劈(0.2f));
			yield return _Data._Mono组件.StartCoroutine(Cor_红刀_蓄力下劈());
			break;
		}
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_下劈()
	{
		base._Ani.Play("攻击_下劈", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下劈._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		if (F_判断是否是远距离(13f))
		{
			base._Rb2D.velocity = _攻击方向 * 60f;
		}
		else
		{
			base._Rb2D.velocity = _攻击方向 * 30f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	private IEnumerator Cor_横砍()
	{
		base._Ani.Play("攻击_横砍", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 横砍._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 横砍._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		if (F_判断是否是远距离(10f))
		{
			base._Rb2D.velocity = _攻击方向 * 55f;
		}
		else
		{
			base._Rb2D.velocity = _攻击方向 * 30f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
	}

	private IEnumerator Cor_冲刺()
	{
		base._Ani.Play("冲刺", 0, 0f);
		F_更新攻击方向();
		float num = Vector2.Distance(base._Mono.transform.position, _目标位置);
		Vector2 dir_1 = _攻击方向;
		int count2 = 1 + (int)num / 2;
		count2 = Mathf.Clamp(count2, 4, 12);
		float 起始速度 = 20 + 2 * count2;
		MyLog.LogError($"寅虎冲刺Count={count2}>>距离={num}>>速度={起始速度}");
		for (int i = 0; i < count2; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.11f) * (起始速度 - (float)i * 2.85f);
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.045f));
		}
	}

	private IEnumerator Cor_旋风劈_1(bool 需要速度归零 = true)
	{
		base._Ani.Play("攻击_旋风劈_1", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 旋风劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 旋风劈._击退力度;
		if (需要速度归零)
		{
			base._Rb2D.velocity = Vector2.zero;
		}
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		if (F_判断是否是远距离(10f))
		{
			base._Rb2D.velocity = _攻击方向 * 50f;
		}
		else
		{
			base._Rb2D.velocity = _攻击方向 * 30f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	private IEnumerator Cor_旋风劈_2(bool 需要速度归零 = true)
	{
		base._Ani.Play("攻击_旋风劈_2", 0, 0f);
		if (需要速度归零)
		{
			base._Rb2D.velocity = Vector2.zero;
		}
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		if (F_判断是否是远距离(10f))
		{
			base._Rb2D.velocity = _攻击方向 * 50f;
		}
		else
		{
			base._Rb2D.velocity = _攻击方向 * 30f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
	}

	private IEnumerator Cor_咆哮()
	{
		base._Ani.Play("咆哮", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 咆哮._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 咆哮._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	private IEnumerator Cor_跳劈()
	{
		base._Ani.Play("攻击_跳劈", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 跳劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 跳劈._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.23f));
		F_更新攻击方向();
		float 起始速度 = 30f;
		if (F_判断是否是远距离(13f))
		{
			起始速度 = 48f;
		}
		F_更新攻击方向();
		Vector2 dir_1 = _攻击方向;
		for (int i = 0; i < 6; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.22f) * (起始速度 - (float)(i * 2));
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.47f));
	}

	private IEnumerator Cor_逆回旋()
	{
		base._Ani.Play("攻击_逆回旋", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 逆回旋._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 逆回旋._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.23f));
		F_更新攻击方向();
		float 起始速度 = 28f;
		if (F_判断是否是远距离(13f))
		{
			起始速度 = 38f;
		}
		F_更新攻击方向();
		Vector2 dir_1 = _攻击方向;
		for (int i = 0; i < 4; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.22f) * (起始速度 - (float)(i * 2));
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.06f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.47f));
	}

	private IEnumerator Cor_铁山靠()
	{
		base._Ani.Play("攻击_铁山靠", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 铁山靠._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 铁山靠._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		float 起始速度 = 30f;
		if (F_判断是否是远距离(9f))
		{
			起始速度 = 40f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		Vector2 dir_1 = _攻击方向;
		for (int i = 0; i < 4; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.22f) * (起始速度 - (float)(i * 3));
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.06f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
	}

	private IEnumerator Cor_攻击前摇(float time = 0.15f)
	{
		base._Ani.Play("攻击前摇", 0, 0f);
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
	}

	private IEnumerator Cor_袈裟斩_1()
	{
		base._Ani.Play("攻击_袈裟斩_1", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 袈裟斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 袈裟斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		if (F_判断是否是远距离(10f))
		{
			base._Rb2D.velocity = _攻击方向 * 50f;
		}
		else
		{
			base._Rb2D.velocity = _攻击方向 * 30f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
	}

	private IEnumerator Cor_袈裟斩_2()
	{
		base._Ani.Play("攻击_袈裟斩_2", 0, 0f);
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		F_更新攻击方向();
		float 起始速度 = 30f;
		if (F_判断是否是远距离(9f))
		{
			起始速度 = 40f;
		}
		Vector2 dir_1 = _攻击方向;
		for (int i = 0; i < 4; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = Vector2.Lerp(dir_1, _攻击方向, 0.22f) * (起始速度 - (float)(i * 3));
			dir_1 = _攻击方向;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.06f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	protected void F_不可被攻击()
	{
		base._Mono._阵营 = 1;
		base._Mono.gameObject.layer = 3;
	}

	protected void F_取消不可被攻击()
	{
		base._Mono._阵营 = 2;
		base._Mono.gameObject.layer = 6;
	}

	private void Cor_叶影()
	{
		base._Ani.Play("攻击_叶影", 0, 0f);
		base._Rb2D.velocity = Vector2.zero;
		base._Mono组件._SR_影子.DOFade(0f, 0.5f);
		F_不可被攻击();
		F_更新攻击方向();
	}

	private void F_出现在敌人身后()
	{
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
	}

	private IEnumerator Cor_影_下劈_影()
	{
		base._Ani.Play("攻击_影_下劈_影", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下劈._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_出现在敌人身后();
		base._Mono组件._SR_影子.DOFade(1f, 0.3f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_取消不可被攻击();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
		base._Mono组件._SR_影子.DOFade(0f, 0.5f);
		F_不可被攻击();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
	}

	private IEnumerator Cor_影_转_影()
	{
		base._Ani.Play("攻击_影_转_影", 0, 0f);
		base._Rb2D.velocity = Vector2.zero;
		F_出现在敌人身后();
		base._Mono组件._SR_影子.DOFade(1f, 0.3f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_取消不可被攻击();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
		base._Mono组件._SR_影子.DOFade(0f, 0.5f);
		F_不可被攻击();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
	}

	private IEnumerator Cor_影_转_转()
	{
		base._Ani.Play("攻击_影_转_转", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 旋风劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 旋风劈._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_出现在敌人身后();
		base._Mono组件._SR_影子.DOFade(1f, 0.3f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_取消不可被攻击();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
	}

	private IEnumerator Cor_影_蓄力()
	{
		base._Ani.Play("攻击_影_蓄力", 0, 0f);
		base._Rb2D.velocity = Vector2.zero;
		F_出现在敌人身后();
		base._Mono组件._SR_影子.DOFade(1f, 0.3f);
		F_取消不可被攻击();
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	private IEnumerator Cor_红刀_蓄力下劈(float time = 0.5f)
	{
		base._Ani.Play("攻击_红刀_蓄力下劈", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 蓄力下劈._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 蓄力下劈._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		Object.Instantiate(_Mono_寅虎._GO_蓄力下劈特效_Prefab, _Mono_寅虎.transform.position, Quaternion.identity).transform.localScale = new Vector3((base._Mono.transform.localScale.x > 0f) ? 1 : (-1), 1f, 1f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
	}
}
