using UnityEngine;

public class EnemyState_小呱呱_追击 : EnemyState_小呱呱
{
	private Vector2 _targetPos;

	private Vector2 _StartPos;

	public override void OnEnter()
	{
		if (!_Data._视野范围有敌人)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
			return;
		}
		base._Ani.Play("站立");
		base._Ani.Play("跳跃");
		_StartPos = base._Mono.transform.position;
		Vector2 vector = _Data._Pos_追击目标位置;
		_targetPos = vector + new Vector2((float)((vector.x < _StartPos.x) ? 1 : (-1)) * Random.Range(1f, 3f), Random.Range(-0.5f, 0.5f));
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.F_判断撞墙次数是否增加(_StartPos, _targetPos, out var dir);
		base._Mono.transform.localScale = new Vector3((dir.x > 0f) ? num : (0f - num), num, num);
		_Timer = 0.35f;
		base._Rb2D.velocity = dir * 8f;
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
			if (_Data._攻击范围有敌人 && Random.value < 0.45f)
			{
				base._Mono.ChangeState(EnemyStateName_Type.攻击);
			}
			else
			{
				base._Mono.ChangeState(EnemyStateName_Type.站立);
			}
		}
	}
}
