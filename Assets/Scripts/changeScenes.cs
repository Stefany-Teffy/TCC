// Importa as bibliotecas necessárias
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScenes : MonoBehaviour {

	// Variável estática para armazenar o nome da cena anterior
	public static string nomeAnt;


	// Método para iniciar a cena seguinte
	public void proximaCena(string scenename)
	{
		// Chama o método cenaAnt para armazenar o nome da cena atual antes de mudar
		cenaAnt();
		// Carrega a cena especificada em "scenename"
		SceneManager.LoadScene(scenename, LoadSceneMode.Single);

	}

	// Método estático para iniciar a cena seguinte (utilizando a classe sem instanciar)
	// qual a diferença?
	public static void proxCena (string scenename)
	{
		cenaAnt ();
		SceneManager.LoadScene (scenename, LoadSceneMode.Single);
	}

	 // Método estático para armazenar o nome da cena atual
	public static void cenaAnt()
	{
		// Obtém e armazena o nome da cena ativa no momento
		nomeAnt = SceneManager.GetActiveScene ().name;
		// Exibe o nome da cena atual no console (para fins de depuração)
		Debug.Log (SceneManager.GetActiveScene ().name);
	}

	// Método para encerrar a aplicação
	public void acabar()
	{
		Application.Quit ();
	}


	void Update()
	{
		// Verifica se a tecla "escape" foi pressionada
		if (Input.GetKey("escape"))
		{
			// Encerra a aplicação
			Application.Quit();
		}
	}

	public void PlayAgain() {
        if (changeScenes.nomeAnt == "selecao") {
            SceneManager.LoadScene("selecao");
        } else if (changeScenes.nomeAnt == "op2") {
            SceneManager.LoadScene("op2");
		} else if (changeScenes.nomeAnt == "Opcoes") {
            SceneManager.LoadScene("Opcoes");
        } else if (changeScenes.nomeAnt == "Niveis"){
            SceneManager.LoadScene("Niveis");
        }
    }
}