using System.Collections;
using DSB;
using UnityEngine;

public class PlayerState_释放蓄力 : Player_StateBase
{
	private Coroutine _Cor_动作中向前力;

	private DataInfo_Player招式 _Data_Player招式_系数_0;

	private DataInfo_Player招式 _Data_Player招式_系数_1;

	private DataInfo_Player招式 _Data_Player招式_系数_2;

	private DataInfo_Player招式 _Data_Player招式_系数_3;

	public PlayerState_释放蓄力()
	{
		_Data_Player招式_系数_0 = MyDataManager.F_Get_Player_招式系数(1005);
		_Data_Player招式_系数_1 = MyDataManager.F_Get_Player_招式系数(1006);
		_Data_Player招式_系数_2 = MyDataManager.F_Get_Player_招式系数(1007);
		_Data_Player招式_系数_3 = MyDataManager.F_Get_Player_招式系数(1008);
	}

	public override void OnEnter()
	{
		if (_Cor_动作中向前力 != null)
		{
			base._Mono.StopCoroutine(_Cor_动作中向前力);
		}
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_蓄力时();
		_NextState = Player_StateName_Type.正常移动;
		_Data._释放蓄力状态 = true;
		base._Mono组件._C_攻击判断._击退力度 = 15f;
		bool 是强破坏力 = false;
		if (_Data._释放豆子 == 1)
		{
			_Data_Player_招式系数 = _Data_Player招式_系数_1;
			base._Ani.Play("蓄力攻击0");
			_Timer = _Data._Dict_AniName_time["蓄力攻击0"];
			_Cor_动作中向前力 = base._Mono.StartCoroutine(Cor_动作中向前力_0());
		}
		else if (_Data._释放豆子 == 2)
		{
			_Data_Player_招式系数 = _Data_Player招式_系数_2;
			base._Ani.Play("蓄力攻击1");
			_Timer = _Data._Dict_AniName_time["蓄力攻击1"];
			_Cor_动作中向前力 = base._Mono.StartCoroutine(Cor_动作中向前力_1());
		}
		else if (_Data._释放豆子 == 3)
		{
			_Data_Player_招式系数 = _Data_Player招式_系数_3;
			base._Ani.Play("蓄力攻击2");
			_Timer = _Data._Dict_AniName_time["蓄力攻击2"];
			是强破坏力 = true;
			_Cor_动作中向前力 = base._Mono.StartCoroutine(Cor_动作中向前力_2());
		}
		else
		{
			_Data_Player_招式系数 = _Data_Player招式_系数_0;
			base._Ani.Play("蓄力攻击0");
			_Timer = _Data._Dict_AniName_time["蓄力攻击0"];
			_Cor_动作中向前力 = base._Mono.StartCoroutine(Cor_动作中向前力_0());
		}
		F_创建攻击命中回调(是强破坏力);
	}

	public override void F_创建攻击命中回调(DataInfo_Player招式 Data_Player招式_系数, bool 是强破坏力 = false)
	{
		float num = 1f + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1302);
		if (_Data._是影身蓄力)
		{
			base._Mono组件._C_攻击判断._伤害 = _Data._攻击力 * Data_Player招式_系数._伤害系数 * num + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1701);
		}
		else
		{
			base._Mono组件._C_攻击判断._伤害 = _Data._攻击力 * Data_Player招式_系数._伤害系数 * num;
		}
		_Data._Curr气力 -= Data_Player招式_系数._气力消耗;
		base._Mono组件._C_攻击判断._攻击命中回调 += F_攻击命中回调;
		base._Mono组件._C_攻击判断._僵直伤害 = Data_Player招式_系数._僵直伤害;
		base._Mono组件._C_攻击判断._是强破坏力 = 是强破坏力;
	}

	public override void F_攻击命中回调()
	{
		if (_Data._释放豆子 == 1)
		{
			DSB_MonoSingleton<CameraController>.Instance.F_屏幕震动(0.7f, 0.08f, 13);
		}
		else if (_Data._释放豆子 == 2)
		{
			DSB_MonoSingleton<CameraController>.Instance.F_屏幕震动(0.8f, 0.1f, 14);
		}
		else if (_Data._释放豆子 == 3)
		{
			DSB_MonoSingleton<CameraController>.Instance.F_屏幕震动(0.9f, 0.12f, 15);
		}
		else
		{
			DSB_MonoSingleton<CameraController>.Instance.F_屏幕震动(0.6f, 0.06f, 12);
		}
	}

	public override void OnExit()
	{
		base._Mono组件._C_攻击判断._攻击命中回调 -= F_攻击命中回调;
		_Data._释放蓄力状态 = false;
		base._Mono组件._Player_蓄力特效.gameObject.SetActive(value: false);
		if (_Cor_动作中向前力 != null)
		{
			base._Mono.StopCoroutine(_Cor_动作中向前力);
			_Cor_动作中向前力 = null;
		}
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}

	private IEnumerator Cor_动作中向前力()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 12, 0f);
		yield return new WaitForSeconds(0.4f);
		F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 8, 0f);
		yield return new WaitForSeconds(0.35f);
		yield return null;
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 16, 0f);
	}

	private IEnumerator Cor_动作中向前力_0()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 12, 0f);
		yield return new WaitForSeconds(0.5f);
		base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 12, 0f);
		yield return new WaitForSeconds(0.3f);
	}

	private IEnumerator Cor_动作中向前力_1()
	{
		base._Rb2D.velocity = Vector2.zero;
		for (int i = 0; i < 6; i++)
		{
			base._Rb2D.velocity = new Vector2((float)(_Data._朝向右边 ? 1 : (-1)) * (14f - (float)i * 1f), 0f);
			yield return new WaitForSeconds(0.075f);
		}
		yield return new WaitForSeconds(0.55f);
	}

	private IEnumerator Cor_动作中向前力_2()
	{
		base._Rb2D.velocity = Vector2.zero;
		for (int i = 0; i < 8; i++)
		{
			base._Rb2D.velocity = new Vector2((float)(_Data._朝向右边 ? 1 : (-1)) * (18f - (float)i * 1.35f), 0f);
			yield return new WaitForSeconds(0.08f);
		}
		yield return new WaitForSeconds(0.46f);
	}
}
