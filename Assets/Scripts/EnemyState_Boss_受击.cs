using UnityEngine;

public class EnemyState_Boss_受击 : EnemyState_Boss
{
	public int _AniID_受击 = Animator.StringToHash("受击");

	public override void OnEnter()
	{
		base._Ani.Play(_AniID_受击);
		base._Mono.transform.localScale = new Vector3((base._Mono._BeAttack_Pos.x > base._Mono.transform.position.x) ? 1 : (-1), 1f, 1f);
		_Timer = base._Mono._受击僵直时长;
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
