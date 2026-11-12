using UnityEngine;

public class EnemyState_狼剑客_追击 : EnemyState_狼剑客
{
	protected Vector2 _targetPos;

	protected Vector2 _StartPos;

	public float _Speed = 4f;

	protected Vector2 _DirSpeed;

	protected int _累计追击判定次数;

	protected int _判断X次后出手;

	protected float 期望 = 5f;

	protected float _间隔 = 0.33f;

	protected int _1 = 5;

	protected int _2 = 10;

	protected float _脱离视野后追击Time;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		F_计算攻击欲望();
		return this;
	}

	public void F_计算攻击欲望()
	{
		_1 = (int)(_Data._追击时间_x / _间隔) + 1;
		_2 = (int)(_Data._追击时间_y / _间隔) + 1;
		_累计追击判定次数 = 0;
		_判断X次后出手 = Random.Range(_1, _2);
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
		_targetPos = vector + new Vector2((float)((vector.x < _StartPos.x) ? 1 : (-1)) * Random.Range(1f, 3f), Random.Range(-0.5f, 0.5f));
		base._Mono.F_判断撞墙次数是否增加(_StartPos, _targetPos, out var dir);
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((dir.x > 0f) ? num : (0f - num), num, num);
		_DirSpeed = dir * _Speed;
		base._Mono组件._Tsf_移动目标.transform.position = _targetPos;
		_Timer = _间隔;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		base._Rb2D.velocity = Vector2.Lerp(base._Rb2D.velocity, _DirSpeed, Time.deltaTime * 10.9f);
		if (!(_Timer < 0f))
		{
			return;
		}
		if (_Data._视野范围有敌人)
		{
			if (_Data._攻击范围有敌人 && F_判断出手())
			{
				base._Mono.ChangeState(EnemyStateName_Type.攻击);
			}
			else
			{
				base._Mono.ChangeState(EnemyStateName_Type.追击);
			}
		}
		else if (_脱离视野后追击Time > 0f)
		{
			base._Mono.ChangeState(EnemyStateName_Type.追击);
		}
		else
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}

	protected bool F_判断出手()
	{
		if (_累计追击判定次数 > _判断X次后出手)
		{
			_累计追击判定次数 = 0;
			_判断X次后出手 = Random.Range(_1, _2);
			return true;
		}
		return false;
	}
}
