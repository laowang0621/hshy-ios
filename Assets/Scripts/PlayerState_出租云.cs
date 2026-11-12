using UnityEngine;

public class PlayerState_出租云 : Player_StateBase
{
	private C_出租云 _出租云;

	private bool _成功释放;

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("滴滴");
		_Timer = _Data._Dict_AniName_time["滴滴"];
		_NextState = Player_StateName_Type.正常移动;
		_成功释放 = false;
		if (_出租云 != null)
		{
			_出租云.F_起飞后销毁();
			_出租云 = null;
		}
	}

	public override void OnExit()
	{
		if (_成功释放)
		{
			_出租云 = Object.Instantiate(base._Mono组件._C_出租云Prefab, base._Mono组件.transform.position, Quaternion.identity);
		}
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			_成功释放 = true;
			ChangeState(_NextState);
		}
	}
}
