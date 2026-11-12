using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_Boss_死亡 : EnemyState_Boss
{
	private float _死亡后等待时间 = 2f;

	public int _AniID_受击 = Animator.StringToHash("受击");

	public EnemyState_Boss_死亡()
	{
	}

	public EnemyState_Boss_死亡(float 死亡后等待时间)
	{
		_死亡后等待时间 = 死亡后等待时间;
	}

	public override void OnEnter()
	{
		_Data._Is死亡 = true;
		if (_Mono_Boss._开启Boss死亡UI)
		{
			DSB_MonoSingleton<UIManager>.Instance.F_显示得胜(base._Mono组件._EnemyID, delegate
			{
				if (!DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_Data._Mono._对话类型_死亡))
				{
					DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_Data._Mono._对话类型_死亡, delegate
					{
						DSB_MonoSingleton<MyDataManager>.Instance.F_击败Boss((int)base._Mono组件._EnemyID);
						_Mono_Boss.F_Action_Boss死亡();
					});
				}
				else
				{
					DSB_MonoSingleton<MyDataManager>.Instance.F_击败Boss((int)base._Mono组件._EnemyID);
					_Mono_Boss.F_Action_Boss死亡();
				}
			});
		}
		else
		{
			DSB_MonoSingleton<MyDataManager>.Instance.F_击败Boss((int)base._Mono组件._EnemyID);
			_Mono_Boss.F_Action_Boss死亡();
		}
		base._Mono.StartCoroutine(Cor_死亡());
	}

	protected virtual IEnumerator Cor_死亡()
	{
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		base._Mono.F_删除血条();
		base._Ani.Play(_AniID_受击);
		base._Rb2D.velocity = 10f * new Vector2(base._Mono.transform.position.x - base._Mono._BeAttack_Pos.x, 0f).normalized;
		base._Mono.transform.localScale = new Vector3((base._Mono._BeAttack_Pos.x > base._Mono.transform.position.x) ? 1 : (-1), 1f, 1f);
		yield return new WaitForSeconds(0.5f);
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("倒地");
		yield return new WaitForSeconds(_死亡后等待时间);
		_Mono_Boss.F_Action_Boss死亡_消失后();
		WaitForSeconds t1 = new WaitForSeconds(0.1f);
		for (int i = 0; i < 5; i++)
		{
			base._Mono组件._SR_Model.SetAlpha(0f);
			base._Mono组件._SR_影子.SetAlpha(0f);
			yield return t1;
			base._Mono组件._SR_Model.SetAlpha(1f);
			base._Mono组件._SR_影子.SetAlpha(1f);
			yield return t1;
		}
		yield return null;
		Object.Destroy(base._Mono.gameObject);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
