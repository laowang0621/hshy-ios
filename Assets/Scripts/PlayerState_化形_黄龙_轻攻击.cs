using System.Collections;
using UnityEngine;

public class PlayerState_化形_黄龙_轻攻击 : PlayerState_化形_轻攻击Base
{
	public PlayerState_化形_黄龙_轻攻击()
	{
		攻击_1 = MyDataManager.F_Get_Player_招式系数(200301);
		攻击_2 = MyDataManager.F_Get_Player_招式系数(200302);
		攻击_3 = MyDataManager.F_Get_Player_招式系数(200303);
	}

	protected override IEnumerator Cor_攻击_1()
	{
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		_Data._重攻击衔接切手技 = true;
		F_攻击前配置(攻击_1, "Player_攻击_1", 0.5f, 0.2f, 0.22f);
		yield return new WaitForSeconds(0.13f);
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 12, 0f);
	}

	protected override IEnumerator Cor_攻击_2()
	{
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		_Data._重攻击衔接切手技 = true;
		F_攻击前配置(攻击_2, "Player_攻击_2", 1f, 0.2f, 0.3f);
		yield return new WaitForSeconds(0.13f);
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 12, 0f);
		yield return new WaitForSeconds(0.24f);
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 12, 0f);
	}

	protected override IEnumerator Cor_攻击_3()
	{
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		_Data._重攻击衔接切手技 = true;
		F_攻击前配置(攻击_3, "Player_攻击_3", 0.67f, 0.2f, 0.22f);
		yield return new WaitForSeconds(0.13f);
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 15, 0f);
	}

	public override void F_重攻击Down()
	{
		if (_NextState != TypeName_化形.重攻击)
		{
			if (!_已经缩短过后摇)
			{
				_已经缩短过后摇 = true;
				_Timer -= _该段后摇可缩短时间;
			}
			_Data._重击选项 = Enum_化形_重攻击.识破;
			_NextState = TypeName_化形.重攻击;
		}
	}
}
