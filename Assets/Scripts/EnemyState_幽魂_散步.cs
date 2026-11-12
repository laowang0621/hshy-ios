using UnityEngine;

public class EnemyState_幽魂_散步 : EnemyState_Boss
{
	private Vector2 _StartPos;

	private Vector2 _targetPos;

	private float _Speed = 1f;

	public float _Distance;

	public override void OnEnter()
	{
		base._Ani.Play("移动");
		_StartPos = base._Mono.transform.position;
		if (_StartPos.x < _Data._StartPos.x)
		{
			_targetPos = _Data._StartPos + new Vector2(6f, 0f);
		}
		else
		{
			_targetPos = _Data._StartPos + new Vector2(-6f, 0f);
		}
		base._Mono.transform.localScale = new Vector3((_targetPos.x > _StartPos.x) ? 1 : (-1), 1f, 1f);
		base._Mono组件._Tsf_移动目标.transform.position = _targetPos;
		_Distance = Vector2.Distance(_StartPos, _targetPos);
		_Timer = 0f;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		if (_Data._视野范围有敌人)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
			return;
		}
		_Timer += Time.deltaTime * _Speed;
		Vector2 position;
		if (_Timer <= _Distance)
		{
			position = Vector2.Lerp(_StartPos, _targetPos, _Timer / _Distance);
		}
		else
		{
			position = _targetPos;
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
		base._Rb2D.MovePosition(position);
	}
}
