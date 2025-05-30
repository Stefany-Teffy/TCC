using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medalhas : MonoBehaviour
{
    public GameObject panelMedalha; // Painel que será ativado
    public GameObject[] medalhas; // Array de medalhas
    private bool[] medalhasDesbloqueadas; // Estado de medalhas desbloqueadas

    private int[] pontosParaMedalha = { 100, 1200, 2100, 3000, 3600, 3900, 4800, 5700, 6600, 7200 }; // Pontuação necessária para cada medalha

    void Start()
    {
        // Inicializa o estado das medalhas desbloqueadas
        medalhasDesbloqueadas = new bool[medalhas.Length];
        for (int i = 0; i < medalhas.Length; i++)
        {
            medalhasDesbloqueadas[i] = PlayerPrefs.GetInt($"Medalha_{i}", 0) == 1;
        }
    }

    public void VerificarMedalhasComAtraso()
    {
        StartCoroutine(ExibirMedalhasAposAtraso(0.5f)); // Espera 0.5 segundos antes de ativar as medalhas
    }

    private IEnumerator ExibirMedalhasAposAtraso(float delay)
    {
        yield return new WaitForSeconds(delay);

        int pontuacaoUsuario = GerenciaJogador.instancia.pontuacaoJogador;

        // Verifica medalhas a partir da pontuação do jogador
        for (int i = 0; i < pontosParaMedalha.Length; i++)
        {
            if (pontuacaoUsuario >= pontosParaMedalha[i] && !medalhasDesbloqueadas[i])
            {
                AtivarMedalha(panelMedalha, medalhas[i]);
                medalhasDesbloqueadas[i] = true;
                PlayerPrefs.SetInt($"Medalha_{i}", 1); // Salva o estado de desbloqueio
                PlayerPrefs.Save();
                break; // Ativa apenas a próxima medalha válida
            }
        }
    }

    private void AtivarMedalha(GameObject painel, GameObject medalha)
    {
        painel.SetActive(true); // Ativa o painel de medalhas
        medalha.SetActive(true); // Ativa a medalha correspondente
    }

    public void ColetarMedalha(GameObject medalha)
    {
        medalha.SetActive(false); // Desativa a medalha específica
        panelMedalha.SetActive(false); // Desativa o painel de medalhas
    }
}
