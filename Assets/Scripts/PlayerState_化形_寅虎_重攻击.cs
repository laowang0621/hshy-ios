using System.Collections;
using UnityEngine;

public class PlayerState_化形_寅虎_重攻击 : PlayerState_化形_Base
{
	private DataInfo_Player招式 蓄力;

	private DataInfo_Player招式 蓄力下劈;

	private bool _本次蓄力已经释放;

	public PlayerState_化形_寅虎_重攻击()
	{
		蓄力 = MyDataManager.F_Get_Player_招式系数(200205);
		蓄力下劈 = MyDataManager.F_Get_Player_招式系数(200206);
	}

	public override void OnEnter()
	{
		_Data._释放蓄力状态 = true;
		_本次蓄力已经释放 = false;
		_Data._Rb2D.mass = 999f;
		_Data._Mono组件_化形._Ani.Play("Player_蓄力", 0, 0f);
	}

	public override void OnUpdate()
	{
		if (!_本次蓄力已经释放)
		{
			_Data._Curr神力 -= 蓄力._气力消耗 * Time.deltaTime;
			_Data._Curr蓄力 += 蓄力._棍势值 * Time.deltaTime;
			if (_Data._Curr神力 <= 0f)
			{
				_Data.F_退出化形();
			}
		}
	}

	public override void OnExit()
	{
		_Data._释放蓄力状态 = false;
		_Data._Rb2D.mass = 5f;
	}

	public override void F_重攻击Up()
	{
		if (!_本次蓄力已经释放)
		{
			_Data.F_Move_不播放动画();
			_Data.F_设置攻击(蓄力下劈);
			float num = _Data._Curr蓄力 / _Data._Max蓄力;
			if (num < 0.2f)
			{
				num = 0.2f;
			}
			_Data._Mono组件_化形._C_攻击判断._伤害 *= num;
			_Data._Mono组件_化形._C_攻击判断._僵直伤害 *= num;
			_Data._Curr蓄力 = 0f;
			_Data._Mono组件_化形.StartCoroutine(Cor_蓄力下劈());
			_Data._释放蓄力状态 = true;
			_本次蓄力已经释放 = true;
		}
	}

	private IEnumerator Cor_蓄力下劈()
	{
		_Data._Mono组件_化形._Ani.Play("Player_蓄力下劈");
		Vector2 currDir = new Vector2(_Data._朝向右边 ? 1 : (-1), 0f);
		yield return new WaitForSeconds(0.25f);
		_Data._Rb2D.velocity = currDir * 16f;
		yield return new WaitForSeconds(0.5f);
		F_化形_ChangeState(TypeName_化形.正常移动);
	}
}
