using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
	public float rotationSpeed;

	void Start()
	{
		//Losujemy prędkość obrotu z zakresu 50% do 150% pierwotnej
		rotationSpeed = Random.Range(0.5f * rotationSpeed, 1.5f * rotationSpeed);
	}

	void FixedUpdate()
	{
		if (!GameManager.instance.inGame) return;
		//Obracamy w osi Z (do ekranu)
		transform.Rotate(new Vector3(0f, 0f, rotationSpeed));
	}
}