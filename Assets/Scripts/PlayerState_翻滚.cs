using DSB;
using UnityEngine;

public class PlayerState_翻滚 : Player_StateBase
{
	private float _EnterDrag;

	private float _完美闪避触发时间 = 0.175f;

	private float _完美闪避触发时间timer;

	public float _翻滚速度;

	public PlayerState_翻滚()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1012);
		_翻滚速度 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.翻滚速度;
	}

	public override void OnEnter()
	{
		_EnterDrag = base._Rb2D.drag;
		base._Rb2D.drag = 0f;
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("翻滚");
		if (_Data._Dir == Vector2.zero)
		{
			base._Rb2D.velocity = new Vector2((float)(_Data._朝向右边 ? 1 : (-1)) * _翻滚速度, 0f);
		}
		else
		{
			base._Rb2D.velocity = _Data._Dir * _翻滚速度;
		}
		_Data._触发完美闪避 = true;
		_Data._可以受击 = false;
		_完美闪避触发时间timer = _完美闪避触发时间;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_跳跃_翻滚();
		_Timer = 0.315f;
		_NextState = Player_StateName_Type.正常移动;
		_Data._Curr气力 -= _Data_Player_招式系数._气力消耗;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
	}

	public override void OnExit()
	{
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		base._Rb2D.drag = _EnterDrag;
		_Data._触发完美闪避 = false;
		_Data._可以受击 = true;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
		_完美闪避触发时间timer -= Time.deltaTime;
		if (_完美闪避触发时间timer <= 0f && _Data._触发完美闪避)
		{
			_Data._触发完美闪避 = false;
		}
	}
}
