using UnityEngine;

public class EnemyAI_黑手道人 : EnemyAI_Boss
{
	public C_飞刀 _C_飞刀_Prefab;

	public Transform _Tsf_飞刀;

	private bool _m空中;

	public bool _开启固定出招;

	public int _固定出招序号 = 1;

	[HideInInspector]
	public bool _冲刺中;

	public override Enum_对话 _对话类型_见面 => Enum_对话.黑手道人前置对话;

	public bool _空中
	{
		get
		{
			return _m空中;
		}
		set
		{
			_m空中 = value;
			if (_m空中)
			{
				base.gameObject.layer = 3;
			}
			else
			{
				base.gameObject.layer = 6;
			}
		}
	}

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_黑手道人_攻击().Init(_Data));
		_冲刺中 = false;
		_空中 = false;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (!_空中)
		{
			base.BeAttack(attackInfo);
		}
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		if (!_冲刺中)
		{
			_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		}
	}

	protected override void OnCollisionStay2D(Collision2D collision)
	{
		if (!_冲刺中)
		{
			if (collision.transform.CompareTag("Player"))
			{
				_Data._Mono组件._Rb2D.velocity = Vector2.zero;
			}
			_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		}
	}
}
