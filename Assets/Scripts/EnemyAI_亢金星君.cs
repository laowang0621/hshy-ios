using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyAI_亢金星君 : EnemyAI_Boss
{
	public bool _开启固定出招;

	public int _固定出招序号 = 1;

	public List<Transform> _List_电疗针生成位置;

	public List<Transform> _List_电疗针2生成位置;

	public List<Transform> _List_电疗针转阶段生成位置;

	public Transform Tsf_后跳雷球;

	public Transform Tsf_雷球;

	public Transform Tsf_召雷;

	public Transform Tsf_电之宝库;

	public Transform Tsf_雷枪;

	public Transform _Tsf_攻击位置_方;

	public List<Transform> _Tsf_三连1_大落雷;

	public List<Transform> _Tsf_三连2_大落雷;

	public List<Transform> _Tsf_三连3_大落雷;

	public List<Transform> _Tsf_四角;

	public C_弹道_电疗针 _C_弹道_电疗针_Prefab;

	public C_弹道 _C_弹道_雷球_Prefab;

	public C_攻击判断 _C_小落雷_Prefab;

	public C_攻击判断 _C_大落雷_Prefab;

	public C_攻击判断 _C_电圈_Prefab;

	public C_亢金_雷枪 _C_亢金_雷枪;

	public Light2D _2D全局光;

	public bool _无敌;

	public override Enum_对话 _对话类型_见面 => Enum_对话.亢金星君前置对话;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_亢金星君_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_亢金星君_攻击().Init(_Data));
		_无敌 = false;
		base._Boss死亡 += delegate
		{
			DOTween.To(() => 0.1f, delegate(float x)
			{
				_2D全局光.intensity = x;
			}, 1f, 1f);
		};
		Tsf_电之宝库.gameObject.SetActive(value: false);
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (!_无敌)
		{
			base.BeAttack(attackInfo);
		}
	}
}
