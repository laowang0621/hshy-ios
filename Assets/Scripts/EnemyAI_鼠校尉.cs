using DSB;
using UnityEngine;

public class EnemyAI_鼠校尉 : EnemyAI_狼剑客
{
	[SerializeField]
	protected bool _有盾 = true;

	protected Enemy_StateData_鼠校尉 _mData_鼠校尉;

	public override Enemy_StateData_狼剑客 _Data
	{
		get
		{
			if (_首次更新Data || _mData == null)
			{
				_首次更新Data = false;
				_mData_鼠校尉 = new Enemy_StateData_鼠校尉(this, _有盾);
				_mData = _mData_鼠校尉;
			}
			return _mData;
		}
	}

	public Enemy_StateData_鼠校尉 _Data_鼠校尉 => _mData_鼠校尉;

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		_已经进入初始化 = true;
		_ = _Data;
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_狼剑客_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.散步, new EnemyState_狼剑客_散步().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_狼剑客_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_狼剑客_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_狼剑客_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_狼剑客_死亡().Init(_Data));
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_Data._Is死亡 || _Data._Is虚幻)
		{
			return;
		}
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		MyLog.Log($"<<{base.name}>>被攻击<<{attackInfo.atk_生命}>>");
		base._BeAttack_Pos = attackInfo.atk_Pos;
		base._BeAttack_击退力度 = attackInfo.atk_击退力度;
		MyLog.LogError("===============" + _Data_鼠校尉._Curr生命_盾);
		bool flag = _Data._Mono组件.transform.localScale.x > 0f;
		bool flag2 = attackInfo.atk_Pos.x > _Data._Mono组件.transform.position.x;
		if (_Data_鼠校尉._Curr生命_盾 > 0f && flag == flag2)
		{
			MyLog.LogError("护盾吸收伤害");
			_Data_鼠校尉._Curr生命_盾 -= attackInfo.atk_生命;
			_Data_鼠校尉.F_设置UI血量_盾(_Data_鼠校尉._Curr生命_盾, _Data_鼠校尉._护盾血量Max);
			if (_Data_鼠校尉._Curr生命_盾 <= 0f)
			{
				_Data_鼠校尉._Mono组件._Ani.SetFloat("_有盾", 0f);
				ChangeState(EnemyStateName_Type.受击);
			}
			return;
		}
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
}
