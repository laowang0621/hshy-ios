using System;

[Serializable]
public struct DataInfo_Player招式
{
	public int _招式ID;

	public string _招式Name;

	public float _伤害系数;

	public float _气力消耗;

	public float _僵直伤害;

	public float _棍势值;

	public float _击退力度;

	public static DataInfo_Player招式 operator *(DataInfo_Player招式 招式, float multiplier)
	{
		招式._伤害系数 *= multiplier;
		招式._僵直伤害 *= multiplier;
		招式._棍势值 *= multiplier;
		return 招式;
	}
}
