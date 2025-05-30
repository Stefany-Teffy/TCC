using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ranking : MonoBehaviour
{
    public GameObject placarIndividualPrefab; 
    public Transform panelInfo; // Painel onde os clones serão criados

    void Start()
    {
        AtualizarRanking();
    }

    public void AtualizarRanking()
    {
        List<Jogador> jogadores = new List<Jogador>();

        string[] nomes = PlayerPrefs.GetString("NomesJogadores", "").Split(',');

        if (nomes.Length == 0 || string.IsNullOrEmpty(nomes[0]))
        {
            Debug.LogWarning("Nenhum jogador registrado para o ranking.");
            return;
        }

        // Adiciona jogadores à lista
        foreach (var nome in nomes)
        {
            if (string.IsNullOrEmpty(nome)) continue;

            int pontuacao = PlayerPrefs.GetInt("pontuacao_" + nome, -1);

            if (pontuacao == -1)
            {
                Debug.LogWarning($"Pontuação não encontrada para o jogador: {nome}");
                continue;
            }

            jogadores.Add(new Jogador(nome, pontuacao));
        }

        // Ordena os jogadores por pontuação (maior para menor)
        jogadores.Sort((a, b) => b.pontuacao.CompareTo(a.pontuacao));

        // Limpa o painel antes de criar os clones
        foreach (Transform child in panelInfo)
        {
            Destroy(child.gameObject);
        }

        // Cria os clones no painel
        foreach (var jogador in jogadores)
        {
            GameObject clone = Instantiate(placarIndividualPrefab, panelInfo);
            TextMeshProUGUI nomeText = clone.transform.Find("Nome")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI pontosText = clone.transform.Find("Pontos")?.GetComponent<TextMeshProUGUI>();

            if (nomeText != null) nomeText.text = jogador.nome;
            if (pontosText != null) pontosText.text = $"{jogador.pontuacao} XP";

            Debug.Log($"Adicionado ao ranking: {jogador.nome} | Pontuação: {jogador.pontuacao}");
        }
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
