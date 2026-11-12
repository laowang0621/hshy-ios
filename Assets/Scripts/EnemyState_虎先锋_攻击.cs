using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_虎先锋_攻击 : EnemyState_Boss
{
	private EnemyAI_虎先锋 _Mono_虎先锋;

	private DataInfo_Enemy招式数值 跳砸;

	private DataInfo_Enemy招式数值 农夫三拳;

	private DataInfo_Enemy招式数值 突刺拔刀斩;

	private DataInfo_Enemy招式数值 拔刀横扫;

	private DataInfo_Enemy招式数值 虎啸;

	private DataInfo_Enemy招式数值 铜头铁臂_反击;

	private DataInfo_Enemy招式数值 拔刀三连;

	private DataInfo_Enemy招式数值 剑气三连;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private float _EnterDrag;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_虎先锋 = (EnemyAI_虎先锋)data._Mono;
		跳砸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(4001);
		农夫三拳 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(4002);
		突刺拔刀斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(4003);
		拔刀横扫 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(4004);
		虎啸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(4005);
		铜头铁臂_反击 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(4006);
		拔刀三连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(4007);
		剑气三连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(4008);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		_Mono_虎先锋._Is铜头铁臂 = false;
		F_攻击();
	}

	public override void OnExit()
	{
		_Mono_虎先锋._Is铜头铁臂 = false;
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
			_Cor_攻击 = null;
		}
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	private void F_攻击()
	{
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		Vector2 a = base._Mono.transform.position;
		_目标位置 = _Data._Pos_追击目标位置;
		if (Vector2.Distance(a, _目标位置) < 10f)
		{
			switch (Random.Range(0, 5))
			{
			case 0:
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_农夫三拳());
				break;
			case 1:
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_拔刀横扫());
				break;
			case 2:
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_拔刀三连());
				break;
			case 3:
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_铜头铁臂());
				break;
			case 4:
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_虎啸());
				break;
			default:
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_农夫三拳());
				break;
			}
		}
		else if (Mathf.Abs(a.y - _目标位置.y) < 3f && Random.value < 0.466f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_剑气三连());
		}
		else if (Random.value < 0.65f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_跳砸());
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_突刺拔刀斩());
		}
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_农夫三拳()
	{
		base._Ani.Play("农夫三拳");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 农夫三拳._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 农夫三拳._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.85f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_剑气三连()
	{
		base._Ani.Play("剑气三连");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 剑气三连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 剑气三连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		C_弹道 _弹道Prefab = ((Random.value < 0.5f) ? ((C_弹道)_Mono_虎先锋._C_弹道_血龙卷Prefab) : ((C_弹道)_Data._Mono组件._C_弹道_剑气Prefab));
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 15f;
		F_发射弓箭(_弹道Prefab);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.8f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 15f;
		F_发射弓箭(_弹道Prefab);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		F_发射弓箭(_弹道Prefab);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private void F_发射弓箭(C_弹道 prefab)
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		float num = Mathf.Abs(_攻击方向.x);
		float num2 = Mathf.Abs(_攻击方向.y);
		Vector2 dir = ((num2 < 0.33f * num) ? _攻击方向 : new Vector2(_攻击方向.x, _攻击方向.y / num2 * 0.33f * num).normalized);
		Object.Instantiate(prefab, base._Mono.transform.position, Quaternion.identity).F_Init(dir, base._Mono组件._C_攻击判断._伤害);
	}

	private IEnumerator Cor_拔刀三连()
	{
		base._Ani.Play("拔刀三连");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 拔刀三连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 拔刀三连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		F_更新攻击方向();
		yield return null;
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		F_更新攻击方向();
		yield return null;
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.9f));
		F_更新攻击方向();
		yield return null;
		float _攻击Timer = 0f;
		while (_攻击Timer < 0.1f)
		{
			if (!base._Mono._Is被定)
			{
				base._Rb2D.velocity = _攻击方向 * 48f;
				_攻击Timer += Time.deltaTime;
			}
			yield return null;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.73f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_拔刀横扫()
	{
		base._Ani.Play("拔刀横扫");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 拔刀横扫._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 拔刀横扫._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.75f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.325f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_跳砸()
	{
		base._Ani.Play("跳砸");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 跳砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 跳砸._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 20f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.17f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 20f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.17f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 20f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.17f));
		base._Rb2D.velocity = _攻击方向 * 25f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.51f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_突刺拔刀斩()
	{
		base._Ani.Play("突刺拔刀斩");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 突刺拔刀斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 突刺拔刀斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		base._Rb2D.drag = 0f;
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 70f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		base._Rb2D.drag = _EnterDrag;
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_虎啸()
	{
		base._Ani.Play("虎啸");
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_虎先锋咆哮();
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 虎啸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 虎啸._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1f));
		base._Ani.Play("直拳");
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		float _攻击Timer = 0f;
		while (_攻击Timer < 0.1f)
		{
			if (!base._Mono._Is被定)
			{
				base._Rb2D.velocity = _攻击方向 * 65f;
				_攻击Timer += Time.deltaTime;
			}
			yield return null;
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_铜头铁臂()
	{
		base._Ani.Play("铜头铁臂");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 铜头铁臂_反击._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 铜头铁臂_反击._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		_Mono_虎先锋._Is铜头铁臂 = true;
		int 铜头铁臂后招式 = 1;
		float _攻击Timer = 0f;
		while (_攻击Timer < 1.114514f)
		{
			if (!base._Mono._Is被定)
			{
				_攻击Timer += Time.deltaTime;
			}
			if (!_Mono_虎先锋._Is铜头铁臂)
			{
				铜头铁臂后招式 = 2;
				DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弹反();
				_攻击Timer = 999f;
			}
			yield return null;
		}
		switch (铜头铁臂后招式)
		{
		case 1:
			base._Ani.Play("直拳");
			base._Rb2D.velocity = Vector2.zero;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 35f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
			break;
		case 2:
			base._Ani.Play("直拳_快");
			base._Rb2D.velocity = Vector2.zero;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.05f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 35f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
			break;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
