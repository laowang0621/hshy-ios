using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_大呱呱1_攻击 : EnemyState_Boss
{
	protected Vector2 _目标位置;

	protected Vector2 _攻击方向;

	protected Coroutine _Cor_攻击;

	protected float _EnterDrag;

	private DataInfo_Enemy招式数值 舌头突击;

	private DataInfo_Enemy招式数值 咆哮;

	private DataInfo_Enemy招式数值 跳砸;

	private DataInfo_Enemy招式数值 巴掌二连;

	private DataInfo_Enemy招式数值 后蹬腿;

	private DataInfo_Enemy招式数值 肉蛋葱鸡;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		舌头突击 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5001);
		咆哮 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5002);
		跳砸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5003);
		巴掌二连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5004);
		后蹬腿 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5005);
		肉蛋葱鸡 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(5006);
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

	protected virtual void F_攻击()
	{
		if (_Cor_攻击 != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor_攻击);
		}
		F_大大呱呱1出招逻辑();
	}

	protected void F_大大呱呱1出招逻辑()
	{
		Vector2 a = base._Mono.transform.position;
		_目标位置 = _Data._Pos_追击目标位置;
		float num = Vector2.Distance(a, _目标位置);
		if (num < 15f && Mathf.Abs(a.y - _目标位置.y) < 1.2f && Random.value < 0.8f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_舌头突击());
		}
		else if (num < 10f)
		{
			float value = Random.value;
			if (value < 0.35f)
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_三连());
			}
			else if (value < 0.7f)
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_后蹬腿());
			}
			else
			{
				_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_咆哮());
			}
		}
		else if (Random.value < 0.6f)
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_肉蛋葱鸡());
		}
		else
		{
			_Cor_攻击 = base._Mono组件.StartCoroutine(Cor_跳砸());
		}
	}

	private protected void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? num : (0f - num), num, num);
	}

	protected IEnumerator Cor_三连()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 巴掌二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 巴掌二连._击退力度;
		base._Ani.Play("三连");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.254f));
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.65f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 跳砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 跳砸._击退力度;
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 45f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.25f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected IEnumerator Cor_舌头突击()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 舌头突击._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 舌头突击._击退力度;
		base._Ani.Play("舌头突击");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.1f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected IEnumerator Cor_跳砸()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 跳砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 跳砸._击退力度;
		base._Ani.Play("跳跃");
		int conut = Random.Range(3, 6);
		for (int i = 0; i < conut; i++)
		{
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 25f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.1f));
		}
		base._Ani.Play("砸地");
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.8f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected IEnumerator Cor_咆哮()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_波浪咆哮();
		yield return new WaitForSeconds(0.1f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 咆哮._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 咆哮._击退力度;
		base._Ani.Play("咆哮");
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.1f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected IEnumerator Cor_后蹬腿()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 后蹬腿._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 后蹬腿._击退力度;
		int 踢次数 = Random.Range(1, 4);
		for (int i = 0; i < 踢次数; i++)
		{
			base._Ani.Play("后蹬腿_0", 0, 0f);
			F_更新攻击方向();
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
			base._Rb2D.velocity = _攻击方向 * 55f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.6f));
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	protected IEnumerator Cor_肉蛋葱鸡()
	{
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 肉蛋葱鸡._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 后蹬腿._击退力度;
		base._Ani.Play("肉蛋葱鸡_0");
		F_更新攻击方向();
		float time = Random.Range(0.6f, 1f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
		base._Rb2D.drag = 2f;
		base._Ani.Play("肉蛋葱鸡_1");
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 55f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.32f));
		base._Rb2D.drag = _Data._StartDrag;
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
