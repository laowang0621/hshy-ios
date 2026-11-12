using DG.Tweening;
using DSB;

public class EnemyState_石母_死亡 : EnemyState_Boss
{
	public override void OnEnter()
	{
		_Data._Is死亡 = true;
		DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
		DSB_MonoSingleton<UIManager>.Instance.F_显示得胜(base._Mono组件._EnemyID, delegate
		{
			if (!DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_Data._Mono._对话类型_死亡))
			{
				GameManager._CanPlay = false;
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
		((EnemyAI_石母)base._Mono)._SR_头发2.DOFade(0f, 0.5f);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
