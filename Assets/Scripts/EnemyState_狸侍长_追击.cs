using UnityEngine;

public class EnemyState_狸侍长_追击 : EnemyState_狼剑客
{
	private Vector2 _StartPos;

	private float _Speed = 3f;

	private Vector2 _DirSpeed;

	private int _追击Count;

	private int _攻击Count;

	private EnemyAI_狸侍长 _Mono_狸侍长;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono_狸侍长 = (EnemyAI_狸侍长)base._Mono;
		return this;
	}

	public override void OnEnter()
	{
		if (_Mono_狸侍长._视野范围有敌人)
		{
			_追击Count = 5;
			_攻击Count++;
		}
		else
		{
			_追击Count--;
		}
		base._Ani.Play("移动");
		_StartPos = base._Mono.transform.position;
		Vector2 目标位置 = _Mono_狸侍长._目标位置;
		Vector2 vector = 目标位置 + new Vector2((float)((目标位置.x < _StartPos.x) ? 1 : (-1)) * Random.Range(1f, 3f), Random.Range(-0.5f, 0.5f));
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((目标位置.x > _StartPos.x) ? num : (0f - num), num, num);
		base._Mono.F_判断撞墙次数是否增加(_StartPos, vector, out var dir);
		_DirSpeed = dir * _Speed;
		base._Mono组件._Tsf_移动目标.transform.position = vector;
		if (_Mono_狸侍长._视野范围有敌人)
		{
			_Timer = Random.Range(0.5f, 0.8f);
		}
		else
		{
			_Timer = Random.Range(2f, 2.5f);
		}
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime * _Speed;
		base._Rb2D.velocity = Vector2.Lerp(base._Rb2D.velocity, _DirSpeed, Time.deltaTime * 10.9f);
		if (_Timer < 0f)
		{
			if (_攻击Count > 3 && _Data._攻击范围有敌人 && Random.value < 0.55f)
			{
				_攻击Count = 0;
				base._Mono.ChangeState(EnemyStateName_Type.攻击);
			}
			else if (_追击Count > 0)
			{
				base._Mono.ChangeState(EnemyStateName_Type.追击);
			}
			else
			{
				base._Mono.ChangeState(EnemyStateName_Type.站立);
			}
		}
	}
}
