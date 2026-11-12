using UnityEngine;
using UnityEngine.UI;

public class ItemUI_文本 : ItemUIBase<ItemUI_文本>
{
	public Text _Text_文本;

	public Transform _TargetPos;

	public void F_SetText(string text)
	{
		_Text_文本.text = text;
	}

	public void F_SetColor(Color color)
	{
		_Text_文本.color = color;
	}

	public void F_SetTarget(Transform target)
	{
		_TargetPos = target;
	}

	public void LateUpdate()
	{
		if (_TargetPos != null)
		{
			F_SetPos(_TargetPos.position);
		}
	}
}
