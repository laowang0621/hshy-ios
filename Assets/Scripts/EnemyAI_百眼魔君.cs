using System.Collections.Generic;
using DSB;
using UnityEngine;

public class EnemyAI_百眼魔君 : EnemyAI_Boss
{
	public bool _吞剑中;

	public Vector2 _初始位置;

	public bool _开启固定出招;

	public int _固定出招序号 = 1;

	public Collider2D _Co2D_尾巴;

	public SpriteRenderer _SR_尾巴;

	public C_攻击判断 _C_大落雷_Prefab;

	public C_弹道 _C_弹道_雷球_Prefab;

	public EnemyAI_百眼魔君_眼球 _C_眼球_Prefab;

	public Transform _Tsf_插地剑;

	public Transform _Tsf_电光炮;

	public Transform _Tsf_眼球生成范围;

	public Transform _Tsf_眼球_攻击指示拖尾_Prefab;

	public ParticleSystem _PS_VFX_血雾;

	public List<EnemyAI_百眼魔君_符纸> _List_符纸;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击(10f).Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_百眼魔君_攻击().Init(_Data));
		_初始位置 = base.transform.position;
		_Tsf_眼球生成范围.parent = null;
		_PS_VFX_血雾.transform.parent = null;
		_PS_VFX_血雾.Stop();
		base._Boss死亡 += delegate
		{
			DSB_MonoSingleton<GameManager>.Instance._PC.F_临时_设置气力上限百分比(1f);
		};
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (_PS_VFX_血雾 != null)
		{
			_PS_VFX_血雾.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
		}
		if (_Tsf_眼球生成范围 != null)
		{
			Object.Destroy(_Tsf_眼球生成范围.gameObject);
		}
	}

	public void F_触发吞剑()
	{
		if (_PS_VFX_血雾 != null)
		{
			_PS_VFX_血雾.Play();
		}
		Debug.LogWarning("触发吞剑");
		((EnemyState_Boss_站立)_FSM.states[EnemyStateName_Type.站立])._AniID_站立 = Animator.StringToHash("站立_无刀");
		((EnemyState_Boss_受击)_FSM.states[EnemyStateName_Type.受击])._AniID_受击 = Animator.StringToHash("受击_无刀");
		((EnemyState_Boss_死亡)_FSM.states[EnemyStateName_Type.死亡])._AniID_受击 = Animator.StringToHash("受击_无刀");
		((EnemyState_Boss_追击)_FSM.states[EnemyStateName_Type.追击])._AniID_站立 = Animator.StringToHash("站立_无刀");
		((EnemyState_Boss_追击)_FSM.states[EnemyStateName_Type.追击])._AniID_移动 = Animator.StringToHash("移动_无刀");
		_吞剑中 = true;
		Invoke("F_吞剑结束", 2f);
	}

	private void F_吞剑结束()
	{
		_吞剑中 = false;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_吞剑中)
		{
			attackInfo.atk_生命 *= 0.2f;
			attackInfo.atk_僵直 = -10f;
		}
		base.BeAttack(attackInfo);
	}

	public override void F_定身(float 时间)
	{
		if (!_吞剑中)
		{
			base.F_定身(时间);
		}
	}
}
