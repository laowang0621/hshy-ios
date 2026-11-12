using UnityEngine;

public class EnemyState_狼剑客_散步 : EnemyState_狼剑客
{
	private Vector2 _targetPos;

	private Vector2 _StartPos;

	private float _Speed = 2f;

	private Vector2 _DirSpeed;

	public override void OnEnter()
	{
		_StartPos = base._Mono.transform.position;
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		if (base._Mono._开启游荡模式)
		{
			base._Ani.Play("移动");
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
			_Timer = Vector2.Distance(_targetPos, _StartPos);
			_DirSpeed = (_targetPos - _StartPos).normalized * _Speed;
			base._Mono.transform.localScale = new Vector3((_DirSpeed.x > 0f) ? num : (0f - num), num, num);
			base._Mono组件._Tsf_移动目标.transform.position = _targetPos;
		}
		else
		{
			_targetPos = _Data._StartPos;
			_Timer = Vector2.Distance(_targetPos, _StartPos);
			if (_Timer < 0.5f)
			{
				ChangeState(EnemyStateName_Type.站立);
				return;
			}
			base._Ani.Play("移动");
			_DirSpeed = (_targetPos - _StartPos).normalized * _Speed;
			base._Mono.transform.localScale = new Vector3((_DirSpeed.x > 0f) ? num : (0f - num), num, num);
			base._Mono组件._Tsf_移动目标.transform.position = _targetPos;
		}
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime * _Speed;
		base._Rb2D.velocity = _DirSpeed;
		if (_Data._视野范围有敌人)
		{
			ChangeState(EnemyStateName_Type.追击);
		}
		if (_Timer < 0f)
		{
			ChangeState(EnemyStateName_Type.站立);
		}
	}
}
