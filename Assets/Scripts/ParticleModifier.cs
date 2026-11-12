using UnityEngine;

public class ParticleModifier : MonoBehaviour
{
	private ParticleSystem _particleSystem;

	[Header("设置Simulation Speed的范围")]
	public float minSimulationSpeed = 0.5f;

	public float maxSimulationSpeed = 2f;

	[Header("设置透明度的范围")]
	public float minAlpha = 0.1f;

	public float maxAlpha = 1f;

	[Header("控制随机变化的间隔时间")]
	public float changeInterval = 1f;

	private float timeSinceLastChange;

	[Header("当前值和目标值")]
	[SerializeField]
	private float currentSpeed;

	[SerializeField]
	private float targetSpeed;

	[SerializeField]
	private float currentAlpha;

	[SerializeField]
	private float targetAlpha;

	[Header("速度过渡的平滑系数")]
	public float smoothSpeed = 0.1f;

	public float smoothAlpha = 0.1f;

	[Header("大风沙")]
	public bool isBigDFS;

	[Header("大风沙目标值")]
	public float bigSpeed = 1f;

	public float bigAlpha = 0.8f;

	[Header("大风沙当前值")]
	public float currentBigSpeed;

	public float currentBigAlpha;

	private void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>();
		currentSpeed = _particleSystem.main.simulationSpeed;
		currentAlpha = Random.Range(minAlpha, maxAlpha);
	}

	private void Update()
	{
		if (isBigDFS)
		{
			currentBigSpeed = Mathf.Lerp(currentBigSpeed, bigSpeed, 1f * Time.deltaTime);
			currentBigAlpha = Mathf.Lerp(currentBigAlpha, bigAlpha, 1f * Time.deltaTime);
			currentSpeed = currentBigSpeed;
			currentAlpha = currentBigAlpha;
			timeSinceLastChange = changeInterval;
			SetSimulationSpeed(currentBigSpeed);
			SetColorOverLifetimeAlpha(currentBigAlpha);
			return;
		}
		timeSinceLastChange += Time.deltaTime;
		if (timeSinceLastChange >= changeInterval)
		{
			timeSinceLastChange = 0f;
			targetSpeed = Random.Range(minSimulationSpeed, maxSimulationSpeed);
			targetAlpha = Random.Range(minAlpha, maxAlpha);
		}
		currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, smoothSpeed * Time.deltaTime);
		currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, smoothAlpha * Time.deltaTime);
		currentBigSpeed = currentSpeed;
		currentBigAlpha = currentAlpha;
		SetSimulationSpeed(currentSpeed);
		SetColorOverLifetimeAlpha(currentAlpha);
	}

	private void SetSimulationSpeed(float speed)
	{
		ParticleSystem.MainModule main = _particleSystem.main;
		main.simulationSpeed = speed;
	}

	private void SetColorOverLifetimeAlpha(float alpha)
	{
		ParticleSystem.ColorOverLifetimeModule colorOverLifetime = _particleSystem.colorOverLifetime;
		if (colorOverLifetime.enabled)
		{
			Gradient gradient = new Gradient();
			GradientAlphaKey[] alphaKeys = colorOverLifetime.color.gradient.alphaKeys;
			GradientColorKey[] colorKeys = colorOverLifetime.color.gradient.colorKeys;
			for (int i = 0; i < alphaKeys.Length; i++)
			{
				alphaKeys[i].alpha = alpha;
			}
			gradient.SetKeys(colorKeys, alphaKeys);
			colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
		}
	}
}
