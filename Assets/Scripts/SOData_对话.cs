using System;
using System.Collections.Generic;
using UnityEngine;

public class SOData_对话 : ScriptableObject
{
	public List<DataInfo_对话框> _List_对话框;

	public List<DataInfo_一条对话> _List_对话内容;

	public DataInfo_一条对话 F_得到一条对话(int 对话ID)
	{
		foreach (DataInfo_一条对话 item in _List_对话内容)
		{
			if (item._对话ID == 对话ID)
			{
				return item;
			}
		}
		throw new Exception($"没有名称为<<{对话ID}>>的对话!");
	}

	public DataInfo_对话框 F_得到对话框(int 对话框数字ID)
	{
		foreach (DataInfo_对话框 item in _List_对话框)
		{
			if (item._说话人ID == 对话框数字ID)
			{
				return item;
			}
		}
		throw new Exception($"没有名称为<<{对话框数字ID}>>的对话!");
	}
}
