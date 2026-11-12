using DSB;
using UnityEngine;

public class C_Test_血包 : MonoBehaviour
{
	private Transform _tsf_Player;

	public float _恢复量 = 10f;

	private void Start()
	{
		_tsf_Player = DSB_MonoSingleton<GameManager>.Instance._PC.transform;
	}

	public void F_设置回复量(float val)
	{
		_恢复量 = val;
	}

	private void Update()
	{
		Vector2 vector = _tsf_Player.position + new Vector3(0f, 1.2f, 2f) - base.transform.position;
		float magnitude = vector.magnitude;
		if (magnitude < 0.55f)
		{
			if (DSB_MonoSingleton<GameManager>.Instance._PC._FSM.currStateName != Player_StateName_Type.死亡)
			{
				DSB_MonoSingleton<GameManager>.Instance._PC._Data._Curr生命 += _恢复量;
			}
			Object.Destroy(base.gameObject);
		}
		else
		{
			base.transform.Translate(vector / magnitude * Time.deltaTime * 15f);
		}
	}
}
