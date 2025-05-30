using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciaJogador : MonoBehaviour
{
    public static GerenciaJogador instancia;
    public string nomeJogador;
    public string senhaJogador;
    public int pontuacaoJogador;

    private const string NOME_KEY = "nome_";
    private const string PONTUACAO_KEY = "pontuacao_";
    private const string NOMES_JOGADORES_KEY = "NomesJogadores";

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); 
    }

    public bool NomeExiste(string nome)
    {
        string nomesExistentes = PlayerPrefs.GetString(NOMES_JOGADORES_KEY, "");
        return nomesExistentes.Contains(nome);
    }

    public bool VerificarSenha(string nome, string senha)
    {
        // Verifica se a senha armazenada corresponde à senha fornecida
        string senhaArmazenada = PlayerPrefs.GetString("senha_" + nome, "");
        return senhaArmazenada == senha;
    }
    
    public void CarregarProgresso(string nome)
    {
        nomeJogador = nome;
        pontuacaoJogador = PlayerPrefs.GetInt(PONTUACAO_KEY + nome, 0);
        Debug.Log("Progresso carregado para " + nomeJogador + ". Pontuação: " + pontuacaoJogador);
    }

    public void RegistrarJogador(string nome, string senha)
    {
        string nomesExistentes = PlayerPrefs.GetString(NOMES_JOGADORES_KEY, "");

        if (!nomesExistentes.Contains(nome))
        {
            // Adiciona o nome à lista de jogadores no PlayerPrefs
            nomesExistentes = string.IsNullOrEmpty(nomesExistentes) ? nome : $"{nomesExistentes},{nome}";
            PlayerPrefs.SetString(NOMES_JOGADORES_KEY, nomesExistentes);
        }

        // Registra o jogador e sua pontuação
        if (PlayerPrefs.HasKey(NOME_KEY + nome))
        {
            nomeJogador = nome;
            pontuacaoJogador = PlayerPrefs.GetInt(PONTUACAO_KEY + nome);
            Debug.Log("Bem-vindo de volta, " + nomeJogador + ". Pontuação carregada: " + pontuacaoJogador);
        }
        else
        {
            nomeJogador = nome;
            pontuacaoJogador = 0;  
            PlayerPrefs.SetString(NOME_KEY + nome, nome); 
            PlayerPrefs.SetInt(PONTUACAO_KEY + nome, pontuacaoJogador); 
            PlayerPrefs.SetString("senha_" + nome, senha);
            Debug.Log("Novo jogador registrado: " + nomeJogador + ". Pontuação inicial: " + pontuacaoJogador);
        }

        PlayerPrefs.Save();
    }

    public void AtualizarPontuacao(int pontos)
    {
        pontuacaoJogador += pontos;
        PlayerPrefs.SetInt(PONTUACAO_KEY + nomeJogador, pontuacaoJogador);
        PlayerPrefs.Save();
        Debug.Log("Pontuação atualizada para: " + pontuacaoJogador);
    }

    public int ObterPontuacao(string nome)
    {
        return PlayerPrefs.GetInt(PONTUACAO_KEY + nome, 0);
    }
}
