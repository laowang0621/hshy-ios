using System.Collections;
using UnityEngine;

public class PlayerState_化形_寅虎_轻攻击 : PlayerState_化形_轻攻击Base
{
	public PlayerState_化形_寅虎_轻攻击()
	{
		攻击_1 = MyDataManager.F_Get_Player_招式系数(200201);
		攻击_2 = MyDataManager.F_Get_Player_招式系数(200202);
		攻击_3 = MyDataManager.F_Get_Player_招式系数(200203);
	}

	public override void F_翻滚()
	{
		F_化形_ChangeState(TypeName_化形.闪避);
	}

	protected override IEnumerator Cor_攻击_1()
	{
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		F_攻击前配置(攻击_1, "Player_攻击_1", 1.21f, 0.3f, 0.32f);
		yield return new WaitForSeconds(0.44f);
		_Data.F_Move_不播放动画();
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 14, 0f);
	}

	protected override IEnumerator Cor_攻击_2()
	{
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		F_攻击前配置(攻击_2, "Player_攻击_2", 0.71f, 0.3f, 0.22f);
		yield return new WaitForSeconds(0.15f);
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 14, 0f);
	}

	protected override IEnumerator Cor_攻击_3()
	{
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		F_攻击前配置(攻击_3, "Player_攻击_3", 1.23f, 0.3f, 0.22f);
		yield return new WaitForSeconds(0.44f);
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 18, 0f);
	}
}
