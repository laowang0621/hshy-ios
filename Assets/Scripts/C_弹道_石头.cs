using UnityEngine;

public class C_弹道_石头 : C_弹道_弓箭
{
	public override void F_Init(Vector2 dir, float 攻击力)
	{
		base.F_Init(dir, 攻击力);
		MyLog.LogError("修改阵营");
	}

	public override void F_Init(Vector2 dir, float 攻击力, IBeAttack 排除对象)
	{
		_阵营 = 3;
		base.F_Init(dir, 攻击力, 排除对象);
	}
}
