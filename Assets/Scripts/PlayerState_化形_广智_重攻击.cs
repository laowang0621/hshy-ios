using System.Collections;
using UnityEngine;

public class PlayerState_化形_广智_重攻击 : PlayerState_化形_Base
{
	private DataInfo_Player招式 突刺;

	private DataInfo_Player招式 撑杆跳;

	private DataInfo_Player招式 大风车;

	public PlayerState_化形_广智_重攻击()
	{
		突刺 = MyDataManager.F_Get_Player_招式系数(200104);
		大风车 = MyDataManager.F_Get_Player_招式系数(200105);
		撑杆跳 = MyDataManager.F_Get_Player_招式系数(200106);
	}

	public override void OnEnter()
	{
		_Data._释放蓄力状态 = true;
		if (_Data._重击选项 == Enum_化形_重攻击.普通)
		{
			_Data.F_设置攻击(突刺);
			_Data._Mono组件_化形.StartCoroutine(Cor_突刺());
		}
		else if (_Data._重击选项 == Enum_化形_重攻击.撑杆跳)
		{
			_Data.F_设置攻击(撑杆跳);
			_Data._Curr蓄力 = 0f;
			_Data._Mono组件_化形.StartCoroutine(Cor_撑杆跳());
		}
		else if (_Data._重击选项 == Enum_化形_重攻击.大风车)
		{
			_Data.F_设置攻击(大风车);
			_Data._Curr蓄力 = 0f;
			_Data._Mono组件_化形.StartCoroutine(Cor_大风车());
		}
	}

	public override void OnExit()
	{
		_Data._重击选项 = Enum_化形_重攻击.普通;
		_Data._释放蓄力状态 = false;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		_Data._Mono组件_化形.StopAllCoroutines();
		_Data._Rb2D.drag = 5f;
	}

	private IEnumerator Cor_突刺()
	{
		_Data._Mono组件_化形._Ani.Play("突刺_0");
		Vector2 currDir = ((!(_Data._Dir == Vector2.zero)) ? _Data._Dir : new Vector2(_Data._朝向右边 ? 1 : (-1), 0f));
		yield return new WaitForSeconds(0.5f);
		_Data._Mono组件_化形._Ani.Play("突刺_1");
		_Data._Rb2D.drag = 0f;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		_Data._Rb2D.velocity = currDir * 44f;
		yield return new WaitForSeconds(0.15888f);
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		_Data._Rb2D.drag = 5f;
		_Data._Mono组件_化形._Ani.Play("突刺_2");
		yield return new WaitForSeconds(0.3f);
		F_化形_ChangeState(TypeName_化形.正常移动);
	}

	private IEnumerator Cor_撑杆跳()
	{
		_Data._Mono组件_化形._Ani.Play("撑杆跳");
		Vector2 currDir3 = new Vector2(_Data._朝向右边 ? 1 : (-1), 0f);
		yield return null;
		_Data._Rb2D.velocity = currDir3 * 10f;
		yield return new WaitForSeconds(0.2f);
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		currDir3 = new Vector2(_Data._朝向右边 ? 1 : (-1), 0f);
		yield return null;
		_Data._Rb2D.velocity = currDir3 * 15f;
		yield return new WaitForSeconds(0.25f);
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		currDir3 = new Vector2(_Data._朝向右边 ? 1 : (-1), 0f);
		yield return null;
		_Data._Rb2D.velocity = currDir3 * 20f;
		yield return new WaitForSeconds(0.17f);
		Object.Instantiate(_Data._Mono组件_化形._拍地板特效Prefab, _Data._Mono组件_化形.transform.position + new Vector3((float)(_Data._朝向右边 ? 1 : (-1)) * 2.75f, 0f, 0f), Quaternion.identity);
		yield return new WaitForSeconds(0.35f);
		F_化形_ChangeState(TypeName_化形.正常移动);
	}

	private IEnumerator Cor_大风车()
	{
		_Data._Mono组件_化形._Ani.Play("大风车");
		Vector2 currDir = ((!(_Data._Dir == Vector2.zero)) ? _Data._Dir : new Vector2(_Data._朝向右边 ? 1 : (-1), 0f));
		for (int i = 0; i < 8; i++)
		{
			_Data._Rb2D.velocity = currDir * 15f;
			yield return new WaitForSeconds(0.075f);
		}
		F_化形_ChangeState(TypeName_化形.正常移动);
	}

	public override void OnUpdate()
	{
	}
}
