using DSB;
using UnityEngine;

public class EnemyAI_寅虎 : EnemyAI_Boss
{
	protected int _m阵营 = 2;

	public bool _可以磐石;

	public bool _触发磐石;

	public int _磐石能量;

	public bool _非受击死亡状态_定身中被攻击;

	public GameObject _GO_蓄力特效_Prefab;

	public GameObject _GO_磐石特效_Prefab;

	public GameObject _GO_蓄力下劈特效_Prefab;

	[HideInInspector]
	public bool _可以隐身 = true;

	public override float _受击僵直时长 => 2.85f;

	public override Enum_对话 _对话类型_见面 => Enum_对话.寅虎前置对话;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.击败寅虎对话;

	public override int _阵营
	{
		get
		{
			return _m阵营;
		}
		set
		{
			_m阵营 = value;
		}
	}

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyStat_寅虎_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_寅虎_攻击().Init(_Data));
	}

	public void F_触发隐身()
	{
		_可以隐身 = false;
		Invoke("F_设置可以隐身", 35f);
	}

	public void F_设置可以隐身()
	{
		_可以隐身 = true;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_FSM.currStateName != EnemyStateName_Type.倒地 && _FSM.currStateName != EnemyStateName_Type.死亡 && base._Is被定)
		{
			MyLog.Log($"<<{base.name}>>_Is被定前_被攻击<<{attackInfo.atk_生命}>>");
			attackInfo.atk_生命 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1402);
			attackInfo.atk_生命 *= 1f + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1403);
			_Data._Curr生命 -= attackInfo.atk_生命;
			if (_Data._Curr生命 <= 99f)
			{
				_Data._Curr生命 = 99f;
			}
			_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
			if (_Data._Curr生命 <= 100f)
			{
				DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
				ChangeState(EnemyStateName_Type.死亡);
			}
			else if (_可以隐身)
			{
				_非受击死亡状态_定身中被攻击 = true;
				F_定身结束();
				ChangeState(EnemyStateName_Type.攻击);
			}
		}
		else if (_可以磐石)
		{
			_触发磐石 = true;
			_Data._Curr生命 -= attackInfo.atk_生命 * 0.3f;
			if (_Data._Curr生命 <= 99f)
			{
				_Data._Curr生命 = 99f;
			}
			_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
			if (_Data._Curr生命 <= 100f)
			{
				DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
				ChangeState(EnemyStateName_Type.死亡);
			}
			else
			{
				_Data._Curr僵直数值 = 0f;
				ChangeState(EnemyStateName_Type.攻击);
			}
		}
		else
		{
			if (_Data._Is死亡 || _Data._Is虚幻)
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
			_Data._Curr生命 -= attackInfo.atk_生命;
			if (_Data._Curr生命 <= 99f)
			{
				_Data._Curr生命 = 99f;
			}
			_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
			if (_Data._Curr生命 <= 100f)
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
}
