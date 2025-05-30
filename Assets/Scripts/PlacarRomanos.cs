using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlacarRomanos : MonoBehaviour
{
    public GameObject placa;
    public static GameObject placar;
    public GameObject estrela3;
    public GameObject estrela1;
    public GameObject estrela2;
    public GameObject textoPerdeu;
    public GameObject textoVenceu;
    public GameObject botaoProxNivel;
    public static GameObject perdeu;
    public static GameObject venceu;
    public static GameObject proxNivel;
    public GerenciadorEstrelasRomanos gerenciadorEstrelas;

    void Start()
    {
       placar = placa;
       perdeu = textoPerdeu;
       venceu = textoVenceu;
       proxNivel = botaoProxNivel;
       gerenciadorEstrelas = GameObject.Find("GerenciadorEstrelasRomanos").GetComponent<GerenciadorEstrelasRomanos>();
       Debug.Log ("placar: " + placar.transform.name);

    }

    public static void Desativar(){
        placar.SetActive (false);
        Debug.Log ("Placar desativado");
    }

    public static void Ativar( GerenciadorEstrelasRomanos gerenciadorEstrelas, int valortotal, int numeroGerado, int tentativas, int fases, int nAtual){
        if (nAtual == 12){
            proxNivel.SetActive(false);
        }
        placar.SetActive (true);
        Debug.Log ("Placar ativado");
        int quantidadeEstrelasDesativadas = 0;

        if (tentativas == 0 && fases == 1)
        {
            if(valortotal != numeroGerado){
                venceu.SetActive(false);
                perdeu.SetActive(true);
                proxNivel.SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaEsquerda").SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaDireita").SetActive(false);
                quantidadeEstrelasDesativadas = 3;
            }
            else{
                GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaDireita").SetActive(false);
                quantidadeEstrelasDesativadas = 2;
            }
        }
        else if(tentativas == 0 && fases == 2)
        {
            if(valortotal != numeroGerado){
                GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaDireita").SetActive(false);
                quantidadeEstrelasDesativadas = 2;
            }
            else{
                GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
                quantidadeEstrelasDesativadas = 1;
            }
        }
        else if (tentativas == 0 && fases == 3 && valortotal != numeroGerado)
        {
                GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
                quantidadeEstrelasDesativadas = 1;
        }
         gerenciadorEstrelas.DefinirPorNivel(nAtual, quantidadeEstrelasDesativadas);


        int estrelasAtivas = 3 - quantidadeEstrelasDesativadas;
        int pontosNivel = estrelasAtivas * 100;

        TextMeshProUGUI placarText = GameObject.Find("PlacarText")?.GetComponent<TextMeshProUGUI>();
        placarText.text = "+" + pontosNivel + " XP";
        GerenciaJogador.instancia.AtualizarPontuacao(pontosNivel);

        Debug.Log($"Nível {nAtual} completo! Estrelas ativas: {estrelasAtivas}, Pontos obtidos: {pontosNivel}");

       Medalhas medalhasScript = GameObject.FindObjectOfType<Medalhas>();
        if (medalhasScript != null)
        {
            medalhasScript.VerificarMedalhasComAtraso(); // Verifica as medalhas com atraso
        }

    }
}
