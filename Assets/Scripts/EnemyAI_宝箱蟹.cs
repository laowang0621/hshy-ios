using DSB;
using UnityEngine;

public class EnemyAI_宝箱蟹 : EnemyAI_Base<EnemyAI_宝箱蟹, EnemyState_宝箱蟹, Enemy_StateData_宝箱蟹>
{
	protected bool _已经进入初始化;

	[HideInInspector]
	public ItemUI_交互 _ItemUI_交互键;

	[HideInInspector]
	public Vector2 _Dir;

	public float _移动速度 = 5f;

	public Collider2D _伤害区域;

	[Header("和宝箱一直即可.用于记录宝箱蟹是否击败,可以用场景(id+编号)(205+01=20501)的形式记录")]
	public int _宝箱蟹ID = 114514;

	public override int _阵营
	{
		get
		{
			return 3;
		}
		set
		{
			MyLog.LogError(6666);
		}
	}

	public override void F_显示()
	{
		if (!DSB_MonoSingleton<MyDataManager>.Instance.F_判断宝箱已开启_新(_宝箱蟹ID))
		{
			base.F_显示();
		}
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_ = _Data;
		_Data._Mono组件._Rb2D.drag = 5f;
		_已经进入初始化 = true;
		_ItemUI_交互键 = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_交互();
		_ItemUI_交互键._可见性 = false;
		_ItemUI_交互键.F_SetPos(_Data._Mono组件._Tsf_HP.position);
		_ItemUI_交互键.F_SetText("打开");
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_宝箱蟹_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_宝箱蟹_散步().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_宝箱蟹_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_宝箱蟹_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_宝箱蟹_死亡().Init(_Data));
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_FSM.currStateName != 0)
		{
			base.BeAttack(attackInfo);
		}
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (_ItemUI_交互键._可见性)
		{
			_ItemUI_交互键.F_SetPos(_Data._Mono组件._Tsf_HP.position);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		_FSM?.currState?.OnCollisionEnter2D(collision);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_FSM?.currState?.OnTriggerEnter2D(collision);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		_FSM?.currState?.OnTriggerExit2D(collision);
	}

	public void F_朝向玩家()
	{
		_Dir = ((Vector2)(DSB_MonoSingleton<GameManager>.Instance._PC.transform.position - base.transform.position)).normalized;
	}

	public void F_反方向移动()
	{
		_Dir = (-_Dir).Rotate(Random.Range(-25, 25));
	}
}
