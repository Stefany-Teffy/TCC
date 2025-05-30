using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Usuario : MonoBehaviour
{
    public GameObject panelIdentificacao;
    public GameObject botaoSair; 
    public string nome;
    public string senha;
    public Transform WarningText1; 
    public Transform WarningText2; 
    public Text txt1;
    public Text txt2;

    void Start()
    {
        // Verifica se o nome foi salvo nos PlayerPrefs
        nome = PlayerPrefs.GetString("UsuarioNome", "");
        senha = PlayerPrefs.GetString("UsuarioSenha", "");

        // Se não houver nome salvo, ativa o painel de login
        if (string.IsNullOrEmpty(nome))
        {
            AtivarPanel(panelIdentificacao);
        }
        else
        {
            // Se o nome estiver salvo, ativa o botão de sair
            if (botaoSair != null)
            {
                botaoSair.SetActive(true);
            }
        }

        // Verifica se o campo de texto está atribuído
        if (txt1 == null)
        {
            txt1 = GetComponent<Text>();  
        }
        if (txt2 == null)
        {
            txt2 = GetComponent<Text>();  
        }
    }

    private void AtivarPanel(GameObject panelIdentificacao)
    {
         if (panelIdentificacao != null)
        {
            panelIdentificacao.SetActive(true);
        }
        else
        {
            Debug.LogError("Panel de identificação não foi atribuído!");
        }
    }

    public void VerificaUsuario()
    {
        string entradaNome = txt1.text.Trim(); 
        string entradaSenha = txt2.text.Trim(); 

        bool nomeValido = NomeValido(entradaNome);
        bool senhaValida = !string.IsNullOrEmpty(entradaSenha);

        WarningText1.gameObject.SetActive(!nomeValido);
        WarningText2.gameObject.SetActive(!senhaValida);

        if (!nomeValido || !senhaValida)
        {
            Debug.Log("Nome ou senha inválidos!");
            return;
        }

        // Verifica se o nome de usuário já existe
        if (GerenciaJogador.instancia.NomeExiste(entradaNome))
        {
            // Verifica se a senha está correta
            if (GerenciaJogador.instancia.VerificarSenha(entradaNome, entradaSenha))
            {
                // Nome e senha corretos: carrega o progresso do usuário
                GerenciaJogador.instancia.CarregarProgresso(entradaNome);
                nome = entradaNome;
                senha = entradaSenha;
                PlayerPrefs.SetString("UsuarioNome", nome);
                PlayerPrefs.SetString("UsuarioSenha", senha);

                WarningText1.gameObject.SetActive(false);
                WarningText2.gameObject.SetActive(false);
                Debug.Log("Usuário logado com sucesso: " + nome);

                if (botaoSair != null)
                {
                    botaoSair.SetActive(true);
                }

                if (panelIdentificacao != null)
                {
                    panelIdentificacao.SetActive(false);
                }
            }
            else
            {
                // Senha incorreta: pede para trocar o nome
                WarningText1.gameObject.SetActive(true);
                WarningText2.gameObject.SetActive(true);
                Debug.Log("Senha incorreta! Escolha outro nome de usuário.");
            }
        }
        else
        {
            // Se o nome não existe, registra um novo usuário
            nome = entradaNome;
            senha = entradaSenha;
            PlayerPrefs.SetString("UsuarioNome", nome);
            PlayerPrefs.SetString("UsuarioSenha", senha);
            GerenciaJogador.instancia.RegistrarJogador(nome, senha);

            WarningText1.gameObject.SetActive(false);
            WarningText2.gameObject.SetActive(false);
            Debug.Log("Usuário registrado com sucesso: " + nome);

            if (botaoSair != null)
            {
                botaoSair.SetActive(true);
            }

            if (panelIdentificacao != null)
            {
                panelIdentificacao.SetActive(false);
            }
        }
    }

    private bool NomeValido(string entrada)
    {
        bool temLetra = false;
        foreach (char c in entrada)
        {
            if (char.IsLetter(c))
            {
                temLetra = true;
            }
        }
        return temLetra;
    }

    public void Sair()
    {
        nome = ""; // Limpa o nome do usuário
        senha = ""; // Limpa a senha do usuário

        // Limpa o nome dos PlayerPrefs
        PlayerPrefs.DeleteKey("UsuarioNome");
        PlayerPrefs.DeleteKey("UsuarioSenha");

        if (txt1 != null)
        {
            txt1.text = ""; // Limpa o campo de texto
        }
         if (txt2 != null)
        {
            txt2.text = ""; // Limpa o campo de texto
        }

        if (botaoSair != null)
        {
            botaoSair.SetActive(false); // Desativa o botão de sair
        }

        AtivarPanel(panelIdentificacao); // Ativa novamente o painel de identificação para o login

        Debug.Log("Usuário deslogado.");
    }
}
