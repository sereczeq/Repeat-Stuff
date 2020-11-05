using UnityEngine;

public class Destroyer : MonoBehaviour
{

	//Brutalne - gdy cokolwiek znajdzie się wewnątrz collidera zostanie zniszczone
	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(other.gameObject);
	}
}
