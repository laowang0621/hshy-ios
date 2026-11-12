using DSB;
using UnityEngine;

public class PlayerState_完美闪避 : Player_StateBase
{
	public float _翻滚速度;

	public PlayerState_完美闪避()
	{
		_翻滚速度 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.翻滚速度;
	}

	public override void OnEnter()
	{
		base._Rb2D.drag = 0f;
		base._Rb2D.velocity = Vector2.zero;
		Object.Instantiate(base._Mono组件._GO_完美闪避残影Prefab, base._Mono.transform.position, Quaternion.identity);
		base._Ani.Play("完美闪避");
		if (_Data._Dir == Vector2.zero)
		{
			base._Rb2D.velocity = new Vector2((float)(_Data._朝向右边 ? 1 : (-1)) * _翻滚速度, 0f);
		}
		else
		{
			base._Rb2D.velocity = _Data._Dir * _翻滚速度;
		}
		F_玩家转向(0f - base._Rb2D.velocity.x);
		_Data._可以受击 = false;
		_Timer = 0.3f;
		_NextState = Player_StateName_Type.正常移动;
		_Data._State_攻击连段_判定Timer = _Timer + 0.3f;
		_Data._State_攻击连段_判定 = Player_StateName_Type.轻棍3;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_完美闪避();
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
	}

	public override void OnExit()
	{
		base._Rb2D.drag = 5f;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		_Data._可以受击 = true;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}

	public override void F_跳跃Down()
	{
		ChangeState(Player_StateName_Type.跳跃);
	}

	public override void F_轻攻击Down()
	{
		ChangeToState(Player_StateName_Type.轻棍3);
	}

	public override void F_重攻击Down()
	{
		ChangeState(Player_StateName_Type.蓄力);
	}

	public override void F_翻滚()
	{
		ChangeState(Player_StateName_Type.翻滚);
	}
}
