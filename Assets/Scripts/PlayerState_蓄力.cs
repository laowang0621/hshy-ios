using DSB;
using UnityEngine;

public class PlayerState_蓄力 : Player_StateBase
{
	private bool _Is释放蓄力;

	private float _移动速度_正常;

	private float _移动速度_疾奔;

	private float _当前移动速度;

	public PlayerState_蓄力()
	{
		_Data_Player_招式系数 = MyDataManager.F_Get_Player_招式系数(1011);
		_移动速度_正常 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.蓄力移动速度;
		_移动速度_疾奔 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.正常移动速度;
	}

	public override void OnEnter()
	{
		_Data._是影身蓄力 = false;
		base._Mono组件._Player_蓄力特效.gameObject.SetActive(value: true);
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(103) > 0.1f)
		{
			_当前移动速度 = _移动速度_疾奔;
			MyLog.LogError($"蓄力>>疾奔>>{_当前移动速度}");
		}
		else
		{
			_当前移动速度 = _移动速度_正常;
			MyLog.LogError($"蓄力>>正常>>{_当前移动速度}");
		}
		_Is释放蓄力 = false;
		_Timer = 0.4f;
		_Data._能保留豆子 = false;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_豆子_Change._EventID, F_豆子变化时);
	}

	public void F_豆子变化时(DSB_EventArgs args)
	{
		if (((EventArgs_Player_豆子_Change)args)._当前Val != 0)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_蓄力加豆子时();
		}
	}

	public override void OnExit()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_Player_豆子_Change._EventID, F_豆子变化时);
		base._Mono组件._Player_蓄力特效.gameObject.SetActive(value: false);
		if (!_Data._能保留豆子)
		{
			_Data._释放豆子 = _Data._Curr豆子;
			_Data._Curr豆子 = 0;
			_Data._Curr蓄力 = 0f;
		}
	}

	public override void OnUpdate()
	{
		if (F_PlayerMove(_当前移动速度))
		{
			base._Ani.Play("跑步");
		}
		else
		{
			base._Ani.Play("站立");
		}
		_Timer -= Time.deltaTime;
		if (_Is释放蓄力 && _Timer <= 0f)
		{
			ChangeToState(Player_StateName_Type.释放蓄力);
		}
		_Data._Curr气力 -= _Data_Player_招式系数._气力消耗 * Time.deltaTime;
		if (_Data._Curr气力 <= 0f)
		{
			_Is释放蓄力 = true;
		}
		if (_Data._Curr豆子 != 3)
		{
			_Data._Curr蓄力 += (_Data_Player_招式系数._棍势值 + _Data._蓄力额外速度) * Time.deltaTime;
		}
	}

	public override void F_重攻击Up()
	{
		_Is释放蓄力 = true;
	}

	public override void F_翻滚()
	{
		base._Mono组件._Player_蓄力特效.gameObject.SetActive(value: false);
		ChangeState(Player_StateName_Type.翻滚);
	}
}
