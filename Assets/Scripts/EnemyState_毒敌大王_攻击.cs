using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyState_毒敌大王_攻击 : EnemyState_Boss
{
	private EnemyAI_毒敌大王 _Mono_毒敌大王;

	private DataInfo_Enemy招式数值 二连;

	private DataInfo_Enemy招式数值 七连;

	private DataInfo_Enemy招式数值 正鞭;

	private DataInfo_Enemy招式数值 侧鞭;

	private DataInfo_Enemy招式数值 尾后针;

	private DataInfo_Enemy招式数值 钳地手;

	private DataInfo_Enemy招式数值 入土;

	private DataInfo_Enemy招式数值 出土;

	private DataInfo_Enemy招式数值 毒爆;

	private Vector2 _Pos_尾巴_3;

	private Vector2 _Pos_尾巴_4;

	private Vector2 _Pos_尾巴_5;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private float _EnterDrag;

	private bool _跟随移动;

	private Vector2 _MoveDirSpeed;

	private bool 清空尾巴;

	private int _攻击序号 = 10;

	private List<int> _出招序号表 = new List<int>
	{
		1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
		11
	};

	private int _x次攻击内不会重复出入土;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_毒敌大王 = (EnemyAI_毒敌大王)data._Mono;
		二连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(16001);
		七连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(16002);
		正鞭 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(16003);
		侧鞭 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(16004);
		尾后针 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(16005);
		钳地手 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(16006);
		入土 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(16007);
		出土 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(16008);
		毒爆 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(16009);
		_Pos_尾巴_3 = _Mono_毒敌大王._Tsf_尾巴_3.localPosition;
		_Pos_尾巴_4 = _Mono_毒敌大王._Tsf_尾巴_4.localPosition;
		_Pos_尾巴_5 = _Mono_毒敌大王._Tsf_尾巴_5.localPosition;
		_x次攻击内不会重复出入土 = 3;
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
		清空尾巴 = false;
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		_Data._Mono组件.StopAllCoroutines();
		base._Mono组件.StartCoroutine(Cor_攻击());
	}

	public override void OnExit()
	{
		_Data._Mono组件.StopAllCoroutines();
		_跟随移动 = false;
		base._Rb2D.bodyType = RigidbodyType2D.Dynamic;
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
		if (清空尾巴)
		{
			_Mono_毒敌大王._Tsf_尾巴_3.DOKill();
			_Mono_毒敌大王._Tsf_尾巴_4.DOKill();
			_Mono_毒敌大王._Tsf_尾巴_5.DOKill();
			_Mono_毒敌大王._Tsf_尾巴_3.DOKill();
			_Mono_毒敌大王._Tsf_尾巴_4.DOKill();
			_Mono_毒敌大王._Tsf_尾巴_5.DOKill();
		}
	}

	private IEnumerator Cor_攻击()
	{
		_攻击序号 = _出招序号表.GetRandom();
		Vector2 vector = base._Mono.transform.position;
		Vector2 vector2 = (Vector2)_Data._Pos_追击目标位置 - vector;
		float magnitude = vector2.magnitude;
		Vector2 direction = vector2 / magnitude;
		if ((bool)Physics2D.Raycast(vector, direction, magnitude, LayerMask.GetMask("Default", "中立", "障碍")))
		{
			_攻击序号 = 8;
		}
		if (_攻击序号 == 8)
		{
			if (_x次攻击内不会重复出入土 >= 0)
			{
				_攻击序号 = _出招序号表.GetRandom();
			}
			else
			{
				_x次攻击内不会重复出入土 = 2;
			}
		}
		_x次攻击内不会重复出入土--;
		if (_Mono_毒敌大王._开启固定出招)
		{
			_攻击序号 = _Mono_毒敌大王._固定出招序号;
		}
		switch (_攻击序号)
		{
		case 1:
			yield return base._Mono组件.StartCoroutine(Cor_二连());
			break;
		case 2:
			yield return base._Mono组件.StartCoroutine(Cor_七连());
			break;
		case 3:
			yield return base._Mono组件.StartCoroutine(Cor_正鞭());
			break;
		case 4:
			yield return base._Mono组件.StartCoroutine(Cor_侧侧鞭());
			break;
		case 5:
			yield return base._Mono组件.StartCoroutine(Cor_侧侧正鞭());
			break;
		case 6:
			yield return base._Mono组件.StartCoroutine(Cor_尾后针());
			break;
		case 7:
			yield return base._Mono组件.StartCoroutine(Cor_钳地手());
			break;
		case 8:
			yield return base._Mono组件.StartCoroutine(Cor_入土());
			break;
		case 9:
			yield return base._Mono组件.StartCoroutine(Cor_侧侧鞭());
			break;
		case 10:
			yield return base._Mono组件.StartCoroutine(Cor_侧侧正鞭());
			break;
		case 11:
			yield return base._Mono组件.StartCoroutine(Cor_尾后针());
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

	private IEnumerator Cor_二连()
	{
		base._Ani.Play("攻击_二连", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.83f));
	}

	private IEnumerator Cor_七连()
	{
		base._Ani.Play("攻击_七连", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 七连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 七连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.37f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.58f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	private IEnumerator Cor_正鞭()
	{
		base._Ani.Play("攻击_正鞭", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 正鞭._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 正鞭._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		float num = Vector2.Distance(_Data._Pos_追击目标位置, base._Mono.transform.position);
		float 初速度 = 36f;
		float 减速参数 = 6f;
		if (num > 10f)
		{
			初速度 = 48f;
			减速参数 = 8f;
		}
		else if (num > 20f)
		{
			初速度 = 58f;
			减速参数 = 12f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 初速度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
		base._Rb2D.velocity = _攻击方向 * (初速度 - 减速参数);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
		base._Rb2D.velocity = _攻击方向 * (初速度 - 减速参数 * 2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
		base._Rb2D.velocity = _攻击方向 * 8f;
		if (Random.value < 0.65f)
		{
			Vector2 pos毒爆 = base._Mono.transform.position;
			float dirPos = ((base._Mono.transform.localScale.x > 0f) ? 1 : (-1));
			pos毒爆.x += dirPos * 5f;
			F_生成毒爆(pos毒爆);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			pos毒爆.x += dirPos * 2f;
			F_生成毒爆(pos毒爆);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			pos毒爆.x += dirPos * 2f;
			F_生成毒爆(pos毒爆);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		}
		else
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		}
	}

	private IEnumerator Cor_侧侧鞭()
	{
		base._Ani.Play("攻击_侧侧鞭", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 侧鞭._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 侧鞭._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		float num = Vector2.Distance(_Data._Pos_追击目标位置, base._Mono.transform.position);
		float 初速度2 = 36f;
		float 减速参数2 = 6f;
		if (num > 10f)
		{
			初速度2 = 48f;
			减速参数2 = 8f;
		}
		else if (num > 20f)
		{
			初速度2 = 58f;
			减速参数2 = 12f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 初速度2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2 * 2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.31f));
		base._Rb2D.velocity = _攻击方向 * 8f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.34f));
		num = Vector2.Distance(_Data._Pos_追击目标位置, base._Mono.transform.position);
		if (num > 10f)
		{
			初速度2 = 48f;
			减速参数2 = 8f;
		}
		else if (num > 20f)
		{
			初速度2 = 58f;
			减速参数2 = 12f;
		}
		else
		{
			初速度2 = 36f;
			减速参数2 = 6f;
		}
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 初速度2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2 * 2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.31f));
		base._Rb2D.velocity = _攻击方向 * 8f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
	}

	private IEnumerator Cor_侧侧正鞭()
	{
		base._Ani.Play("攻击_侧侧正鞭", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 侧鞭._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 侧鞭._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		float num = Vector2.Distance(_Data._Pos_追击目标位置, base._Mono.transform.position);
		float 初速度2 = 36f;
		float 减速参数2 = 6f;
		if (num > 10f)
		{
			初速度2 = 48f;
			减速参数2 = 8f;
		}
		else if (num > 20f)
		{
			初速度2 = 58f;
			减速参数2 = 12f;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 初速度2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2 * 2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.31f));
		base._Rb2D.velocity = _攻击方向 * 8f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.34f));
		num = Vector2.Distance(_Data._Pos_追击目标位置, base._Mono.transform.position);
		初速度2 = 36f;
		减速参数2 = 6f;
		if (num > 10f)
		{
			初速度2 = 48f;
			减速参数2 = 8f;
		}
		else if (num > 20f)
		{
			初速度2 = 58f;
			减速参数2 = 12f;
		}
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 初速度2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2 * 2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.31f));
		base._Rb2D.velocity = _攻击方向 * 8f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		num = Vector2.Distance(_Data._Pos_追击目标位置, base._Mono.transform.position);
		初速度2 = 36f;
		减速参数2 = 6f;
		if (num > 10f)
		{
			初速度2 = 48f;
			减速参数2 = 8f;
		}
		else if (num > 20f)
		{
			初速度2 = 58f;
			减速参数2 = 12f;
		}
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 正鞭._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 正鞭._击退力度;
		base._Rb2D.velocity = _攻击方向 * 初速度2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度2 - 减速参数2 * 2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
		base._Rb2D.velocity = _攻击方向 * 8f;
		if (Random.value < 0.65f)
		{
			Vector2 pos毒爆 = base._Mono.transform.position;
			float dirPos = ((base._Mono.transform.localScale.x > 0f) ? 1 : (-1));
			pos毒爆.x += dirPos * 5f;
			F_生成毒爆(pos毒爆);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			pos毒爆.x += dirPos * 2f;
			F_生成毒爆(pos毒爆);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			pos毒爆.x += dirPos * 2f;
			F_生成毒爆(pos毒爆);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		}
		else
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		}
	}

	private void F_生成毒爆(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_毒敌大王._C_毒液_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 毒爆._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 毒爆._击退力度;
	}

	private IEnumerator Cor_尾后针()
	{
		if (Random.value < 0.8f)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_尾后针();
		}
		清空尾巴 = true;
		base._Ani.Play("攻击_尾后针_新", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 尾后针._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 尾后针._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		_Mono_毒敌大王._Tsf_尾巴_3.localPosition = _Pos_尾巴_3;
		_Mono_毒敌大王._Tsf_尾巴_4.localPosition = _Pos_尾巴_4;
		_Mono_毒敌大王._Tsf_尾巴_5.localPosition = _Pos_尾巴_5;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.32f));
		F_更新攻击方向();
		Vector2 vector = _Mono_毒敌大王.transform.position;
		float num = Vector2.Angle(_Mono_毒敌大王.transform.right, _攻击方向);
		if (num > 90f)
		{
			num = 180f - num;
		}
		num = Mathf.Clamp(num, 0f, 45f);
		float 旋转角度 = ((!(vector.y > _目标位置.y)) ? Mathf.Lerp(0f, 10f, num / 45f) : Mathf.Lerp(0f, -10f, num / 45f));
		_Mono_毒敌大王._毒敌大王_尾巴.F_应用旋转角度(旋转角度);
		Vector2 vector2 = _Mono_毒敌大王._Tsf_尾巴末端节点.position;
		Vector2 vector3 = _Mono_毒敌大王.transform.InverseTransformPoint(vector2);
		List<Vector2> points = new List<Vector2>
		{
			new Vector2(0f, 0f),
			vector3
		};
		_Mono_毒敌大王._Edge_尾巴攻击区域.SetPoints(points);
		_Mono_毒敌大王._LR_尾巴_影子.SetPosition(1, vector3);
		if (Random.value < 0.75f)
		{
			Vector2 vector4 = vector2 - vector;
			float magnitude = vector4.magnitude;
			Vector2 vector5 = vector4 / magnitude;
			for (int i = 1; i < 4; i++)
			{
				F_生成毒爆(vector + vector5 * i * (magnitude / 3f));
			}
		}
		Quaternion localRotation = _Mono_毒敌大王._Tsf_尾巴_3.localRotation;
		Quaternion localRotation2 = _Mono_毒敌大王._Tsf_尾巴_4.localRotation;
		Quaternion localRotation3 = _Mono_毒敌大王._Tsf_尾巴_5.localRotation;
		_Mono_毒敌大王._Tsf_尾巴_3.localPosition = Vector2.zero;
		_Mono_毒敌大王._Tsf_尾巴_4.localPosition = Vector2.zero;
		_Mono_毒敌大王._Tsf_尾巴_5.localPosition = Vector2.zero;
		_Mono_毒敌大王._Tsf_尾巴_3.localRotation = Quaternion.identity;
		_Mono_毒敌大王._Tsf_尾巴_4.localRotation = Quaternion.identity;
		_Mono_毒敌大王._Tsf_尾巴_5.localRotation = Quaternion.identity;
		_Mono_毒敌大王._Tsf_尾巴_3.DOLocalMove(_Pos_尾巴_3, 0.1f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_4.DOLocalMove(_Pos_尾巴_4, 0.1f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_5.DOLocalMove(_Pos_尾巴_5, 0.1f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_3.DOLocalRotateQuaternion(localRotation, 0.1f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_4.DOLocalRotateQuaternion(localRotation2, 0.1f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_5.DOLocalRotateQuaternion(localRotation3, 0.1f).SetEase(Ease.OutCubic);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
		_Mono_毒敌大王._Tsf_尾巴_3.DOLocalMove(Vector2.zero, 0.08f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_4.DOLocalMove(Vector2.zero, 0.08f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_5.DOLocalMove(Vector2.zero, 0.08f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_3.DOLocalRotateQuaternion(Quaternion.identity, 0.08f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_4.DOLocalRotateQuaternion(Quaternion.identity, 0.08f).SetEase(Ease.OutCubic);
		_Mono_毒敌大王._Tsf_尾巴_5.DOLocalRotateQuaternion(Quaternion.identity, 0.08f).SetEase(Ease.OutCubic);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.06f));
	}

	private IEnumerator Cor_钳地手()
	{
		base._Ani.Play("攻击_钳地手_奔跑", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 钳地手._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 钳地手._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		_跟随移动 = true;
		for (int i = 0; i < 15; i++)
		{
			F_更新攻击方向();
			_MoveDirSpeed = _攻击方向 * 16f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
			if (i > 3 && Vector2.Distance(base._Mono.transform.position, _Data._Pos_追击目标位置) < 5f)
			{
				break;
			}
		}
		_跟随移动 = false;
		F_更新攻击方向();
		base._Ani.Play("攻击_钳地手_出手", 0, 0f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = _攻击方向 * 28f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	private IEnumerator Cor_入土()
	{
		base._Ani.Play("攻击_入土", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 入土._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 入土._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		float num = Vector2.Distance(_Data._Pos_追击目标位置, base._Mono.transform.position);
		float 初速度 = 36f;
		float 减速参数 = 6f;
		if (num > 10f)
		{
			初速度 = 48f;
			减速参数 = 8f;
		}
		else if (num > 20f)
		{
			初速度 = 58f;
			减速参数 = 12f;
		}
		base._Rb2D.velocity = _攻击方向 * 初速度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度 - 减速参数);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * (初速度 - 减速参数 * 2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		base._Rb2D.bodyType = RigidbodyType2D.Kinematic;
		base._Rb2D.velocity = Vector2.zero;
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		float 等待时间 = Random.Range(0.6f, 1.6f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(等待时间 / 2f));
		F_更新攻击方向();
		base._Mono组件.transform.position = _Data._Pos_追击目标位置;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(等待时间 / 2f));
		base._Rb2D.bodyType = RigidbodyType2D.Dynamic;
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
		base._Ani.Play("攻击_出土", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 出土._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 出土._击退力度;
		_跟随移动 = true;
		for (int i = 0; i < 6; i++)
		{
			F_更新攻击方向();
			_MoveDirSpeed = _攻击方向 * 18f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		_跟随移动 = false;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 28f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.9f));
	}
}
