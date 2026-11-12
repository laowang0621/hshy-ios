using UnityEngine;
using UnityEngine.UI;

public class UI_帧率 : MonoBehaviour
{
	public Text _Text_帧率;

	private float _更新间隔 = 0.33f;

	private float _累积时间;

	private int _间隔内运行帧数;

	private float _Timer;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		_Timer = _更新间隔;
	}

	private void Update()
	{
		_Timer -= Time.deltaTime;
		_累积时间 += Time.timeScale / Time.deltaTime;
		_间隔内运行帧数++;
		if (_Timer <= 0f)
		{
			float num = _累积时间 / (float)_间隔内运行帧数;
			_Text_帧率.text = $"FPS:{num:F2}";
			_Timer = _更新间隔;
			_累积时间 = 0f;
			_间隔内运行帧数 = 0;
		}
	}
}
