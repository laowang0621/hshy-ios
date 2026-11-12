using System.Collections.Generic;
using DSB;
using UnityEngine;

public class EnemyAI_亢金龙 : EnemyAI_Boss
{
	private DataInfo_Enemy招式数值 雷球;

	public List<Transform> _Tsf_三连大落雷;

	public List<Transform> _Tsf_三连1_大落雷;

	public List<Transform> _Tsf_三连2_大落雷;

	public List<Transform> _Tsf_三连3_大落雷;

	public List<Transform> _Tsf_四角;

	public Transform _Tsf_龙息_雷球;

	public Transform _Tsf_攻击位置_方;

	public C_弹道 _C_弹道_雷球_Prefab;

	public C_弹道 _C_龙息_雷球_Prefab;

	public C_攻击判断 _C_小落雷_Prefab;

	public C_攻击判断 _C_大落雷_Prefab;

	public C_攻击判断 _C_电圈_Prefab;

	public bool _解锁特效攻击;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.击败亢金龙;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState亢金龙_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_亢金龙_攻击().Init(_Data));
		_解锁特效攻击 = false;
		雷球 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(7003);
	}

	public void F_Ani_Play_SFX_敲地()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_敲地();
	}

	public void F_Ani_生成雷球()
	{
		if (_解锁特效攻击)
		{
			C_弹道 c_弹道 = Object.Instantiate(_C_弹道_雷球_Prefab, _Tsf_龙息_雷球.position, Quaternion.identity);
			c_弹道._伤害 = 雷球._伤害系数 * _Data._Atk;
			Vector2 vector = _Tsf_攻击位置_方.position - _Tsf_龙息_雷球.position;
			float magnitude = vector.magnitude;
			Vector2 dir = vector / magnitude;
			c_弹道.F_Init_按距离销毁(dir, magnitude * 2f);
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小电流();
		}
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (!_Data._Is死亡 && !_Data._Is虚幻)
		{
			MyLog.LogWarning($"<<{base.name}>>被攻击<<{attackInfo.atk_生命}>>");
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
			if (!_解锁特效攻击 && _Data._Curr生命 <= _Data._MaxHP * 0.56f)
			{
				_解锁特效攻击 = true;
			}
			if (_Data._Curr生命 <= 0f)
			{
				DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
				ChangeState(EnemyStateName_Type.死亡);
			}
			else
			{
				_Data._Mono组件._Rb2D.velocity = 0.6f * base._BeAttack_击退力度 * new Vector2((base.transform.position.x > base._BeAttack_Pos.x) ? 1 : (-1), 0f);
			}
		}
	}
}
