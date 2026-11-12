using UnityEngine;

public class EnemyState_沙国王_受击 : EnemyState_沙国王
{
	public override void OnEnter()
	{
		if (_mMono_沙国王._在地面)
		{
			base._Ani.Play("受击");
			base._Mono.transform.localScale = new Vector3((base._Mono._BeAttack_Pos.x > base._Mono.transform.position.x) ? 1 : (-1), 1f, 1f);
			_Timer = 1.5f;
		}
		else
		{
			base._Ani.Play("眩晕");
			_Timer = 4.5f;
			_mMono_沙国王._在地面 = true;
		}
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}
}
