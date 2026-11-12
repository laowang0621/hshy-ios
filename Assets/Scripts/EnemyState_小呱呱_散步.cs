using UnityEngine;

public class EnemyState_小呱呱_散步 : EnemyState_小呱呱
{
	private Vector2 _targetPos;

	private Vector2 _StartPos;

	private Vector2 _DirSpeed;

	public override void OnEnter()
	{
		base._Ani.Play("跳跃");
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
		_Timer = 0.3f;
		_DirSpeed = (_targetPos - _StartPos).normalized;
		base._Mono.transform.localScale = new Vector3((_DirSpeed.x > 0f) ? num : (0f - num), num, num);
		base._Rb2D.velocity = _DirSpeed * 6f;
		base._Mono组件._Tsf_移动目标.transform.position = _targetPos;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer < 0f)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}
}
