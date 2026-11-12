using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyState_黄眉_攻击 : EnemyState_Boss
{
	private EnemyAI_黄眉 _Mono_黄眉;

	private DataInfo_Enemy招式数值 小落雷;

	private DataInfo_Enemy招式数值 大落雷;

	private DataInfo_Enemy招式数值 电圈;

	private DataInfo_Enemy招式数值 横扫1;

	private DataInfo_Enemy招式数值 横扫2;

	private DataInfo_Enemy招式数值 戳锤;

	private DataInfo_Enemy招式数值 下砸;

	private DataInfo_Enemy招式数值 插地;

	private DataInfo_Enemy招式数值 跺脚;

	private DataInfo_Enemy招式数值 挥锤;

	private DataInfo_Enemy招式数值 自爆;

	private DataInfo_Enemy招式数值 特效_地裂;

	private DataInfo_Enemy招式数值 特效_自爆;

	private DataInfo_Enemy招式数值 特效_雷暴1;

	private DataInfo_Enemy招式数值 特效_雷暴2;

	private DataInfo_Enemy招式数值 特效_雷暴3;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private float _EnterDrag;

	private int _攻击序号 = 1;

	private List<int> _出招序号表 = new List<int> { 1, 2, 3, 4, 5, 51, 7, 8, 9 };

	private List<int> _出招序号表_2 = new List<int> { 1, 2, 3, 4, 5, 51, 6, 7, 8, 9 };

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_黄眉 = (EnemyAI_黄眉)data._Mono;
		小落雷 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7002);
		大落雷 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7005);
		电圈 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7011);
		横扫1 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11001);
		横扫2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11002);
		戳锤 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11003);
		下砸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11004);
		插地 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11005);
		跺脚 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11006);
		挥锤 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11007);
		自爆 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11008);
		特效_地裂 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11009);
		特效_自爆 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11010);
		特效_雷暴1 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11011);
		特效_雷暴2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11012);
		特效_雷暴3 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(11013);
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
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
			_Cor_攻击 = null;
		}
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
		_Mono_黄眉._无敌 = false;
		base._Mono组件._SR_Model.DOKill();
		base._Mono组件._SR_Model.SetAlpha(1f);
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	private void F_生成小落雷(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_黄眉._C_小落雷_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 小落雷._伤害系数 * _Data._Atk;
		c_攻击判断._是地面伤害 = false;
		c_攻击判断._击退力度 = 小落雷._击退力度;
	}

	private void F_生成大落雷(Vector2 pos, bool _带音效 = true)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_黄眉._C_大落雷_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 大落雷._伤害系数 * _Data._Atk;
		c_攻击判断._是地面伤害 = false;
		c_攻击判断._击退力度 = 大落雷._击退力度;
		if (_带音效)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_大落雷();
		}
	}

	private void F_生成电圈(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_黄眉._C_电圈_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 电圈._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 电圈._击退力度;
		c_攻击判断._是地面伤害 = false;
	}

	private void F_生成右半边电圈(Vector2 pos)
	{
		pos += new Vector2((float)((_攻击方向.x > 0f) ? 1 : (-1)) * 2.5f, 0f);
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_黄眉._C_右半边电圈_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 电圈._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 电圈._击退力度;
		c_攻击判断._是地面伤害 = false;
		c_攻击判断.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private void F_生成地裂(Transform tsf)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_黄眉._C_地裂_Prefab, tsf.position, Quaternion.identity);
		c_攻击判断._伤害 = 特效_地裂._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 特效_地裂._击退力度;
		c_攻击判断._是地面伤害 = false;
		c_攻击判断.transform.localScale = tsf.lossyScale;
	}

	private void F_生成自爆(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_黄眉._C_自爆_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 特效_自爆._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 特效_自爆._击退力度;
		c_攻击判断._是地面伤害 = false;
	}

	private void F_生成雷暴1(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_黄眉._C_雷暴1_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 特效_雷暴1._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 特效_雷暴1._击退力度;
		c_攻击判断._是地面伤害 = false;
	}

	private void F_生成雷暴2(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_黄眉._C_雷暴2_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 特效_雷暴2._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 特效_雷暴2._击退力度;
		c_攻击判断._是地面伤害 = false;
	}

	private void F_生成雷暴3(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_Mono_黄眉._C_雷暴3_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 特效_雷暴3._伤害系数 * _Data._Atk;
		c_攻击判断._击退力度 = 特效_雷暴3._击退力度;
		c_攻击判断._是地面伤害 = false;
	}

	private IEnumerator Cor_小落雷_戳锤(int count)
	{
		for (int i = 0; i < count && i < _Mono_黄眉._Tsf_小落雷_戳锤.Count; i++)
		{
			Vector2 pos = _Mono_黄眉._Tsf_小落雷_戳锤[i].position;
			F_生成大落雷(pos);
			if (i == 2)
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
				F_生成电圈(pos);
			}
			else
			{
				yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
			}
		}
	}

	private IEnumerator F_攻击()
	{
		if (_Mono_黄眉._是二阶段)
		{
			_攻击序号 = _出招序号表_2.GetRandom();
		}
		else
		{
			_攻击序号 = _出招序号表.GetRandom();
		}
		if (_Mono_黄眉._开启固定出招)
		{
			_攻击序号 = _Mono_黄眉._固定出招序号;
		}
		if (_Mono_黄眉._是二阶段 && _Mono_黄眉._Is被定)
		{
			_攻击序号 = 10;
		}
		if (_Mono_黄眉._是二阶段 && !_Mono_黄眉._金身 && Random.value < 0.2f)
		{
			_攻击序号 = 10;
		}
		if (_Mono_黄眉._下次策反猴群 && _Mono_黄眉._List_猴子猴孙 != null)
		{
			bool flag = false;
			foreach (EnemyAI_猴子猴孙 item in _Mono_黄眉._List_猴子猴孙)
			{
				if (item != null)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				_攻击序号 = 11;
			}
		}
		switch (_攻击序号)
		{
		case 1:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_横扫_1_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_横扫_1_2());
			break;
		case 2:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_横扫_2_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_横扫_2_2());
			break;
		case 3:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_横扫_12_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_横扫_12_2());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_横扫_12_3());
			break;
		case 4:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_戳锤_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_戳锤_2());
			break;
		case 5:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下砸_雷暴2_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下砸_雷暴2_2());
			break;
		case 51:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下砸_地裂_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_下砸_地裂_2());
			break;
		case 6:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_插地_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_插地_2());
			break;
		case 7:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_跺脚_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_跺脚_2());
			break;
		case 8:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_挥锤_1());
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_挥锤_2());
			break;
		case 9:
			yield return _Data._Mono组件.StartCoroutine(Cor_攻击_自爆());
			break;
		case 10:
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
			_Mono_黄眉._金身 = true;
			base._Ani.Play("攻击_挥锤_2", 0, 0f);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.95f));
			break;
		case 11:
			base._Ani.Play("攻击_挥锤_2", 0, 0f);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
			foreach (EnemyAI_猴子猴孙 item2 in _Mono_黄眉._List_猴子猴孙)
			{
				if (item2 != null)
				{
					Object.Instantiate(_Mono_黄眉._C_策反Test_Prefab, _Mono_黄眉._Tsf_自爆Tsf_策反特效生成位置.position, Quaternion.identity).F_绑定猴子猴孙(item2);
				}
			}
			_Mono_黄眉._下次策反猴群 = false;
			_Mono_黄眉._List_猴子猴孙 = null;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
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

	private IEnumerator Cor_攻击_瞬移()
	{
		base._Ani.Play("攻击_横扫_1", 0, 0f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.05f));
	}

	private IEnumerator Cor_攻击_横扫_1_1()
	{
		base._Ani.Play("攻击_横扫_1", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 横扫1._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 横扫1._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 40f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
	}

	private IEnumerator Cor_攻击_横扫_1_2()
	{
		if (_Mono_黄眉._是二阶段)
		{
			F_生成右半边电圈(base._Mono.transform.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.56f));
	}

	private IEnumerator Cor_攻击_横扫_2_1()
	{
		base._Ani.Play("攻击_横扫_2", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 横扫2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 横扫2._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 38f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
	}

	private IEnumerator Cor_攻击_横扫_2_2()
	{
		if (_Mono_黄眉._是二阶段)
		{
			F_生成右半边电圈(base._Mono.transform.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.69f));
	}

	private IEnumerator Cor_攻击_横扫_12_1()
	{
		base._Ani.Play("攻击_横扫_12", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 横扫1._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 横扫1._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 40f, _攻击方向.y * 24f);
	}

	private IEnumerator Cor_攻击_横扫_12_2()
	{
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
		if (_Mono_黄眉._是二阶段)
		{
			F_生成右半边电圈(base._Mono.transform.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.64f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 横扫2._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 横扫2._击退力度;
	}

	private IEnumerator Cor_攻击_横扫_12_3()
	{
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 38f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
		if (_Mono_黄眉._是二阶段)
		{
			F_生成右半边电圈(base._Mono.transform.position);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.69f));
	}

	private IEnumerator Cor_攻击_戳锤_1()
	{
		base._Ani.Play("攻击_戳锤", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 戳锤._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 戳锤._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 40f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
	}

	private IEnumerator Cor_攻击_戳锤_2()
	{
		if (_Mono_黄眉._是二阶段)
		{
			_Data._Mono组件.StartCoroutine(Cor_小落雷_戳锤(3));
		}
		else
		{
			_Data._Mono组件.StartCoroutine(Cor_小落雷_戳锤(2));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private IEnumerator Cor_攻击_下砸_雷暴2_1()
	{
		base._Ani.Play("攻击_下砸", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下砸._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 40f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	private IEnumerator Cor_攻击_下砸_雷暴2_2()
	{
		Vector2 pos = _Mono_黄眉._Tsf_下砸_雷暴2.position;
		F_生成雷暴2(pos);
		if (_Mono_黄眉._是二阶段)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
			F_生成电圈(pos);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.85f));
	}

	private IEnumerator Cor_攻击_下砸_地裂_1()
	{
		base._Ani.Play("攻击_下砸", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 下砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 下砸._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 40f, _攻击方向.y * 24f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
	}

	private IEnumerator Cor_攻击_下砸_地裂_2()
	{
		_ = (Vector2)_Mono_黄眉._Tsf_下砸_地裂.position;
		F_生成地裂(_Mono_黄眉._Tsf_下砸_地裂);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.85f));
	}

	private IEnumerator Cor_攻击_插地_1()
	{
		base._Ani.Play("攻击_插地", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 插地._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 插地._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
	}

	private IEnumerator Cor_攻击_插地_2()
	{
		Vector2 pos = _Mono_黄眉._Tsf_插地_雷暴3.position;
		F_生成雷暴3(pos);
		for (int i = 0; i < 3; i++)
		{
			F_生成电圈(pos);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
	}

	private IEnumerator Cor_攻击_跺脚_1()
	{
		base._Ani.Play("攻击_跺脚", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 跺脚._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 跺脚._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		base._Rb2D.velocity = new Vector2(_攻击方向.x * 30f, _攻击方向.y * 30f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.26f));
	}

	private IEnumerator Cor_攻击_跺脚_2()
	{
		Vector2 pos = _Mono_黄眉._Tsf_跺脚_雷暴1.position;
		F_生成雷暴1(pos);
		if (_Mono_黄眉._是二阶段)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
			F_生成电圈(pos);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.7f));
	}

	private IEnumerator Cor_攻击_挥锤_1()
	{
		base._Ani.Play("攻击_挥锤", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 挥锤._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 挥锤._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		F_生成大落雷(_目标位置);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
	}

	private IEnumerator Cor_攻击_挥锤_2()
	{
		F_更新攻击方向();
		Vector2 pos = _目标位置;
		F_生成大落雷(pos);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		if (_Mono_黄眉._是二阶段)
		{
			F_生成电圈(pos);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
	}

	private IEnumerator Cor_攻击_自爆()
	{
		base._Ani.Play("攻击_自爆", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 自爆._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 自爆._击退力度;
		F_更新攻击方向();
		Vector2 pos = _Mono_黄眉._Tsf_自爆.position;
		F_生成自爆(pos);
		int count = 1;
		if (_Mono_黄眉._是二阶段)
		{
			count = 3;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.18f));
		for (int i = 0; i < count; i++)
		{
			F_生成电圈(pos);
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.95f));
	}
}
