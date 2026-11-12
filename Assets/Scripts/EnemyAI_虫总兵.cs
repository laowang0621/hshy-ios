using UnityEngine;

public class EnemyAI_虫总兵 : EnemyAI_Boss
{
	public bool _开启固定出招;

	public int _固定出招序号 = 1;

	public EnemyAI_虫总兵_梨花针 _投射物Prefab;

	public Transform _Tsf_梨花针;

	public bool _格挡架势;

	public bool _触发格挡;

	public int _格挡次数;

	public override Enum_对话 _对话类型_见面 => Enum_对话.虫总兵前置对话;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击(10f).Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_虫总兵_攻击().Init(_Data));
		_格挡架势 = false;
		_触发格挡 = false;
		_格挡次数 = 0;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		MyLog.LogError($"attackInfo.atk_有僵直:{attackInfo.atk_有僵直},_格挡架势:{_格挡架势}");
		if (!base._Is被定 && attackInfo.atk_有僵直 && _格挡架势)
		{
			_触发格挡 = true;
			ChangeState(EnemyStateName_Type.攻击);
		}
		else
		{
			base.BeAttack(attackInfo);
		}
	}
}
