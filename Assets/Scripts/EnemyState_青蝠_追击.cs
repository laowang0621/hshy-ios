using UnityEngine;

public class EnemyState_青蝠_追击 : EnemyState_石苍苍_追击
{
	private EnemyAI_青蝠 _Mono青蝠;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono青蝠 = (EnemyAI_青蝠)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		if (_Data._视野范围有敌人)
		{
			_脱离视野后追击Time = 5f;
		}
		else
		{
			_脱离视野后追击Time -= _间隔;
		}
		_累计追击判定次数++;
		_StartPos = base._Mono.transform.position;
		Vector2 vector = _Data._Pos_追击目标位置;
		_targetPos = vector + new Vector2((float)((vector.x < _StartPos.x) ? 1 : (-1)) * Random.Range(1f, 3f), Random.Range(-0.5f, 0.5f));
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((vector.x > _StartPos.x) ? num : (0f - num), num, num);
		base._Mono.F_判断撞墙次数是否增加(_StartPos, _targetPos, out var dir);
		_DirSpeed = dir * _Speed;
		base._Mono组件._Tsf_移动目标.transform.position = _targetPos;
		_Timer = _间隔;
		_Mono青蝠._MoveTimer += _Timer;
		MyLog.LogError($"_Mono青蝠._飞_高>>{_Mono青蝠._飞_高}");
		base._Ani.SetBool("飞_高", _Mono青蝠._飞_高);
	}
}
