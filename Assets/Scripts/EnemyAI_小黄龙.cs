using UnityEngine;

public class EnemyAI_小黄龙 : EnemyAI_Boss
{
	private bool _m空中;

	public bool _开启固定出招;

	public int _固定出招序号 = 1;

	public TrailRenderer _TR_拖尾_1;

	public TrailRenderer _TR_拖尾_2;

	[HideInInspector]
	public bool _冲刺中;

	public override Enum_对话 _对话类型_见面 => Enum_对话.小黄龙BOSS战前置对话;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.打败小黄龙四妹对话;

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
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_小黄龙_攻击().Init(_Data));
		_TR_拖尾_1.emitting = false;
		_TR_拖尾_2.emitting = false;
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
