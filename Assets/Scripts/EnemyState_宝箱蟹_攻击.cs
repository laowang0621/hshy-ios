using UnityEngine;

public class EnemyState_宝箱蟹_攻击 : EnemyState_宝箱蟹
{
	private bool _IsOK_攻击冲击力;

	private float _攻击冲击力Timer;

	public override void OnEnter()
	{
		base._Mono.F_朝向玩家();
		_Data._Mono.transform.localScale = new Vector3((base._Mono._Dir.x < 0f) ? 1 : (-1), 1f, 1f);
		base._Ani.Play("攻击");
		_Timer = _Data._Dict_Anis_name_time["攻击"];
		_IsOK_攻击冲击力 = false;
		_攻击冲击力Timer = 0f;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (!_IsOK_攻击冲击力)
		{
			_攻击冲击力Timer += Time.deltaTime;
			if (_攻击冲击力Timer > 0.15f)
			{
				_IsOK_攻击冲击力 = false;
				base._Rb2D.velocity = new Vector2(((base._Mono._Dir.x > 0f) ? 1 : (-1)) * 8, 0f);
			}
		}
		if (_Timer < 0f)
		{
			base._Mono.ChangeState(EnemyStateName_Type.散步);
		}
	}
}
