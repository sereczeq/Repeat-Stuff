using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{

	//Pola do których przypisujemy duże kafelki z podłogą
	//Zakładamy, że na początku do floorTile1 jest przypisany lewy kafelek
	public GameObject floorTile1, floorTile2;

    // UWAGA - korzystamy z metody FixedUpdate, która wywołuje się raz na tick silnika fizyki nie co klatkę.
    // Pozwoli nam to uniezależnić ruchy obiektów od ilości wyświetlanych w danym momencie klatek na sekundę
    void FixedUpdate()
	{
		if (!GameManager.instance.inGame) return;
		//Co tick silnika fizyki przesywamy oba kafelki o worldScrolingSpeed
		// -= ponieważ chcemy przesuwać świat w lewo, czyli zmniejszać współrzędną X
		floorTile1.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed, 0f, 0f);
		floorTile2.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed, 0f, 0f);

		//Jeśli środek prawego kafelka przejechał przez środek ekranu (x < 0) lewy kafelek przenosimy
		//na jego prawą stronę i zamieniamy zmienne

		if (floorTile2.transform.position.x < 0f)
		{
			//przesuwam lewy kafelek w prawo o 20 jednostek (czyli 2 jego szerokości)
			floorTile1.transform.position += new Vector3(30.42f, 0f, 0f);

			//Zamieniam zmienne
			var tmp = floorTile1;
			floorTile1 = floorTile2;
			floorTile2 = tmp;
		}
	}
}

