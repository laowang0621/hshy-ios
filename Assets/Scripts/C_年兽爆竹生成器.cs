using UnityEngine;

public class C_年兽爆竹生成器 : MonoBehaviour
{
	public Transform _Tsf_生成范围;

	public C_爆竹 _爆竹Prefab;

	private Vector2 _Pos_左下;

	private Vector2 _Pos_右上;

	private bool _开始生成;

	private bool _等待上一个爆炸_已经爆炸;

	public Vector2 _生成间隔;

	private float _Curr生成间隔;

	private float _Timer = 1f;

	private void Awake()
	{
		_开始生成 = false;
		_等待上一个爆炸_已经爆炸 = false;
		Vector3 vector = _Tsf_生成范围.localScale / 2f;
		_Pos_左下 = _Tsf_生成范围.transform.position - vector;
		_Pos_右上 = _Tsf_生成范围.transform.position + vector;
	}

	private void OnDisable()
	{
		_开始生成 = false;
		_等待上一个爆炸_已经爆炸 = false;
		_Timer = 5f;
	}

	public void F_开始生成()
	{
		MyLog.LogError("开始生成爆竹");
		_开始生成 = true;
		_等待上一个爆炸_已经爆炸 = true;
		_Curr生成间隔 = Random.Range(_生成间隔.x, _生成间隔.y);
	}

	private void Update()
	{
		if (!_开始生成 || !_等待上一个爆炸_已经爆炸)
		{
			return;
		}
		_Timer += Time.deltaTime;
		if (_Timer > _Curr生成间隔)
		{
			MyLog.LogError("生成一个爆竹>>等待爆炸");
			_等待上一个爆炸_已经爆炸 = false;
			_Timer = 0f;
			C_爆竹 c_爆竹 = Object.Instantiate(_爆竹Prefab, F_获取随机点(), Quaternion.identity);
			c_爆竹.F_空降();
			c_爆竹._Event_引爆 += delegate
			{
				MyLog.LogError("回调>>爆竹爆炸");
				_等待上一个爆炸_已经爆炸 = true;
			};
		}
	}

	private Vector2 F_获取随机点()
	{
		return new Vector2(Random.Range(_Pos_左下.x, _Pos_右上.x), Random.Range(_Pos_左下.y, _Pos_右上.y));
	}

	public void F_停止生成()
	{
		_开始生成 = false;
	}
}
