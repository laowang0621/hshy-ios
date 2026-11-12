using UnityEngine;

public class C_黄眉金身反击 : MonoBehaviour
{
	private int _几帧后关闭;

	private void OnEnable()
	{
		_几帧后关闭 = 3;
	}

	private void FixedUpdate()
	{
		if (_几帧后关闭-- == 0)
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
