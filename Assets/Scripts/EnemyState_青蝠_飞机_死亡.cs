using UnityEngine;

public class EnemyState_青蝠_飞机_死亡 : EnemyState_狼剑客
{
	public override void OnEnter()
	{
		base._Ani.Play("倒地");
		_Data._Is死亡 = true;
		_Data.F_设置UI血量(1f, 1f, _可见性: false);
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		base._Mono组件._Rb2D.gravityScale = 5f;
		Object.Destroy(base._Mono组件.gameObject, 5f);
	}
}
