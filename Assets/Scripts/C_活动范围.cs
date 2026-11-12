using UnityEngine;

public class C_活动范围 : MonoBehaviour
{
	private Vector2 _Pos1;

	private Vector2 _Pos2;

	private void Start()
	{
		BoxCollider2D component = GetComponent<BoxCollider2D>();
		Vector2 vector = base.transform.position;
		Vector2 vector2 = component.size / 2f;
		_Pos1 = vector - vector2;
		_Pos2 = vector + vector2;
	}

	public Vector2 F_得到范围内随机点()
	{
		return new Vector2(Random.Range(_Pos1.x, _Pos2.x), Random.Range(_Pos1.y, _Pos2.y));
	}
}
