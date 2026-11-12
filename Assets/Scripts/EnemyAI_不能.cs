using DSB;
using UnityEngine;

public class EnemyAI_不能 : EnemyAI_Boss
{
	public bool _攻击架势;

	[HideInInspector]
	public bool _无敌;

	public bool _转阶段;

	public override Enum_对话 _对话类型_见面 => Enum_对话.不能前置对话;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.NULL;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_不能_攻击().Init(_Data));
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_无敌 || _Data._Is死亡 || _Data._Is虚幻)
		{
			return;
		}
		if (base._Is被定)
		{
			MyLog.Log($"<<{base.name}>>_Is被定前_被攻击<<{attackInfo.atk_生命}>>");
			attackInfo.atk_生命 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1402);
			attackInfo.atk_生命 *= 1f + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1403);
		}
		MyLog.Log($"<<{base.name}>>被攻击<<{attackInfo.atk_生命}>>");
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		base._BeAttack_Pos = attackInfo.atk_Pos;
		base._BeAttack_击退力度 = attackInfo.atk_击退力度;
		if (_Cor_受击变色 != null)
		{
			StopCoroutine(_Cor_受击变色);
		}
		_Cor_受击变色 = StartCoroutine(Cor_受击变色());
		if (_攻击架势)
		{
			_Data._Curr生命 -= attackInfo.atk_生命 * 0.5f;
		}
		else
		{
			_Data._Curr生命 -= attackInfo.atk_生命;
		}
		_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
		if (_Data._Curr生命 <= 0f)
		{
			DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
			ChangeState(EnemyStateName_Type.死亡);
			return;
		}
		_Data._Curr僵直数值 += attackInfo.atk_僵直;
		if (!_攻击架势 && _Data._Curr僵直数值 >= _Data._Max僵直数值)
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

	public override void F_定身(float 时间)
	{
		if (!_转阶段)
		{
			base.F_定身(时间);
		}
	}
}
