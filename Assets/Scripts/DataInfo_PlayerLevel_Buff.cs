using System;

[Serializable]
public class DataInfo_PlayerLevel_Buff
{
	public int _BuffID;

	public float _每一级数值;

	public string _升级描述;

	public int _升级提示类型;

	public int _每一级消耗点数 = 1;
}
