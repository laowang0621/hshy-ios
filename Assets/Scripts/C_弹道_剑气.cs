using UnityEngine;

public class C_弹道_剑气 : C_弹道
{
	[SerializeField]
	protected Transform _Tsf_Model;

	[SerializeField]
	protected Transform _Tsf_影子;

	public override void F_Init(Vector2 dir, float 攻击力)
	{
		base.F_Init(dir, 攻击力);
		_Tsf_Model.right = dir;
		_Tsf_影子.right = dir;
	}
}
