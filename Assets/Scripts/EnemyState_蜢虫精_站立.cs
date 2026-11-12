using UnityEngine;

public class EnemyState_蜢虫精_站立 : EnemyState_狼剑客
{
	private EnemyAI_蜢虫精 _Mono蜢虫精;

	private bool _会主动出击;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono蜢虫精 = (EnemyAI_蜢虫精)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		if (_Mono蜢虫精._茧状态)
		{
			base._Ani.Play("站立_茧", 0, Random.value);
			base._Rb2D.velocity = Vector2.zero;
			_会主动出击 = Random.value < 0.2f;
		}
		else
		{
			base._Ani.Play("站立");
			base._Rb2D.velocity = Vector2.zero;
			_Timer = Random.Range(_Data._站立时间_x, _Data._站立时间_y);
		}
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		if (_Mono蜢虫精._茧状态)
		{
			if (_会主动出击 && _Data._攻击范围有敌人)
			{
				_Mono蜢虫精.F_取消茧状态();
			}
			return;
		}
		_Timer -= Time.deltaTime;
		if (_Data._视野范围有敌人)
		{
			ChangeState(EnemyStateName_Type.追击);
		}
		if (_Timer <= 0f)
		{
			_Timer = Random.Range(_Data._站立时间_x, _Data._站立时间_y);
			ChangeState(EnemyStateName_Type.散步);
		}
	}
}
