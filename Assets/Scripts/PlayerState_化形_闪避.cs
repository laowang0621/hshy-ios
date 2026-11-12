using DSB;
using UnityEngine;

public class PlayerState_化形_闪避 : PlayerState_化形_Base
{
	public int _闪避动画名称 = Animator.StringToHash("闪避");

	protected TypeName_化形 _NextState;

	public override void OnEnter()
	{
		_Data._Rb2D.drag = 0f;
		_Data._不可受击状态 = true;
		_NextState = TypeName_化形.正常移动;
		_Data.F_Move_不播放动画();
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_广智闪避();
		if (_Data._Dir == Vector2.zero)
		{
			_Data._Rb2D.velocity = new Vector2((float)(_Data._朝向右边 ? 1 : (-1)) * _Data._闪避速度, 0f);
		}
		else
		{
			_Data._Rb2D.velocity = _Data._Dir * _Data._闪避速度;
		}
		_Data._Curr神力 -= _Data._闪避消耗神力;
		_Timer = _Data._闪避持续时间;
		_Data._Mono组件_化形._Ani.Play(_闪避动画名称);
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
	}

	public override void OnExit()
	{
		_Data._不可受击状态 = false;
		_Data._Rb2D.drag = 5f;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			F_化形_ChangeState(_NextState);
		}
	}
}
