using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Conquistas : MonoBehaviour
{
    public GameObject medalha1; 
    public GameObject medalha2; 
    public GameObject medalha3; 
    public GameObject medalha4; 
    public GameObject medalha5; 
    public GameObject medalha6; 
    public GameObject medalha7; 
    public GameObject medalha8; 
    public GameObject medalha9; 
    public GameObject medalha10; 

    public GameObject emblema1;
    public GameObject emblema2;
    public GameObject emblema3;

    private GameObject medalhaAtual; // Medalha atualmente ativa (se houver)
    public TextMeshProUGUI pontuacaoText;
    private int pontuacaoTotal = 7200;

    // Lista para armazenar as medalhas e seus valores correspondentes
    private List<(GameObject medalha, int pontosNecessarios)> medalhas;

    void Start()
    {
        // Inicializa a lista de medalhas e suas pontuações
        medalhas = new List<(GameObject, int)>()
        {
            (medalha1, 100),
            (medalha2, 1200),
            (medalha3, 2100),
            (medalha4, 3000),
            (medalha5, 3600),
            (medalha6, 3900),
            (medalha7, 4800),
            (medalha8, 5700),
            (medalha9, 6600),
            (medalha10, 7200)
        };

        VerificarMedalhas(); 
        VerificarEmblemas();
    }

    public void VerificarMedalhas()
    {
        int pontuacaoUsuario = GerenciaJogador.instancia.pontuacaoJogador;
        AtualizarPontuacaoText(pontuacaoUsuario);

        // Verifica qual a maior medalha desbloqueável pela pontuação atual
        foreach (var (medalha, pontosNecessarios) in medalhas)
        {
            if (pontuacaoUsuario >= pontosNecessarios)
            {
                AtualizarMedalhaAtiva(medalha);
            }
        }
    }

    private void AtualizarMedalhaAtiva(GameObject novaMedalha)
    {
        // Evita reativar uma medalha já ativa
        if (medalhaAtual == novaMedalha) return;

        // Desativa a medalha atual (se houver)
        if (medalhaAtual != null)
        {
            medalhaAtual.SetActive(false);
        }

        // Ativa a nova medalha
        medalhaAtual = novaMedalha;
        medalhaAtual.SetActive(true);
    }

    public void AtualizarPontuacaoText(int pontuacaoAtual)
    {
        if (pontuacaoText != null)
        {
            pontuacaoText.text = $"{pontuacaoAtual} / {pontuacaoTotal}"; // Exibe no formato "pontuaçãoAtual / 7200"
        }
    }

    private void VerificarEmblemas()
    {
        if (PlayerPrefs.GetInt("EmblemaBloco1Concluido", 0) == 1)
        {
            emblema1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("EmblemaBloco2Concluido", 0) == 1)
        {
            emblema2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("EmblemaBloco3Concluido", 0) == 1)
        {
            emblema3.SetActive(true);
        }
    }
}
