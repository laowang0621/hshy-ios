using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataInfo_一条对话
{
	public int _对话ID;

	[HideInInspector]
	public string _只读_对话标题;

	public int _回调事件ID;

	[HideInInspector]
	public string _只读_回调事件;

	public List<DataInfo_对话内容_Item> _对话内容;
}
