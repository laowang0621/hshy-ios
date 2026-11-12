using System.Collections.Generic;
using DSB;
using UnityEngine;

public class EnemyAI_雪僵尸 : EnemyAI_狼剑客
{
	public bool _已重置需静止;

	public bool _可以修改_已重置需静止;

	public SpriteRenderer _SR_静止;

	public List<Sprite> _List_静止_Sprite;

	public override void F_重置()
	{
		base.F_重置();
		_已重置需静止 = true;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		base.BeAttack(attackInfo);
		if (_SR_静止.color.a > 0.5f)
		{
			_SR_静止.SetAlpha(0f);
			F_取消静止伪装();
		}
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_可以修改_已重置需静止 = false;
		_已重置需静止 = true;
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_雪僵尸_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_狼剑客_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_雪僵尸_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_狼剑客_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_雪僵尸_死亡().Init(_Data));
		F_随机静止姿势();
	}

	public override void ChangeState(EnemyStateName_Type state)
	{
		_可以修改_已重置需静止 = state != EnemyStateName_Type.站立;
		base.ChangeState(state);
	}

	public virtual void F_随机静止姿势()
	{
		_SR_静止.SetAlpha(1f);
		_SR_静止.sprite = _List_静止_Sprite[Random.Range(0, _List_静止_Sprite.Count)];
	}

	public virtual void F_取消静止伪装()
	{
		_SR_静止.SetAlpha(0f);
		_Data._Mono组件._SR_Model.SetAlpha(1f);
	}
}
