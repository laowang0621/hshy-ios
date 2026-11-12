using System;
using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_沙二郎_攻击 : EnemyState_Boss
{
	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor_攻击;

	private float _EnterDrag;

	private DataInfo_Enemy招式数值 直升机;

	private DataInfo_Enemy招式数值 冲撞;

	private DataInfo_Enemy招式数值 二连锤;

	private DataInfo_Enemy招式数值 剑气;

	private DataInfo_Enemy招式数值 原地连砸;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		直升机 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(3001);
		冲撞 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(3002);
		二连锤 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(3003);
		剑气 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(3004);
		原地连砸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(3005);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		F_攻击();
	}

	public override void OnExit()
	{
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
		_ = (Vector2)base._Mono.transform.position;
		_目标位置 = _Data._Pos_追击目标位置;
		_Cor_攻击 = UnityEngine.Random.Range(0, 5) switch
		{
			0 => base._Mono组件.StartCoroutine(Cor_二连锤()), 
			1 => base._Mono组件.StartCoroutine(Cor_冲撞()), 
			2 => base._Mono组件.StartCoroutine(Cor_原地连砸()), 
			3 => base._Mono组件.StartCoroutine(Cor_横锤剑气()), 
			4 => base._Mono组件.StartCoroutine(Cor_直升机()), 
			_ => throw new ArgumentOutOfRangeException(), 
		};
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_二连锤()
	{
		MyLog.LogError("Cor_二连锤");
		base._Ani.Play("二连锤");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连锤._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连锤._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 20f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_冲撞()
	{
		MyLog.LogError("Cor_冲撞");
		base._Ani.Play("冲撞_0");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 冲撞._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 冲撞._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		base._Rb2D.drag = 0f;
		F_更新攻击方向();
		base._Ani.Play("冲撞_1");
		base._Rb2D.velocity = _攻击方向 * 45f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		base._Rb2D.drag = _EnterDrag;
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_原地连砸()
	{
		MyLog.LogError("Cor_原地连砸");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 原地连砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 原地连砸._击退力度;
		int _val = UnityEngine.Random.Range(0, 2);
		base._Ani.Play("原地连砸_" + _val);
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 15f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.83f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 15f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 25f;
		if (_val == 0)
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.8f));
		}
		else
		{
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.5f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 40f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_横锤剑气()
	{
		MyLog.LogError("Cor_横锤剑气");
		base._Ani.Play("横锤剑气");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 剑气._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 剑气._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 10f;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		float num = Mathf.Abs(_攻击方向.x);
		float num2 = Mathf.Abs(_攻击方向.y);
		Vector2 dir = ((num2 < 0.33f * num) ? _攻击方向 : new Vector2(_攻击方向.x, _攻击方向.y / num2 * 0.33f * num).normalized);
		UnityEngine.Object.Instantiate(base._Mono组件._C_弹道_剑气Prefab, base._Mono.transform.position, Quaternion.identity).F_Init(dir, base._Mono组件._C_攻击判断._伤害);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private IEnumerator Cor_直升机()
	{
		MyLog.LogError("Cor_直升机");
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 直升机._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 直升机._击退力度;
		int count = UnityEngine.Random.Range(3, 5);
		for (int i = 0; i < count; i++)
		{
			base._Ani.Play("直升机_0", 0, 0f);
			base._Rb2D.velocity = Vector2.zero;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 10f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.35f));
		}
		MyLog.LogError("Cor_直升机111");
		base._Ani.Play("直升机_1");
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 10f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 40f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
