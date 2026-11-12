using UnityEngine;

public class C_弹道_赤尻马猴_黑边血色剑气 : C_弹道
{
	private Vector2 _Dir = Vector2.right;

	public void F_原地保留(Vector2 dir, float 伤害)
	{
		_Dir = dir;
		_伤害 = 伤害;
	}

	public void F_激发()
	{
		F_Init(_Dir);
	}
}
