using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplazarEnemigoVertical : MonoBehaviour
{
	
	public float minY;
	public float maxY;
	public float TiempoEspera = 5f;
	public float Velocidad = 1f;

	private GameObject _LugarObjetivo;


	// Se llama al inicio antes de la primera actualización de frames
	void Start()
    {
		UpdateObjetivo();
		StartCoroutine("Patrullar");
	}

	// La actualización se llama una vez por cuadro
	void Update()
    {
        
    }


	private void UpdateObjetivo()
	{

		// Si es la primera vez iniciar el patrullaje para arriba
		if (_LugarObjetivo == null) {
			_LugarObjetivo = new GameObject("Sitio_objetivo");
			_LugarObjetivo.transform.position = new Vector2(transform.position.x, maxY);
			//transform.localScale = new Vector3(1, 1, 1);
			return;
		}

		// iniciar el patrullaje para abajo
		if (_LugarObjetivo.transform.position.y == maxY) {
			_LugarObjetivo.transform.position = new Vector2( transform.position.x, minY);
			//transform.localScale = new Vector3(1, 1, 1);
		}

		// Cambio de sentido 
		else if (_LugarObjetivo.transform.position.y == minY) {
			_LugarObjetivo.transform.position = new Vector2( transform.position.x, maxY);
			//transform.localScale = new Vector3(1, 1, 1);
		}
	}

	private IEnumerator Patrullar()
	{
		
		// Co-rutina para mover el enemigo
		while (Vector2.Distance(_LugarObjetivo.transform.position ,transform.position   ) > 0.02f) {
			// Se desplazará hasta el sitio objetivo
			Vector2 direction = _LugarObjetivo.transform.position - transform.position;
			float yDirection = direction.y;
			
			transform.Translate(direction.normalized * Velocidad * Time.deltaTime);

			yield return null;
		}

		// En este punto, se alcanzó el objetivo, se establece nuestra posición en la del objetivo.
		//Debug.Log("Se alcanzo el Objetivo");
		transform.position = new Vector2(_LugarObjetivo.transform.position.x , transform.position.y);

		// Esperamos un momento antes de volver a movernos
		//Debug.Log("Esperando " + TiempoEspera + " segundos");
		yield return new WaitForSeconds(TiempoEspera);

		//Debug.Log("Se espera lo necesario para que termine y vuelva a empezar movimiento");
		UpdateObjetivo();
		StartCoroutine("Patrullar");
	}
}
