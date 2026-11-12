using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyState_黄眉_死亡 : EnemyState_Boss_死亡
{
	private EnemyAI_黄眉 _Mono_黄眉;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_黄眉 = (EnemyAI_黄眉)data._Mono;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		_Data._Is死亡 = true;
		_Mono_黄眉._金身 = false;
		if (_Data._Mono._Is被定)
		{
			_Data._Mono.F_定身结束();
		}
		if (_Mono_黄眉._死亡转阶段)
		{
			_Mono_Boss.F_Action_Boss死亡();
			base._Mono.StartCoroutine(Cor_黄眉1_死亡());
			return;
		}
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

	protected virtual IEnumerator Cor_黄眉1_死亡()
	{
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		base._Mono.F_删除血条();
		base._Ani.Play("转阶段1");
		base._Rb2D.velocity = Vector2.zero;
		base._Mono组件._SR_影子.DOFade(0f, 2f);
		base._Mono.transform.localScale = new Vector3((base._Mono._BeAttack_Pos.x > base._Mono.transform.position.x) ? 1 : (-1), 1f, 1f);
		yield return new WaitForSeconds(0.4f);
		_Mono_黄眉._PS_消散粒子.Play();
		yield return new WaitForSeconds(1f);
		_Mono_Boss.F_Action_Boss死亡_消失后();
		yield return new WaitForSeconds(2.5f);
		Object.Destroy(base._Mono.gameObject);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
