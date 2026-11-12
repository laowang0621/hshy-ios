using DSB;
using UnityEngine;

public class Enemy_StateData_Boss : Enemy_StateData_Base<EnemyAI_Boss, EnemyState_Boss, Enemy_StateData_Boss>
{
	public ItemUI_BossHP _UI_BossHP;

	public bool _Is飞升;

	private bool _已经发现玩家;

	public override bool _视野范围有敌人
	{
		get
		{
			if (_已经发现玩家)
			{
				return true;
			}
			if (base._Mono组件._视野范围._范围内发现敌人)
			{
				_已经发现玩家 = true;
				return true;
			}
			return false;
		}
	}

	public override Vector3 _Pos_追击目标位置
	{
		get
		{
			if (base._Mono组件._视野范围._范围内发现敌人)
			{
				return base._Mono组件._视野范围._目标位置;
			}
			return DSB_MonoSingleton<GameManager>.Instance._PC.transform.position;
		}
	}

	public Enemy_StateData_Boss(EnemyAI_Boss mono)
		: base(mono)
	{
	}

	public override void F_获取血条UI()
	{
		_UI_BossHP = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_BossHP();
		_UI_BossHP._可见性 = false;
		_UI_BossHP.F_SetVal(_Curr生命, _MaxHP);
		_UI_BossHP.F_设置名字(base._Mono组件._EnemyID.ToString());
	}

	public override void F_设置UI血量(float currHP, float maxHP, bool _可见性 = true)
	{
		if (!(_UI_BossHP == null))
		{
			_UI_BossHP._可见性 = _可见性;
			if (_可见性)
			{
				_UI_BossHP.F_SetVal(currHP, maxHP);
			}
		}
	}
}
