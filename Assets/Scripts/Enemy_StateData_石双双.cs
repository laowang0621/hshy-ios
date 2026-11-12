using DSB;
using UnityEngine;

public class Enemy_StateData_石双双 : Enemy_StateData_狼剑客
{
	public EnemyAI_石双双 _Mono_石双双;

	public override bool _视野范围有敌人
	{
		get
		{
			if (_Mono_石双双._Is召唤)
			{
				return true;
			}
			if (!base._Mono组件._视野范围._范围内发现敌人)
			{
				return base._Mono组件._攻击范围._范围内发现敌人;
			}
			return true;
		}
	}

	public override bool _攻击范围有敌人 => base._Mono组件._攻击范围._范围内发现敌人;

	public override Vector3 _Pos_追击目标位置
	{
		get
		{
			if (_攻击范围有敌人)
			{
				return base._Mono组件._攻击范围._目标位置;
			}
			if (base._Mono组件._视野范围._范围内发现敌人)
			{
				return base._Mono组件._视野范围._目标位置;
			}
			return DSB_MonoSingleton<GameManager>.Instance._PC.transform.position;
		}
	}

	public Enemy_StateData_石双双(EnemyAI_狼剑客 mono)
		: base(mono)
	{
		_Mono_石双双 = (EnemyAI_石双双)mono;
	}
}
