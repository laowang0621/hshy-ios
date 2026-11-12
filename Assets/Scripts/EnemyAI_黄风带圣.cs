using DSB;
using UnityEngine;

public class EnemyAI_黄风带圣 : EnemyAI_Boss
{
	public C_地刺生成器 _地刺生成器;

	public C_黄风带圣_召唤蝜蝂 _C_黄风带圣_召唤蝜蝂;

	public EnemyAI_黄风带圣_龙卷风 _EnemyAI_黄风带圣_龙卷风;

	public bool _二阶段已触发;

	public bool _三阶段已触发;

	private float _二阶段_生命;

	private float _三阶段_生命;

	private bool _蝜蝂已死;

	public GameObject _GO_脚印Prefab;

	public bool _转阶段;

	public override Enum_对话 _对话类型_见面 => Enum_对话.黄风大圣前置对话;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.战胜黄风大圣;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_黄风带圣_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.飞升, new EnemyState_黄风带圣_转阶段().Init(_Data));
		_蝜蝂已死 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败(20205);
		_二阶段_生命 = _Data._MaxHP * 0.7f;
		_三阶段_生命 = _Data._MaxHP * 0.4f;
	}

	public override void F_定身(float 时间)
	{
		if (!_转阶段)
		{
			base.F_定身(时间);
		}
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_Data._Is死亡 || _Data._Is虚幻 || _转阶段)
		{
			return;
		}
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		base._BeAttack_Pos = attackInfo.atk_Pos;
		base._BeAttack_击退力度 = attackInfo.atk_击退力度;
		if (_Cor_受击变色 != null)
		{
			StopCoroutine(_Cor_受击变色);
		}
		_Cor_受击变色 = StartCoroutine(Cor_受击变色());
		_Data._Curr生命 -= attackInfo.atk_生命;
		_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
		if (_Data._Curr生命 <= 0f)
		{
			DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
			ChangeState(EnemyStateName_Type.死亡);
			return;
		}
		if (!_二阶段已触发 && _Data._Curr生命 < _二阶段_生命)
		{
			_二阶段已触发 = true;
			ChangeState(EnemyStateName_Type.飞升);
			return;
		}
		if (!_三阶段已触发 && _Data._Curr生命 < _三阶段_生命)
		{
			_三阶段已触发 = true;
			if (!_蝜蝂已死)
			{
				ChangeState(EnemyStateName_Type.飞升);
			}
			return;
		}
		_Data._Curr僵直数值 += attackInfo.atk_僵直;
		if (_Data._Curr僵直数值 >= _Data._Max僵直数值)
		{
			_Data._Curr僵直数值 = 0f;
			_Data._Max僵直数值 += _Data._僵直数值增长;
			ChangeState(EnemyStateName_Type.受击);
		}
		else
		{
			_Data._Mono组件._Rb2D.velocity = 0.8f * base._BeAttack_击退力度 * new Vector2((base.transform.position.x > base._BeAttack_Pos.x) ? 1 : (-1), 0f);
		}
	}
}
