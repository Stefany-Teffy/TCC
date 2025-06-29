using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ranking : MonoBehaviour
{
    public GameObject placarIndividualPrefab;
    public Transform PanelEspecifico;

    private readonly List<int> medalThresholds = new List<int>()
    {
        100, 1200, 2100, 3000, 3600, 3900, 4800, 5700, 6600, 7200
    };

    void Start()
    {
        AtualizarRanking();
    }
    void OnEnable()
    {
        AtualizarRanking();
    }

    public void AtualizarRanking()
    {
        string nomeJogadorLogado = "";
        if (GerenciaJogador.instancia != null)
        {
            nomeJogadorLogado = GerenciaJogador.instancia.nomeJogador;
        }

        List<Jogador> jogadores = new List<Jogador>();
        string[] nomes = PlayerPrefs.GetString("NomesJogadores", "").Split(',');
        if (nomes.Length == 0 || string.IsNullOrEmpty(nomes[0])) { return; }
        foreach (var nome in nomes)
        {
            if (string.IsNullOrEmpty(nome)) continue;
            int pontuacao = PlayerPrefs.GetInt("pontuacao_" + nome, -1);
            if (pontuacao == -1) continue;
            jogadores.Add(new Jogador(nome, pontuacao));
        }
        jogadores.Sort((a, b) => b.pontuacao.CompareTo(a.pontuacao));
        foreach (Transform child in PanelEspecifico) { Destroy(child.gameObject); }

        int numeroDeJogadoresAMostrar = Mathf.Min(jogadores.Count, 3);
        for (int i = 0; i < numeroDeJogadoresAMostrar; i++)
        {
            Jogador jogador = jogadores[i];
            GameObject clone = Instantiate(placarIndividualPrefab, PanelEspecifico);

            TextMeshProUGUI nomeText = clone.transform.Find("Nome")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI pontosText = clone.transform.Find("Pontos")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI posicaoText = clone.transform.Find("posicao")?.GetComponent<TextMeshProUGUI>();

            if (nomeText != null) nomeText.text = jogador.nome;
            if (pontosText != null) pontosText.text = $"{jogador.pontuacao} XP";
            if (posicaoText != null) posicaoText.text = $"{i + 1}º";

            Transform medalhasContainer = clone.transform.Find("medalhas");
            if (medalhasContainer != null)
            {
                foreach (Transform medal in medalhasContainer) { medal.gameObject.SetActive(false); }
                int indiceMedalha = DeterminarIndiceDaMedalha(jogador.pontuacao);
                if (indiceMedalha != -1)
                {
                    string nomeMedalha = $"medalha{indiceMedalha + 1}";
                    Transform medalhaParaAtivar = medalhasContainer.Find(nomeMedalha);
                    if (medalhaParaAtivar != null) { medalhaParaAtivar.gameObject.SetActive(true); }
                }
            }
            
            if (!string.IsNullOrEmpty(nomeJogadorLogado) && jogador.nome == nomeJogadorLogado)
            {
                Transform userAtivoImage = clone.transform.Find("userAtivo");
                if (userAtivoImage != null)
                {
                    userAtivoImage.gameObject.SetActive(true);
                }
            }
        }
    }

    private int DeterminarIndiceDaMedalha(int pontuacao)
    {
        int indiceDaMedalha = -1;
        for (int i = 0; i < medalThresholds.Count; i++)
        {
            if (pontuacao >= medalThresholds[i]) { indiceDaMedalha = i; }
            else { break; }
        }
        return indiceDaMedalha;
    }
}

[System.Serializable]
public class Jogador
{
    public string nome;
    public int pontuacao;

    public Jogador(string nome, int pontuacao)
    {
        this.nome = nome;
        this.pontuacao = pontuacao;
    }
}