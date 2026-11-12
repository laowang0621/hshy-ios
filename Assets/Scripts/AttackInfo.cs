using UnityEngine;

public class AttackInfo
{
	public float atk_生命;

	public float atk_僵直;

	public float atk_击退力度;

	public Vector2 atk_Pos = Vector2.zero;

	public bool atk_Is远程;

	public bool atk_Is地面;

	public bool atk_强破坏力;

	public bool atk_有僵直 = true;

	public bool atk_硬直是虚弱动作;

	public bool atk_可以被闪避 = true;

	public bool atk_强制受击;

	public float atk_受击时长 = -1f;

	public AttackInfo()
	{
	}

	public AttackInfo(float atk_生命)
	{
		this.atk_生命 = atk_生命;
	}
}
