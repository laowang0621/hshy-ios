using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyState_亢金星君_攻击 : EnemyState_Boss
{
	private EnemyAI_亢金星君 _Mono_亢金星君;

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

	private DataInfo_Enemy招式数值 飞踢;

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

	private bool _转阶段_2 = true;

	private float _血量百分比_2 = 0.7f;

	private bool _转阶段_3 = true;

	private float _血量百分比_3 = 0.4f;

	private List<int> _出招序号表 = new List<int> { 1, 2, 3, 4 };

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_亢金星君 = (EnemyAI_亢金星君)data._Mono;
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
		飞踢 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7012);
		龙车 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7101);
		二连爪 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7102);
		龙爪 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7103);
		龙尾 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7104);
		龙息 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7105);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		_Cor_攻击 = base._Mono组件.StartCoroutine(F_攻击());
	}

	public override void OnExit()
	{
		_Data._Mono组件.StopAllCoroutines();
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
		_Mono_亢金星君._无敌 = false;
		base._Mono组件._SR_Model.DOKill();
		base._Mono组件._SR_Model.SetAlpha(1f);
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	private IEnumerator F_攻击()
	{
		if (_转阶段_2 && _Data._Curr生命 < _Data._MaxHP * _血量百分比_2)
		{
			_转阶段_2 = false;
			_出招序号表.Add(6);
			_出招序号表.Add(7);
			_出招序号表.Add(8);
			_出招序号表.Add(9);
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_起飞_中心点());
			yield return _Data._Mono组件.StartCoroutine(Cor_转阶段_龙舞());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_原地降落());
			base._Mono.ChangeState(EnemyStateName_Type.站立);
			yield break;
		}
		if (_转阶段_3 && _Data._Curr生命 < _Data._MaxHP * _血量百分比_3)
		{
			_转阶段_3 = false;
			_出招序号表.Add(10);
			_出招序号表.Add(11);
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_起飞_中心点());
			yield return _Data._Mono组件.StartCoroutine(Cor_转阶段_电疗针());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_原地降落());
			base._Mono.ChangeState(EnemyStateName_Type.站立);
			yield break;
		}
		if (Random.value < 0.1f && Vector2.Distance(_Data._Pos_追击目标位置, base._Mono.transform.position) < 2f)
		{
			_攻击序号 = 5;
		}
		else
		{
			_攻击序号 = _出招序号表.GetRandom();
		}
		if (_Mono_亢金星君._开启固定出招)
		{
			_攻击序号 = _Mono_亢金星君._固定出招序号;
		}
		switch (_攻击序号)
		{
		case 1:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_电疗针());
			break;
		case 2:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_小落雷());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_后跳雷球());
			break;
		case 3:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_电场());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_大落雷(0.9f));
			break;
		case 4:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_雷球());
			if (Random.value < 0.5f)
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_1_雷球());
			}
			else
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_攻击_飞踢());
			}
			break;
		case 5:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_3());
			break;
		case 6:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_起飞_中心点());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_召雷());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_雷枪());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_原地降落());
			break;
		case 7:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_电疗针());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_电疗针_2());
			break;
		case 8:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_大落雷(0.65f));
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_龙尾());
			break;
		case 9:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_起飞_中心点());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_自爆());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_原地降落());
			break;
		case 10:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_起飞_远离玩家的左或右侧());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_电之宝库());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_原地降落());
			break;
		case 11:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_雷球());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_三连_1_雷球());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_X());
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

	private void F_生成雷球(Vector2 pos, Vector2 dir)
	{
		C_弹道 cgo = Object.Instantiate(_Mono_亢金星君._C_弹道_雷球_Prefab, pos, Quaternion.identity);
		cgo._伤害 = 雷球._伤害系数 * _Data._Atk;
		cgo._击退力度 = 雷球._击退力度;
		cgo.F_Init(dir);
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小电流();
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(cgo.gameObject, Random.Range(0.33f, 0.55f), delegate
		{
			if (cgo != null && cgo.gameObject != null)
			{
				F_生成电圈(cgo.transform.position);
				Object.Destroy(cgo.gameObject);
			}
		});
	}

	private void F_生成小落雷(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_亢金星君._C_小落雷_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 小落雷._伤害系数 * _Data._Atk;
		c_攻击判断._是地面伤害 = false;
		c_攻击判断._击退力度 = 小落雷._击退力度;
	}

	private void F_生成大落雷(Vector2 pos, bool _带音效 = true)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_亢金星君._C_大落雷_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 大落雷._伤害系数 * _Data._Atk;
		c_攻击判断._是地面伤害 = false;
		c_攻击判断._击退力度 = 大落雷._击退力度;
		if (_带音效)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_大落雷();
		}
	}

	private void F_生成电疗针_直接激发(Vector2 startPos)
	{
		C_弹道_电疗针 c_弹道_电疗针 = Object.Instantiate(_Mono_亢金星君._C_弹道_电疗针_Prefab, startPos, Quaternion.identity);
		c_弹道_电疗针.F_Init(((Vector2)_Data._Pos_追击目标位置 - startPos).normalized, 电疗针._伤害系数 * _Data._Atk);
		c_弹道_电疗针._击退力度 = 电疗针._击退力度;
	}

	private C_弹道_电疗针 F_生成电疗针_仅初始状态(Vector2 startPos)
	{
		C_弹道_电疗针 c_弹道_电疗针 = Object.Instantiate(_Mono_亢金星君._C_弹道_电疗针_Prefab, startPos, Quaternion.identity);
		c_弹道_电疗针._伤害 = 电疗针._伤害系数 * _Data._Atk;
		c_弹道_电疗针._击退力度 = 电疗针._击退力度;
		c_弹道_电疗针.F_Init_初始状态();
		return c_弹道_电疗针;
	}

	private void F_生成电圈(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_亢金星君._C_电圈_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 电圈._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 电圈._击退力度;
		c_攻击判断._是地面伤害 = false;
	}

	private IEnumerator Cor_特效攻击_连环环形落雷()
	{
		F_生成一圈大落雷(_Mono_亢金星君.transform.position, 5f, 6);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		F_生成一圈大落雷(_Mono_亢金星君.transform.position, 9f, 9);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		F_生成一圈大落雷(_Mono_亢金星君.transform.position, 18f, 15);
	}

	private IEnumerator Cor_三连大落雷()
	{
		foreach (Transform item in _Mono_亢金星君._Tsf_三连1_大落雷)
		{
			F_生成大落雷(item.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		foreach (Transform item2 in _Mono_亢金星君._Tsf_三连2_大落雷)
		{
			F_生成大落雷(item2.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		foreach (Transform item3 in _Mono_亢金星君._Tsf_三连3_大落雷)
		{
			F_生成大落雷(item3.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private void F_生成一圈大落雷(Vector2 pos, float 距离, int 数量)
	{
		Vector2 @this = new Vector2(距离, 0f);
		float num = 360f / (float)数量;
		for (int i = 0; i < 数量; i++)
		{
			Vector2 vector = @this.Rotate(num * (float)i);
			vector.y *= 0.6f;
			F_生成大落雷(pos + vector, i % 4 == 0);
		}
	}

	private void F_生成一圈小落雷(Vector2 pos, float 距离, int 数量)
	{
		Vector2 @this = new Vector2(距离, 0f);
		float num = 360f / (float)数量;
		for (int i = 0; i < 数量; i++)
		{
			Vector2 vector = @this.Rotate(num * (float)i);
			vector.y *= 0.6f;
			F_生成小落雷(pos + vector);
		}
	}

	private IEnumerator Cor_攻击_电疗针()
	{
		base._Ani.Play("攻击_电疗针");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		MyLog.LogError(">>>>攻击_电疗针");
		int 电疗针数量 = 4;
		for (int i = 0; i < 电疗针数量; i++)
		{
			Vector2 startPos = _Mono_亢金星君._List_电疗针生成位置[i].position;
			F_生成电疗针_直接激发(startPos);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
	}

	private IEnumerator Cor_攻击_电疗针_2()
	{
		base._Ani.Play("攻击_电疗针_2");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		MyLog.LogError(">>>>攻击_电疗针");
		int num = 3;
		for (int i = 0; i < num; i++)
		{
			int index = i;
			Vector2 startPos = _Mono_亢金星君._List_电疗针2生成位置[index].position;
			F_生成电疗针_直接激发(startPos);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
	}

	private IEnumerator Cor_攻击_小落雷()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_小落雷");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_生成小落雷(_Data._Pos_追击目标位置);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
	}

	private IEnumerator Cor_攻击_后跳雷球()
	{
		base._Ani.Play("攻击_后跳雷球");
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * -10f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * -15f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * -20f;
		F_生成雷球(_Mono_亢金星君.Tsf_雷球.position, ((Vector2)(_Data._Pos_追击目标位置 - _Mono_亢金星君.Tsf_后跳雷球.position)).normalized);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.71f));
	}

	private IEnumerator Cor_攻击_电场()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 电场._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 电场._击退力度;
		base._Ani.Play("攻击_电场");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		_Mono_亢金星君._无敌 = true;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		_Mono_亢金星君._无敌 = false;
	}

	private IEnumerator Cor_攻击_大落雷(float time)
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_小落雷");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_生成大落雷(_Data._Pos_追击目标位置);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time - 0.33f));
	}

	private IEnumerator Cor_攻击_雷球()
	{
		base._Ani.Play("攻击_雷球");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.53f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * -8f;
		F_生成雷球(_Mono_亢金星君.Tsf_雷球.position, ((Vector2)(_Data._Pos_追击目标位置 - _Mono_亢金星君.Tsf_雷球.position)).normalized);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
	}

	private IEnumerator Cor_攻击_三连_1_雷球()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 三连1._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 三连1._击退力度;
		base._Mono组件._SR_Model.DOFade(0f, 0.2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Mono组件._SR_Model.DOFade(1f, 0.2f);
		base._Rb2D.velocity = Vector2.zero;
		Vector2 vector = base._Mono组件.transform.position;
		Vector2 vector2 = _Data._Pos_追击目标位置;
		Vector2 vector3 = ((vector.x > vector2.x) ? Vector2.left : Vector2.right);
		float num = Random.Range(3f, 5f);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2, vector3, num, 2048);
		if ((bool)raycastHit2D)
		{
			base._Mono组件.transform.position = raycastHit2D.point;
		}
		else
		{
			base._Mono组件.transform.position = vector2 + vector3 * num;
		}
		base._Ani.Play("攻击_三连_1");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.575f));
	}

	private IEnumerator Cor_攻击_飞踢()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 飞踢._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 飞踢._击退力度;
		base._Mono组件._SR_Model.DOFade(0f, 0.2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		base._Mono组件._SR_Model.DOFade(1f, 0.2f);
		base._Rb2D.velocity = Vector2.zero;
		Vector2 vector = base._Mono组件.transform.position;
		Vector2 vector2 = _Data._Pos_追击目标位置;
		Vector2 vector3 = ((vector.x < vector2.x) ? Vector2.left : Vector2.right);
		float num = Random.Range(5f, 8f);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2, vector3, num, 2048);
		if ((bool)raycastHit2D)
		{
			base._Mono组件.transform.position = raycastHit2D.point;
		}
		else
		{
			base._Mono组件.transform.position = vector2 + vector3 * num;
		}
		yield return new WaitForEndOfFrame();
		base._Ani.Play("攻击_飞踢");
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private IEnumerator Cor_攻击_三连_1()
	{
		base._Ani.Play("攻击_三连_1");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 三连1._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 三连1._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.475f));
	}

	private IEnumerator Cor_攻击_三连_2()
	{
		base._Ani.Play("攻击_三连_2");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 三连2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 三连2._击退力度;
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
	}

	private IEnumerator Cor_攻击_X()
	{
		base._Ani.Play("攻击_X");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * X._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = X._击退力度;
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 30f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_生成一圈小落雷(_Mono_亢金星君.transform.position, 5f, 6);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	private IEnumerator Cor_攻击_三连_3()
	{
		base._Ani.Play("攻击_三连_3");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 三连3._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 三连3._击退力度;
		F_更新攻击方向();
		base._Rb2D.velocity = new Vector3((!(_攻击方向.x > 0f)) ? 1 : (-1), 1f, 1f) * 8f;
		_Data._Mono组件.StartCoroutine(Cor_三连大落雷());
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
	}

	private IEnumerator Cor_攻击_起飞_中心点()
	{
		base._Ani.Play("攻击_起飞");
		Vector2 vector = new Vector2(0f, -6f);
		Vector2 vector2 = _Mono_亢金星君.transform.position;
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		float num = (vector - vector2).magnitude / 10f;
		_Mono_亢金星君.transform.DOMove(vector, num);
		if (num > 0.75f)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(num));
		}
		else
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		}
	}

	private IEnumerator Cor_攻击_起飞_远离玩家的左或右侧()
	{
		base._Ani.Play("攻击_起飞");
		new Vector2(0f, -6f);
		Vector2 vector = ((!(_Data._Pos_追击目标位置.x < 0f)) ? new Vector2(-15f, -6f) : new Vector2(15f, -6f));
		Vector2 vector2 = _Mono_亢金星君.transform.position;
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		float num = (vector - vector2).magnitude / 16f;
		_Mono_亢金星君.transform.DOMove(vector, num);
		if (num > 0.75f)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(num));
		}
		else
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		}
	}

	private IEnumerator Cor_攻击_起飞_原地()
	{
		base._Ani.Play("攻击_起飞");
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
	}

	private IEnumerator Cor_攻击_原地降落()
	{
		base._Ani.Play("攻击_降落");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.77f));
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
	}

	private IEnumerator Cor_攻击_召雷()
	{
		base._Ani.Play("攻击_召雷");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		for (int i = 0; i < 3; i++)
		{
			F_生成小落雷(_Data._Pos_追击目标位置);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		}
		F_更新攻击方向();
		F_生成电疗针_直接激发(_Mono_亢金星君.Tsf_召雷.position);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.725f));
	}

	private IEnumerator Cor_攻击_雷枪()
	{
		base._Ani.Play("攻击_雷枪");
		F_更新攻击方向();
		Vector2 vector = _Data._Pos_追击目标位置;
		if (vector.y > base._Mono.transform.position.y)
		{
			vector.y = base._Mono.transform.position.y;
		}
		Vector2 vector2 = _Mono_亢金星君.Tsf_雷枪.position;
		Vector2 vector3 = vector2 - vector;
		float magnitude = vector3.magnitude;
		Vector2 dir_雷枪 = vector3 / magnitude;
		C_亢金_雷枪 雷枪 = Object.Instantiate(_Mono_亢金星君._C_亢金_雷枪, vector, Quaternion.identity);
		雷枪.F_Init_生成(vector2, dir_雷枪, magnitude);
		雷枪.F_设置伤害(base._Mono._Data._Atk * 电疗针._伤害系数, 电疗针._伤害系数, base._Mono._Data._Atk * 电场._伤害系数, 电场._伤害系数);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		雷枪.F_发射();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.725f));
	}

	private IEnumerator Cor_攻击_龙尾()
	{
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		base._Mono组件._SR_Model.DOFade(0f, 0.23f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		base._Ani.Play("攻击_龙尾");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 龙尾._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 龙尾._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		Vector2 vector = _Mono_亢金星君.transform.position;
		Vector2 vector2 = _Data._Pos_追击目标位置;
		Vector2 vector3 = ((vector.x > vector2.x) ? Vector2.left : Vector2.right);
		float num = Random.Range(4f, 6.5f);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2, vector3, num, 2048);
		if ((bool)raycastHit2D)
		{
			_Mono_亢金星君.transform.position = raycastHit2D.point;
		}
		else
		{
			_Mono_亢金星君.transform.position = vector2 + vector3 * num;
		}
		yield return null;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		base._Rb2D.velocity = _攻击方向 * 45f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		_Data._Mono组件.StartCoroutine(Cor_生成电圈和四角落雷());
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.225f));
		base._Mono组件._SR_Model.DOFade(1f, 0.28f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
	}

	private IEnumerator Cor_生成电圈和四角落雷()
	{
		yield return null;
		F_生成电圈(_Mono_亢金星君._Tsf_攻击位置_方.position);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		List<Vector2> poss = new List<Vector2>();
		foreach (Transform item in _Mono_亢金星君._Tsf_四角)
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

	private IEnumerator Cor_攻击_自爆()
	{
		base._Ani.Play("攻击_自爆");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 自爆._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 自爆._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.84f));
		_Data._Mono组件.StartCoroutine(Cor_特效攻击_连环环形落雷());
		F_生成电圈(base._Mono.transform.position);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.82f));
	}

	private IEnumerator Cor_转阶段_龙舞()
	{
		base._Mono组件._SR_Model.DOFade(0f, 0.2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.21f));
		F_更新攻击方向();
		base._Mono组件._SR_Model.DOFade(1f, 0.2f);
		base._Ani.Play("转阶段_龙舞");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.41f));
		for (int i = 0; i < 30; i++)
		{
			if (i % 8 == 0)
			{
				F_生成大落雷(_Data._Pos_追击目标位置);
			}
			else
			{
				Vector2 vector = _Data._Pos_追击目标位置;
				Vector2 vector2 = Random.insideUnitCircle * Random.Range(6, 31);
				vector2.y *= 0.45f;
				if (Random.value < 0.5f)
				{
					vector2.x *= -1f;
				}
				if (Random.value < 0.5f)
				{
					vector2.x *= -1f;
				}
				Vector2 pos = vector + vector2;
				F_生成大落雷(pos);
			}
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(Random.Range(1.2f, 1.8f) / 4f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
	}

	private IEnumerator Cor_转阶段_电疗针()
	{
		base._Ani.Play("转阶段_电疗针");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		MyLog.LogError(">>>>攻击_电疗针");
		int num = 5;
		for (int i = 0; i < num; i++)
		{
			int index = i;
			Vector2 startPos = _Mono_亢金星君._List_电疗针转阶段生成位置[index].position;
			C_弹道_电疗针 cgo = F_生成电疗针_仅初始状态(startPos);
			if (i < 3)
			{
				_Data._Mono组件.StartCoroutine(Cor_转阶段_电疗针_单个控制(cgo, 0.35f));
			}
			else
			{
				_Data._Mono组件.StartCoroutine(Cor_转阶段_电疗针_单个控制(cgo, 0.95f));
			}
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.6f));
		DOTween.To(() => 1f, delegate(float x)
		{
			_Mono_亢金星君._2D全局光.intensity = x;
		}, 0.383f, 2f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		F_生成电圈(base._Mono.transform.position);
		yield return _Data._Mono组件.StartCoroutine(Cor_特效攻击_连环环形落雷());
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
	}

	private IEnumerator Cor_转阶段_电疗针_单个控制(C_弹道_电疗针 cgo, float 延迟发射)
	{
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(延迟发射));
		Vector2 normalized = ((Vector2)_Data._Pos_追击目标位置 - (Vector2)cgo.transform.position).normalized;
		cgo.F_Init_第一次瞄准(normalized);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		yield return new WaitForEndOfFrame();
		normalized = ((Vector2)_Data._Pos_追击目标位置 - (Vector2)cgo.transform.position).normalized;
		cgo.F_Init_发射(normalized, base._Mono组件._C_攻击判断._伤害);
	}

	private IEnumerator Cor_攻击_电之宝库()
	{
		F_更新攻击方向();
		base._Ani.Play("攻击_电之宝库");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 电疗针._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 电疗针._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		Vector2 pos_电之宝库 = _Mono_亢金星君.Tsf_电之宝库.position;
		Vector2 size = _Mono_亢金星君.Tsf_电之宝库.localScale / 2f;
		Vector2 dir = ((Vector2)_Data._Pos_追击目标位置 - pos_电之宝库).normalized;
		for (int i = 0; i < 80; i++)
		{
			Vector2 insideUnitCircle = Random.insideUnitCircle;
			Vector2 vector = pos_电之宝库 + insideUnitCircle * size;
			Object.Instantiate(_Mono_亢金星君._C_弹道_电疗针_Prefab, vector, Quaternion.identity).F_Init_直接转向_直接发射(dir, base._Mono组件._C_攻击判断._伤害);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(Random.Range(0.025f, 0.1f)));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
	}
}
