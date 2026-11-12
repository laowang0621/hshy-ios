using UnityEngine;

public class EnemyState_青蝠_散步 : EnemyState_狼剑客
{
	private Vector2 _targetPos;

	private Vector2 _StartPos;

	private float _Speed = 2f;

	private Vector2 _DirSpeed;

	protected float _间隔 = 0.5f;

	private int count;

	private EnemyAI_青蝠 _Mono青蝠;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono青蝠 = (EnemyAI_青蝠)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		_StartPos = base._Mono.transform.position;
		if (base._Mono组件._C_活动范围 != null)
		{
			_targetPos = base._Mono组件._C_活动范围.F_得到范围内随机点();
		}
		else
		{
			Vector2 vector = Random.insideUnitCircle * 4f;
			vector.y /= 4f;
			_targetPos = _Data._StartPos + vector;
		}
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.F_判断撞墙次数是否增加(_StartPos, _targetPos, out var dir);
		base._Mono.transform.localScale = new Vector3((dir.x > 0f) ? num : (0f - num), num, num);
		_DirSpeed = dir * _Speed;
		base._Mono组件._Tsf_移动目标.transform.position = _targetPos;
		base._Rb2D.velocity = Vector2.zero;
		_Timer = _间隔;
		_Mono青蝠._MoveTimer += _Timer;
		count++;
		base._Ani.SetBool("飞_高", _Mono青蝠._飞_高);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		base._Rb2D.velocity = Vector2.Lerp(base._Rb2D.velocity, _DirSpeed, Time.deltaTime * 10.9f);
		if (_Data._视野范围有敌人)
		{
			ChangeState(EnemyStateName_Type.追击);
		}
		if (_Timer < 0f)
		{
			if (count >= 5)
			{
				count = 0;
				ChangeState(EnemyStateName_Type.站立);
			}
			else
			{
				ChangeState(EnemyStateName_Type.散步);
			}
		}
	}
}
