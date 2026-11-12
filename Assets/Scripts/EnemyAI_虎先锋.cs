using DSB;
using UnityEngine;

public class EnemyAI_虎先锋 : EnemyAI_Boss
{
	public C_弹道_血龙卷 _C_弹道_血龙卷Prefab;

	public bool _Is铜头铁臂;

	public override Enum_对话 _对话类型_见面 => Enum_对话.虎先锋前置对话;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.NULL;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_虎先锋_攻击().Init(_Data));
		_Is铜头铁臂 = false;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_Data._Is死亡 || _Data._Is虚幻)
		{
			return;
		}
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		if (_Is铜头铁臂)
		{
			_Is铜头铁臂 = false;
			MyLog.LogError("你在干什么，虎先锋释放铜头铁臂不饿能攻击他!");
			return;
		}
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

	public void F_Ani_Play_砸地()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_砸地();
	}

	public void F_Ani_Play_虎先锋近战1()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_虎先锋近战1();
	}

	public void F_Ani_Play_虎先锋近战2()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_虎先锋近战2();
	}

	public void F_Ani_Play_虎先锋近战3()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_虎先锋近战3();
	}
}
