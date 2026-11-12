using DSB;

public class Enemy_StateData_鼠校尉 : Enemy_StateData_狼剑客
{
	public float _护盾血量Max;

	public float _Curr生命_盾;

	public bool _有盾 = true;

	protected ItemUI_EnemyHP_带盾 _UI_HP_带盾;

	public Enemy_StateData_鼠校尉(EnemyAI_鼠校尉 mono, bool 有盾)
		: base(mono)
	{
		_有盾 = 有盾;
		if (_有盾)
		{
			_护盾血量Max = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_基础数值((int)base._Mono组件._EnemyID2)._生命值;
			_UI_HP_带盾 = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_EnemyHP_带盾();
			_UI_HP = _UI_HP_带盾;
			base._Mono组件._Ani.SetFloat("_有盾", 1f);
		}
		else
		{
			_Curr生命_盾 = -100f;
			_UI_HP = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_EnemyHP();
		}
	}

	public override void F_获取血条UI()
	{
	}

	public override void F_设置UI血量(float currHP, float maxHP, bool _可见性 = true)
	{
		if (!(_UI_HP == null))
		{
			_UI_HP._可见性 = _可见性;
			_UI_HP.F_SetVal(currHP, maxHP);
		}
	}

	public void F_设置UI血量_盾(float currHP, float maxHP, bool _可见性 = true)
	{
		if (!(_UI_HP == null))
		{
			_UI_HP._可见性 = _可见性;
			_UI_HP_带盾.F_SetVal_盾(currHP, maxHP);
		}
	}

	public override void F_显示()
	{
		base.F_显示();
		if (_有盾)
		{
			_Curr生命_盾 = _护盾血量Max;
			F_设置UI血量(_Curr生命, _MaxHP, _可见性: false);
			F_设置UI血量_盾(_Curr生命_盾, _护盾血量Max, _可见性: false);
			base._Mono组件._Ani.SetFloat("_有盾", 1f);
		}
	}

	public override void F_隐藏()
	{
		base.F_隐藏();
		_ = _有盾;
	}
}
