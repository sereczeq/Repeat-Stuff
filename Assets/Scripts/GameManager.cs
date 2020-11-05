
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Tworzymy statyczną zmienną przechowującą jedyny obiekt klasy GameManager (wg. wzorca Singletonu)
	//Pozwoli to na odwołanie się do GameManagera w dowolnym miejscu projektu poprzez GameManager.instance
	public static GameManager instance;

	//Pole zawierające prędkość świata do którego będzie mógł odwołać się dowolny obiekt w grze
	//Ustawienie wartości następuje w edytorze
	public float worldScrollingSpeed;

	public Text scoreText;
	public float score;

	//Pole na prefab przeszkody.
	public GameObject obstacle;
	//Co ile sekund ma pojawiać się kolejna przeszkoda
	public float obstacleSpawnRate;
	//Max wysokość spawnu przeszkody
	public float maxObstacleSpawnHeight;
	//Min wysokość spawnu przeszkody
	public float minObstacleSpawnHeight;
	//Miejsce na osi X gdzie będziemy spawnować przeszkody
	public float obstacleSpawnPositionX;

	//pole w którym będziemy pamiętać czy aktualnie trwa gra
	public bool inGame;
	//do zapisania worldScrollingSpeed w momencie zatrzymania gry
	private float savedSpeed;
	//pole do podpięcia przycisku reset
	public GameObject resetButton;

	// Use this for initialization
	void Start()
	{
		//Podczas uruchomienia przypisujemy aktualną instancję do statycznego pola instance
		//!!! Należy uważać, żeby zawsze na scenie był dokładnie jeden GameManager !!!
		instance = this;

		//Uruchamiamy spawnowanie przeszkód
		InitializeGame();
	}

    void Update()
    {

        if (!inGame && worldScrollingSpeed != 0)
        {
			savedSpeed = worldScrollingSpeed;
			worldScrollingSpeed = 0f;
		}
		else if (inGame && worldScrollingSpeed == 0)
        {
			worldScrollingSpeed = savedSpeed;
        }
    }

    void FixedUpdate()
	{
		if (!GameManager.instance.inGame) return;
		//Co tick silnika fizyki dopisujemy do wyniku przebytą odległość i wywołujemy metodę wyświetlającą wynik na ekranie
		UpdateOnScreenScore();
	}

	private void UpdateOnScreenScore()
    {
		score += worldScrollingSpeed;
		scoreText.text = score.ToString("0");
    }

	void SpawnObstacle()
	{
		if (!GameManager.instance.inGame) return;
		//Losujemy wysokość przeszkody i przygotowujemy cały wektor jej początkowej pozycji
		var spawnPosition = new Vector3
			(obstacleSpawnPositionX, Random.Range(minObstacleSpawnHeight, maxObstacleSpawnHeight), 0f);

		//Instancjonujemy przeszkodę na przygotowanej pozycji i z zerowym obrotem
		Instantiate(obstacle, spawnPosition, Quaternion.identity);
	}
	void InitializeGame()
	{
		//Ustawiamy pole mówiące, że jesteśmy  w trakcie gry
		inGame = true;

		//Uruchamiamy spawnowanie przeszkód
		InvokeRepeating("SpawnObstacle", obstacleSpawnRate, obstacleSpawnRate);
	}

	public void GameOver()
	{
		//gra się skończyła, więc:
		inGame = false;
		//Wyświetlamy przycisk Restart
		resetButton.SetActive(true);
		//Przerywamy spawnowanie przeszkód
		CancelInvoke("SpawnObstacle");
	}

	//na górze dodaj using UnityEngine.SceneManagement
	public void RestartGame()
	{
		//Ponownie ładujemy scenę o indeksie 0 (czyli jednyną w naszej grze)
		//Spowoduje to reset gry
		SceneManager.LoadScene(0);
	}


}
