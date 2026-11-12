using System.Collections;
using UnityEngine;

public class PlayerState_化形_广智_轻攻击 : PlayerState_化形_轻攻击Base
{
	public PlayerState_化形_广智_轻攻击()
	{
		攻击_1 = MyDataManager.F_Get_Player_招式系数(200101);
		攻击_2 = MyDataManager.F_Get_Player_招式系数(200102);
		攻击_3 = MyDataManager.F_Get_Player_招式系数(200103);
	}

	protected override IEnumerator Cor_攻击_1()
	{
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		F_攻击前配置(攻击_1, "Player_横扫", 0.63f, 0.3f, 0.12f);
		yield return new WaitForSeconds(0.1f);
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 12, 0f);
	}

	protected override IEnumerator Cor_攻击_2()
	{
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		F_攻击前配置(攻击_2, "Player_上挑", 0.46f, 0.3f, 0.12f);
		yield return new WaitForSeconds(0.1f);
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 12, 0f);
	}

	protected override IEnumerator Cor_攻击_3()
	{
		_Data._MonoPlayer.F_玩家转向(MyInputManager.Horizontal);
		yield return null;
		F_攻击前配置(攻击_3, "Player_下劈", 0.55f, 0.3f, 0.12f);
		yield return new WaitForSeconds(0.1f);
		_Data._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 15, 0f);
	}
}
