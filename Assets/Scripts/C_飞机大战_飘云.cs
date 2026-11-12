using System.Collections.Generic;
using UnityEngine;

public class C_飞机大战_飘云 : MonoBehaviour
{
	public float _云速度;

	public Transform _Tsf_List_云;

	private List<Transform> _List_云;

	public Transform _Tsf_左上;

	public Transform _Tsf_右下;

	public Transform _Tsf_缓冲区间;

	private Vector2 _Pos_左上;

	private Vector2 _Pos_右下;

	private float _X_缓冲区间;

	private void Awake()
	{
		_Pos_左上 = _Tsf_左上.transform.position;
		_Pos_右下 = _Tsf_右下.transform.position;
		_X_缓冲区间 = _Tsf_缓冲区间.transform.position.x;
		_List_云 = new List<Transform>();
		foreach (Transform item in _Tsf_List_云)
		{
			_List_云.Add(item);
		}
	}

	private void Update()
	{
		foreach (Transform item in _List_云)
		{
			item.position -= new Vector3(_云速度, 0f, 0f) * Time.deltaTime;
			if (item.position.x < _Pos_左上.x)
			{
				float x = Random.Range(_Pos_右下.x, _X_缓冲区间);
				float y = Random.Range(_Pos_右下.y, _Pos_左上.y);
				item.position = new Vector2(x, y);
			}
		}
	}
}
