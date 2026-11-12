using DSB;
using UnityEngine;

public class C_弹道_弓箭 : C_弹道
{
	[SerializeField]
	public Transform _Tsf_Model;

	[SerializeField]
	protected Transform _Tsf_影子;

	private Vector2 _Dir;

	public override void F_Init(Vector2 dir, float 攻击力)
	{
		base.F_Init(dir, 攻击力);
		_Dir = dir;
		_Tsf_Model.right = dir;
		_Tsf_影子.right = dir;
	}

	public override void F_Init(Vector2 dir, float 攻击力, IBeAttack 排除对象)
	{
		base.F_Init(dir, 攻击力, 排除对象);
		_Tsf_Model.right = dir;
		_Tsf_影子.right = dir;
	}

	protected override void F_反弹(int 阵营)
	{
		_Tsf_Model.right = -_Dir;
		_RB2D.velocity *= -1f;
		_伤害 *= 3f;
		_阵营 = 阵营;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_反弹箭();
	}
}
