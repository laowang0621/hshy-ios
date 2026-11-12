using System.Collections.Generic;
using UnityEngine;

public class C_烟花_控制器 : MonoBehaviour
{
	[Header("大烟花")]
	public List<Sprite> _List_大烟花1;

	public List<Sprite> _List_大烟花2;

	public List<Sprite> _List_大烟花3;

	[Header("小烟花")]
	public List<Sprite> _List_小烟花;

	[Header("亮闪闪")]
	public List<Sprite> _List_亮闪闪;

	[Header("烟花生成区域")]
	public Transform _Tsf_烟花位置;

	private Vector2 _Pos_左下;

	private Vector2 _Pos_右上;

	private int _Max_Index_大烟花;

	private void Awake()
	{
		_Max_Index_大烟花 = Mathf.Min(_List_大烟花1.Count, _List_大烟花2.Count, _List_大烟花3.Count);
		Vector2 vector = _Tsf_烟花位置.position;
		vector.y -= 20.25f;
		Vector2 vector2 = _Tsf_烟花位置.localScale / 2f;
		_Pos_左下 = vector - vector2;
		_Pos_右上 = vector + vector2;
	}

	public (Sprite, Sprite, Sprite) F_得到烟花()
	{
		int index = Random.Range(0, _Max_Index_大烟花);
		return (_List_大烟花1[index], _List_大烟花2[index], _List_大烟花3[index]);
	}

	public Sprite F_得到小烟花()
	{
		return _List_小烟花[Random.Range(0, _List_小烟花.Count)];
	}

	public Sprite F_得到亮闪闪()
	{
		return _List_亮闪闪[Random.Range(0, _List_亮闪闪.Count)];
	}

	public Vector2 F_得到随机烟花位置()
	{
		return new Vector2(Random.Range(_Pos_左下.x, _Pos_右上.x), Random.Range(_Pos_左下.y, _Pos_右上.y));
	}
}
