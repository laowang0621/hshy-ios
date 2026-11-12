using UnityEngine;

public class C_是马楼就下地狱100层_墙壁 : MonoBehaviour
{
	public float _间距 = -15.5f;

	public Transform _Tsf_锚点;

	protected float _镜头大小;

	protected int _当前最上层序号;

	protected int _当前最下层序号;

	public virtual void F_Init()
	{
		for (int i = 0; i < _Tsf_锚点.childCount; i++)
		{
			_Tsf_锚点.GetChild(i).localPosition = new Vector3(0f, (float)i * _间距);
			_当前最下层序号 = i;
		}
		_当前最上层序号 = 0;
		_镜头大小 = Lunch._镜头默认大小;
	}

	public virtual void F_镜头移动调用(float 镜头y)
	{
		if (_Tsf_锚点.GetChild(_当前最下层序号 % _Tsf_锚点.childCount).position.y > 镜头y - _镜头大小)
		{
			_Tsf_锚点.GetChild(_当前最上层序号 % _Tsf_锚点.childCount).localPosition = new Vector3(0f, (float)(_当前最下层序号 + 1) * _间距);
			_当前最上层序号++;
			_当前最下层序号++;
		}
	}

	public Vector2 F_获得最下层的位置()
	{
		return _Tsf_锚点.GetChild(_当前最下层序号 % _Tsf_锚点.childCount).position;
	}
}
