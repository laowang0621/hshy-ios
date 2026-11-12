using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyState_亢金龙_攻击 : EnemyState_Boss
{
	private EnemyAI_亢金龙 _Mono_亢金龙;

	private DataInfo_Enemy招式数值 电疗针;

	private DataInfo_Enemy招式数值 小落雷;

	private DataInfo_Enemy招式数值 雷球;

	private DataInfo_Enemy招式数值 电场;

	private DataInfo_Enemy招式数值 大落雷;

	private DataInfo_Enemy招式数值 三连1;

	private DataInfo_Enemy招式数值 三连2;

	private DataInfo_Enemy招式数值 三连3;

	private DataInfo_Enemy招式数值 X;

	private DataInfo_Enemy招式数值 自爆;

	private DataInfo_Enemy招式数值 电圈;

	private DataInfo_Enemy招式数值 龙车;

	private DataInfo_Enemy招式数值 二连爪;

	private DataInfo_Enemy招式数值 龙爪;

	private DataInfo_Enemy招式数值 龙尾;

	private DataInfo_Enemy招式数值 龙息;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private float _EnterDrag;

	private int _攻击序号 = 10;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_亢金龙 = (EnemyAI_亢金龙)data._Mono;
		电疗针 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7001);
		小落雷 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7002);
		雷球 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7003);
		电场 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7004);
		大落雷 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7005);
		三连1 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7006);
		三连2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7007);
		三连3 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7008);
		X = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7009);
		自爆 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7010);
		电圈 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7011);
		龙车 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7101);
		二连爪 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7102);
		龙爪 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7103);
		龙尾 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7104);
		龙息 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7105);
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
		_Cor_攻击 = base._Mono组件.StartCoroutine(F_攻击());
		base._Rb2D.bodyType = RigidbodyType2D.Dynamic;
	}

	public override void OnExit()
	{
		_Data._Mono组件.StopAllCoroutines();
		base._Rb2D.bodyType = RigidbodyType2D.Dynamic;
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
		base._Rb2D.drag = _EnterDrag;
		base._Mono组件._SR_Model.SetAlpha(1f);
		base._Mono组件._攻击范围.enabled = true;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	private IEnumerator F_攻击()
	{
		Vector2 vector = _Data._Pos_追击目标位置 - base._Mono.transform.position;
		float magnitude = vector.magnitude;
		Vector2 direction = vector / magnitude;
		if (magnitude > 20f)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_龙车());
		}
		else if ((bool)Physics2D.Raycast(base._Mono.transform.position, direction, magnitude, 2048))
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_龙车());
		}
		else
		{
			_攻击序号 = Random.Range(1, 6);
			switch (_攻击序号)
			{
			case 1:
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_二连());
				break;
			case 2:
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_龙爪());
				break;
			case 3:
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_龙尾());
				break;
			case 4:
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_龙息());
				break;
			case 5:
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_龙车());
				break;
			default:
				_攻击序号 = 1;
				_攻击序号--;
				break;
			}
			_攻击序号++;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_三连大落雷_old()
	{
		F_生成大落雷(_Mono_亢金龙._Tsf_三连大落雷[0].position);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		F_生成大落雷(_Mono_亢金龙._Tsf_三连大落雷[1].position);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		F_生成大落雷(_Mono_亢金龙._Tsf_三连大落雷[2].position);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private void F_生成大落雷(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_亢金龙._C_大落雷_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 大落雷._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 大落雷._击退力度;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_大落雷();
	}

	private void F_生成电圈(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_亢金龙._C_电圈_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 电圈._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 电圈._击退力度;
		c_攻击判断._是地面伤害 = true;
	}

	private IEnumerator Cor_三连大落雷()
	{
		foreach (Transform item in _Mono_亢金龙._Tsf_三连1_大落雷)
		{
			F_生成大落雷(item.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		foreach (Transform item2 in _Mono_亢金龙._Tsf_三连2_大落雷)
		{
			F_生成大落雷(item2.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		foreach (Transform item3 in _Mono_亢金龙._Tsf_三连3_大落雷)
		{
			F_生成大落雷(item3.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private IEnumerator Cor_生成电圈和四角落雷()
	{
		yield return null;
		F_生成电圈(_Mono_亢金龙._Tsf_攻击位置_方.position);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		List<Vector2> poss = new List<Vector2>();
		foreach (Transform item in _Mono_亢金龙._Tsf_四角)
		{
			poss.Add(item.position);
		}
		foreach (Vector2 item2 in poss)
		{
			F_生成大落雷(item2);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		foreach (Vector2 item3 in poss)
		{
			F_生成电圈(item3);
		}
	}

	public void F_生成特效攻击()
	{
		if (Random.value < 0.1f)
		{
			_Data._Mono组件.StartCoroutine(Cor_三连大落雷());
		}
		else
		{
			_Data._Mono组件.StartCoroutine(Cor_生成电圈和四角落雷());
		}
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_攻击_二连()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_二连");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连爪._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连爪._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		if (_Mono_亢金龙._解锁特效攻击)
		{
			F_生成大落雷(_Mono_亢金龙._Tsf_三连大落雷[0].position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		if (_Mono_亢金龙._解锁特效攻击)
		{
			Vector2 pos = _Mono_亢金龙._Tsf_三连大落雷[0].position;
			F_生成大落雷(pos);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
			F_生成电圈(pos);
		}
		else
		{
			F_生成大落雷(_Mono_亢金龙._Tsf_三连大落雷[0].position);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private IEnumerator Cor_攻击_龙爪()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_龙爪");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 龙爪._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 龙爪._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.9f));
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		if (_Mono_亢金龙._解锁特效攻击)
		{
			F_生成特效攻击();
		}
		else
		{
			F_生成大落雷(_Mono_亢金龙._Tsf_三连大落雷[0].position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.95f));
	}

	private IEnumerator Cor_攻击_龙尾()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_龙尾");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 龙尾._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 龙尾._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.8f));
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		if (_Mono_亢金龙._解锁特效攻击)
		{
			F_生成特效攻击();
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.95f));
	}

	private IEnumerator Cor_攻击_龙息()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_龙息");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 龙息._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 龙息._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.5f));
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		if (_Mono_亢金龙._解锁特效攻击)
		{
			_Data._Mono组件.StartCoroutine(Cor_三连大落雷());
		}
		else
		{
			_Data._Mono组件.StartCoroutine(Cor_三连大落雷_old());
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.8f));
	}

	private IEnumerator Cor_攻击_龙车()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 龙车._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 龙车._击退力度;
		F_更新攻击方向();
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("身法_电花变小消失", 0, 0f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.05f));
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		base._Rb2D.bodyType = RigidbodyType2D.Kinematic;
		base._Mono组件.transform.position = Vector2.zero;
		Vector2 vector = base._Mono组件.transform.position;
		Vector2 vector2 = _Data._Pos_追击目标位置;
		Vector2 dir_目标后面方向 = ((vector.x > vector2.x) ? Vector2.left : Vector2.right);
		float num = Random.Range(30f, 50f);
		base._Mono组件.transform.position = vector2 + dir_目标后面方向 * num;
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector2((_攻击方向.x > 0f) ? 1 : (-1), 0f) * 35f;
		base._Ani.Play("攻击_龙车");
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._SR_Model.DOFade(1f, 0.75f);
		if (_Mono_亢金龙._解锁特效攻击)
		{
			for (int i = 0; i < 10; i++)
			{
				F_生成电圈(_Mono_亢金龙._Tsf_攻击位置_方.position);
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.21f));
			}
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				F_生成电圈(_Mono_亢金龙._Tsf_攻击位置_方.position);
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.66f));
			}
		}
		base._Mono组件._SR_Model.DOFade(0f, 0.75f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		base._Mono组件._攻击范围.enabled = false;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Mono组件._SR_Model.SetAlpha(1f);
		base._Ani.Play("身法_小到大出现", 0, 0f);
		base._Rb2D.velocity = Vector2.zero;
		vector2 = _Data._Pos_追击目标位置;
		Vector2 insideUnitCircle = Random.insideUnitCircle;
		num = Random.Range(4f, 8f);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2, insideUnitCircle, num, 2048);
		if ((bool)raycastHit2D)
		{
			base._Mono组件.transform.position = raycastHit2D.point;
		}
		else
		{
			base._Mono组件.transform.position = vector2 + dir_目标后面方向 * num;
		}
		base._Rb2D.bodyType = RigidbodyType2D.Dynamic;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.8f));
	}
}
