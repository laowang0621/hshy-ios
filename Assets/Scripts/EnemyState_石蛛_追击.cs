using UnityEngine;

public class EnemyState_石蛛_追击 : EnemyState_狼剑客_追击
{
	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		_Speed = ((EnemyAI_石蛛)data._Mono)._移动速度;
		return base.Init(data);
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
		base._Ani.Play("移动");
		_StartPos = base._Mono.transform.position;
		Vector2 vector = _Data._Pos_追击目标位置;
		Vector2 vector2 = Random.insideUnitCircle * 6f;
		vector2.y *= 0.5f;
		_targetPos = vector + vector2;
		base._Mono.F_判断撞墙次数是否增加(_StartPos, _targetPos, out var dir);
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((dir.x > 0f) ? num : (0f - num), num, num);
		_DirSpeed = dir * _Speed;
		base._Mono组件._Tsf_移动目标.transform.position = _targetPos;
		_Timer = _间隔;
	}
}
