using UnityEngine;

public class Player_完美闪避残影 : MonoBehaviour
{
	private void Start()
	{
		Invoke("F_延时销毁", 2f);
	}

	private void F_延时销毁()
	{
		Object.Destroy(base.gameObject);
	}
}
