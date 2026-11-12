using UnityEngine;

public class EnemyAI_百眼魔君_符纸 : MonoBehaviour
{
	private Transform _Tsf_符纸;

	private Transform _Tsf_跟随锚点;

	[Header("跟随参数")]
	private float _加速度 = 28f;

	private float _最大跟随速度 = 50f;

	private float _最大倾斜角度 = 75f;

	private float _倾斜响应速度 = 25f;

	private float _当前跟随速度;

	private Vector3 _当前速度向量;

	private Quaternion _目标倾斜旋转;

	private Quaternion _默认锚点旋转 = Quaternion.identity;

	private bool _是否隐藏;

	private void Start()
	{
		if (base.transform.parent == null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		_Tsf_符纸 = base.transform;
		_Tsf_跟随锚点 = base.transform.parent;
		base.transform.parent = null;
		_Tsf_符纸.position = _Tsf_跟随锚点.position;
		_Tsf_符纸.rotation = _Tsf_跟随锚点.rotation;
		_当前速度向量 = Vector3.zero;
		_是否隐藏 = false;
	}

	public void F_隐藏()
	{
		_是否隐藏 = true;
	}

	public void F_归位()
	{
		_是否隐藏 = false;
	}

	private void Update()
	{
		if (_Tsf_跟随锚点 == null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		Vector3 vector;
		if (_是否隐藏)
		{
			vector = new Vector2(0f, 1f);
			if (base.transform.position.y > 20f)
			{
				return;
			}
		}
		else
		{
			vector = _Tsf_跟随锚点.position - _Tsf_符纸.position;
		}
		if (vector.magnitude > 0.2f)
		{
			_当前跟随速度 = Mathf.Min(_当前跟随速度 + _加速度 * Time.deltaTime, _最大跟随速度);
			_当前速度向量 = Vector3.Lerp(_当前速度向量, vector.normalized * _当前跟随速度, 5f * Time.deltaTime);
			_Tsf_符纸.position += _当前速度向量 * Time.deltaTime;
			Vector3 vector2 = new Vector3(_当前速度向量.x, 0f, _当前速度向量.z);
			float num = Mathf.Clamp(vector2.magnitude / _最大跟随速度, 0f, 1f) * _最大倾斜角度;
			Vector3 axis = Vector3.Cross(vector2.normalized, Vector3.up);
			_目标倾斜旋转 = Quaternion.AngleAxis(0f - num, axis) * _默认锚点旋转;
		}
		else
		{
			_当前跟随速度 = Mathf.Lerp(_当前跟随速度, 0f, 10f * Time.deltaTime);
			_目标倾斜旋转 = _默认锚点旋转;
		}
		_Tsf_符纸.rotation = Quaternion.Slerp(_Tsf_符纸.rotation, _目标倾斜旋转, _倾斜响应速度 * Time.deltaTime);
	}
}
