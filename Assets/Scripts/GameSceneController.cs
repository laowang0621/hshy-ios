using DSB;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
	public Transform _Tsf_背景层远1;

	public Transform _Tsf_背景层远;

	public Transform _Tsf_背景层近;

	public Transform _Tsf_场景层;

	public Transform _Tsf_前景层;

	public Transform _Tsf_左边界;

	public Transform _Tsf_右边界;

	public Transform _Tsf_上边界;

	public Transform _Tsf_下边界;

	public void F_初始化()
	{
		DSB_MonoSingleton<CameraController>.Instance._On_LateUpdate += OnLateUpdate;
		F_更新边界();
	}

	public void F_更新边界()
	{
		DSB_MonoSingleton<CameraController>.Instance.F_刷新边界(_Tsf_左边界.position, _Tsf_右边界.position, _Tsf_上边界.position, _Tsf_下边界.position);
	}

	private void OnDisable()
	{
		DSB_MonoSingleton<CameraController>.Instance._On_LateUpdate -= OnLateUpdate;
	}

	private void OnLateUpdate(float playerX)
	{
		F_前景层(playerX);
		F_背景层近(playerX);
		F_背景层远(playerX);
		F_背景层远1(playerX);
	}

	private void F_前景层(float playerX)
	{
		float x = (0f - playerX) * 0.2f;
		_Tsf_前景层.position = new Vector2(x, 0f);
	}

	private void F_背景层近(float playerX)
	{
		float x = playerX * 0.3f;
		_Tsf_背景层近.position = new Vector2(x, 0f);
	}

	private void F_背景层远(float playerX)
	{
		float x = playerX * 0.2f;
		_Tsf_背景层远.position = new Vector2(x, 0f);
	}

	private void F_背景层远1(float playerX)
	{
		float x = playerX * 0.1f;
		_Tsf_背景层远1.position = new Vector2(x, 0f);
	}
}
