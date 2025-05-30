using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Missao
{
    public string nome; 
    public string descricao; 
    public bool concluida; 
    public bool ativa; 
    public string chavePlayerPrefs; 
    public int estrelasNecessarias; 
    public AudioClip audio;
}

[System.Serializable]
public class BlocoMissoes
{
    public string nomeBloco; 
    public List<Missao> missoes = new List<Missao>(); 
    public GameObject botaoBloco; 
    public TextMeshProUGUI textoBotao; 
    public TextMeshProUGUI percentualBotao; 
    public TextMeshProUGUI descricaoText;
    public GameObject fundoEscuro; 
    public GameObject fundoRosa; 
    public GameObject fundoVerde;
    public GameObject popUp;
    public GameObject emblema;
    public GameObject coletar;
    public GameObject botReturn;
    public int missaoAtiva = 0;
    public GameObject fundoTextCinza;
    public GameObject fundoTextRosa;
    public GameObject fundoTextLaranja;
    public GameObject fundoTextAmarelo;
    public GameObject fundoTextVerde;
}

public class GerenciadorMissoes : MonoBehaviour
{
    public List<BlocoMissoes> blocosMissoes = new List<BlocoMissoes>();
    public AudioSource audioSource;

    void Start()
    {
        InicializarBlocosMissoes();
        VerificarMissoes();
        CarregarEstadoMissaoAtiva(); // Carrega o estado da missão ativa
        AtualizarUI();
    }

    private void InicializarBlocosMissoes()
    {
        string nomeJogador = GerenciaJogador.instancia.nomeJogador;
        Debug.Log($"Inicializando blocos de missões para o jogador: {nomeJogador}");

        if (blocosMissoes.Count > 0)
        {
            BlocoMissoes bloco1 = blocosMissoes[0];

            bloco1.missoes = new List<Missao>
            {
                new Missao
                {
                    nome = "Missão 1",
                    descricao = "Jogue o nível 1 (unidades) e conquiste no mínimo 1 estrela.",
                    chavePlayerPrefs = "Bloco1_Missao1Concluida_" + nomeJogador,
                    estrelasNecessarias = 1,
                    audio = Resources.Load<AudioClip>("Sounds/sommissao1bloco1") 
                },
                new Missao
                {
                    nome = "Missão 2",
                    descricao = "Jogue o nível 2 (dezenas) e conquiste no mínimo 2 estrelas.",
                    chavePlayerPrefs = "Bloco1_Missao2Concluida_" + nomeJogador,
                    estrelasNecessarias = 2,
                    audio = Resources.Load<AudioClip>("Sounds/sommissao2bloco1")
                },
                new Missao
                {
                    nome = "Missão 3",
                    descricao = "Jogue o nível 3 (centenas) e conquiste o máximo de estrelas.",
                    chavePlayerPrefs = "Bloco1_Missao3Concluida_" + nomeJogador,
                    estrelasNecessarias = 3,
                    audio = Resources.Load<AudioClip>("Sounds/sommissao3bloco1")
                }
            };

            Debug.Log($"Bloco 1 inicializado com {bloco1.missoes.Count} missões.");

            BlocoMissoes bloco2 = blocosMissoes[1];

            bloco2.missoes = new List<Missao>
            {
                new Missao
                {
                    nome = "Missão 1",
                    descricao = "Jogue o nível 4 (soma de unidades) e conquiste no mínimo 1 estrela.",
                    chavePlayerPrefs = "Bloco2_Missao1Concluida_" + nomeJogador,
                    estrelasNecessarias = 1,
                    audio = Resources.Load<AudioClip>("Sounds/sommissao1bloco2")
                },
                new Missao
                {
                    nome = "Missão 2",
                    descricao = "Jogue o nível 5 (soma de dezenas) e conquiste no mínimo 2 estrelas.",
                    chavePlayerPrefs = "Bloco2_Missao2Concluida_" + nomeJogador,
                    estrelasNecessarias = 2,
                    audio = Resources.Load<AudioClip>("Sounds/sommissao2bloco2")
                },
                new Missao
                {
                    nome = "Missão 3",
                    descricao = "Jogue o nível 6 (soma de centenas) e conquiste o máximo de estrelas.",
                    chavePlayerPrefs = "Bloco2_Missao3Concluida_" + nomeJogador,
                    estrelasNecessarias = 3,
                    audio = Resources.Load<AudioClip>("Sounds/sommissao3bloco2")
                }
            };

            Debug.Log($"Bloco 2 inicializado com {bloco2.missoes.Count} missões.");

            BlocoMissoes bloco3 = blocosMissoes[2];

            bloco3.missoes = new List<Missao>
            {
                new Missao
                {
                    nome = "Missão 1",
                    descricao = "Jogue o nível 7 (subtração de unidades) e conquiste no mínimo 1 estrela.",
                    chavePlayerPrefs = "Bloco3_Missao1Concluida_" + nomeJogador,
                    estrelasNecessarias = 1,
                    audio = Resources.Load<AudioClip>("Sounds/sommissao1bloco3")
                },
                new Missao
                {
                    nome = "Missão 2",
                    descricao = "Jogue o nível 8 (subtração de dezenas) e conquiste no mínimo 2 estrelas.",
                    chavePlayerPrefs = "Bloco3_Missao2Concluida_" + nomeJogador,
                    estrelasNecessarias = 2,
                    audio = Resources.Load<AudioClip>("Sounds/sommissao2bloco3")
                },
                new Missao
                {
                    nome = "Missão 3",
                    descricao = "Jogue o nível 9 (subtração de centenas) e conquiste o máximo de estrelas.",
                    chavePlayerPrefs = "Bloco3_Missao3Concluida_" + nomeJogador,
                    estrelasNecessarias = 3,
                    audio = Resources.Load<AudioClip>("Sounds/sommissao3bloco3")
                }
            };

            Debug.Log($"Bloco 3 inicializado com {bloco3.missoes.Count} missões.");
        }
        else
        {
            Debug.LogError("Nenhum bloco de missões configurado no Inspector.");
        }
    }

    private void VerificarMissoes()
    {
        Debug.Log("Verificando missões...");
        foreach (var bloco in blocosMissoes)
        {
            foreach (var missao in bloco.missoes)
            {
                if (PlayerPrefs.HasKey(missao.chavePlayerPrefs))
                {
                    missao.concluida = PlayerPrefs.GetInt(missao.chavePlayerPrefs, 0) == 1;
                    Debug.Log($"Missão {missao.nome} concluída: {missao.concluida}");
                }
                else
                {
                    Debug.Log($"Missão {missao.nome} não encontrada no PlayerPrefs.");
                    missao.concluida = false;
                }
            }
        }
    }

    private void CarregarEstadoMissaoAtiva()
    {
        foreach (var bloco in blocosMissoes)
        {
            string chaveMissaoAtiva = $"MissaoAtiva_{bloco.nomeBloco}";
            if (PlayerPrefs.HasKey(chaveMissaoAtiva))
            {
                bloco.missaoAtiva = PlayerPrefs.GetInt(chaveMissaoAtiva, 0);
                Debug.Log($"Missão ativa carregada para o bloco {bloco.nomeBloco}: {bloco.missaoAtiva}");
            }
            else
            {
                Debug.Log($"Nenhuma missão ativa salva para o bloco {bloco.nomeBloco}. Usando valor padrão.");
                bloco.missaoAtiva = 0;
            }
            string chaveBlocoConcluido = $"Bloco{blocosMissoes.IndexOf(bloco) + 1}_Concluido";
            if (PlayerPrefs.HasKey(chaveBlocoConcluido) && PlayerPrefs.GetInt(chaveBlocoConcluido) == 1)
            {
                bloco.missaoAtiva = -1; // Marca o bloco como concluído
                Debug.Log($"Bloco {bloco.nomeBloco} já foi concluído.");
            }
        }
    }

    private void SalvarEstadoMissaoAtiva(BlocoMissoes bloco)
    {
        string chaveMissaoAtiva = $"MissaoAtiva_{bloco.nomeBloco}";
        PlayerPrefs.SetInt(chaveMissaoAtiva, bloco.missaoAtiva);
        PlayerPrefs.Save();
        Debug.Log($"Missão ativa salva para o bloco {bloco.nomeBloco}: {bloco.missaoAtiva}");
    }

    private void AtualizarUI()
    {
        Debug.Log("Atualizando UI...");
        foreach (var bloco in blocosMissoes)
        {
            AtualizarBotaoBloco(bloco);
        }
    }

    private void AtualizarBotaoBloco(BlocoMissoes bloco)
    {
        if (bloco.botaoBloco != null)
        {
            Debug.Log($"Atualizando botão do bloco: {bloco.nomeBloco}");

            bloco.fundoEscuro.SetActive(false);
            bloco.fundoRosa.SetActive(false);
            bloco.fundoVerde.SetActive(false);
            bloco.fundoTextCinza.SetActive(false);
            bloco.fundoTextRosa.SetActive(false);
            bloco.fundoTextLaranja.SetActive(false);
            bloco.fundoTextAmarelo.SetActive(false);
            bloco.fundoTextVerde.SetActive(false);
            
                switch(bloco.missaoAtiva){
                    case 0: 
                    bloco.fundoTextCinza.SetActive(true);
                    Debug.Log("Fundo da missão 1 ativado!");
                    break;

                    case 1: 
                    bloco.fundoTextRosa.SetActive(true);
                    Debug.Log("Fundo da missão 2 ativado!");
                    break;

                    case 2: 
                    bloco.fundoTextLaranja.SetActive(true);
                    Debug.Log("Fundo da missão 3 ativado!");
                    break;

                    case 3:
                    bloco.fundoTextAmarelo.SetActive(true);
                    Debug.Log("Fundo da missão 4 ativado!");
                    break;

                    case 4: 
                    bloco.fundoTextVerde.SetActive(true);
                    Debug.Log("Fundo da missão 5 ativado!");
                    break;

                    default: 
                    bloco.fundoTextCinza.SetActive(true);
                    Debug.Log("Todas as missões completas!");
                    break;
                }

            if (bloco.missaoAtiva == -1)
            {
                Debug.Log("Todas as missões do bloco concluídas.");
                bloco.botaoBloco.SetActive(false);
                bloco.emblema.SetActive(false);
                bloco.descricaoText.text = "Todas as missões desse bloco completas!";
            }
            else if (bloco.missaoAtiva >= 0 && bloco.missaoAtiva < bloco.missoes.Count)
            {
                Missao missaoAtual = bloco.missoes[bloco.missaoAtiva];
                Debug.Log($"Missão atual: {missaoAtual.nome}, Concluída: {missaoAtual.concluida}");

                if (bloco.descricaoText != null)
                {
                    bloco.descricaoText.text = missaoAtual.descricao;
                    Debug.Log($"Descrição da missão atual: {missaoAtual.descricao}");
                    int indiceBloco = blocosMissoes.IndexOf(bloco);
                    bloco.descricaoText.GetComponent<Button>().onClick.AddListener(() => {
                    TocarAudioMissaoAtiva(indiceBloco);
                    });
                }

                if (!missaoAtual.concluida)
                {
                    Debug.Log("Missão não concluída. Ativando fundo escuro.");
                    bloco.fundoEscuro.SetActive(true);
                    bloco.textoBotao.text = "Completar";
                    bloco.percentualBotao.text = "0%";
                    //bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().interactable = true;
                    bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                    bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                    Debug.Log("Botão Completar clicado. Redirecionando para a cena Naturais.");
                    changeScenes.proxCena("Naturais"); 
                    });
                }
                else
                {
                    // Missão concluída
                    if (bloco.missaoAtiva < bloco.missoes.Count - 1)
                    {
                        // Existem mais missões após esta
                        Debug.Log("Missão concluída. Ativando fundo rosa.");
                        bloco.fundoRosa.SetActive(true);
                        bloco.textoBotao.text = "Próxima";
                        bloco.percentualBotao.text = "100%";
                        //bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().interactable = true;
                        bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().interactable = true;
                        bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                        bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                        Debug.Log("Botão Próxima clicado. Chamando BotaoBlocoClicado.");
                        BotaoBlocoClicado(blocosMissoes.IndexOf(bloco));
                        });
                    }
                    else
                    {
                        // Última missão concluída
                        Debug.Log("Todas as missões do bloco concluídas. Ativando fundo verde.");
                        bloco.fundoVerde.SetActive(true);
                        bloco.textoBotao.text = "Concluído";
                        bloco.percentualBotao.text = "100%";
                        //.botaoBloco.GetComponent<UnityEngine.UI.Button>().interactable = true;
                        bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().interactable = true;
                        bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                        bloco.botaoBloco.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                        Debug.Log("Botão Concluído clicado. Chamando BotaoBlocoClicado.");
                        BotaoBlocoClicado(blocosMissoes.IndexOf(bloco));
                        });
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Botão do bloco não atribuído.");
        }
    }

    private void DefinirMissaoAtiva(int indiceBloco)
    {
        var bloco = blocosMissoes[indiceBloco];

        if (bloco.missoes.Count > 0)
        {
            bool todasConcluidas = true;

            for (int i = 0; i < bloco.missoes.Count; i++)
            {
                if (!bloco.missoes[i].concluida)
                {
                    todasConcluidas = false;
                    bloco.missaoAtiva = i; 
                    Debug.Log($"Missão {i + 1} definida como ativa no bloco {bloco.nomeBloco}.");
                    break;
                }
            }

            if (todasConcluidas)
            {
                bloco.missaoAtiva = -1;
                Debug.Log($"Todas as missões do bloco {bloco.nomeBloco} concluídas.");
            }
        }
        else
        {
            Debug.LogError("Nenhuma missão configurada no bloco.");
        }
    }

    public void BotaoBlocoClicado(int indiceBloco)
    {
        if (indiceBloco >= 0 && indiceBloco < blocosMissoes.Count)
        {
            var bloco = blocosMissoes[indiceBloco];

            if (bloco.missaoAtiva >= 0 && bloco.missaoAtiva < bloco.missoes.Count)
            {
                Missao missaoAtual = bloco.missoes[bloco.missaoAtiva];

                if (missaoAtual.concluida)
                {
                    if (bloco.missaoAtiva < bloco.missoes.Count - 1)
                    {
                        // Avança para a próxima missão apenas se o botão "Próxima" for clicado
                        bloco.missaoAtiva++;
                        Debug.Log($"Avançando para a missão {bloco.missaoAtiva + 1}.");
                        SalvarEstadoMissaoAtiva(bloco);
                        AtualizarUI();
                    }
                    else
                    {
                        Debug.Log("Todas as missões do bloco concluídas!");
                        bloco.missaoAtiva = -1;
                        bloco.botReturn.SetActive(false);
                        bloco.coletar.SetActive(true);
                        AbrirPopUpDoBloco(indiceBloco);
                        AtualizarUI();

                        PlayerPrefs.SetInt($"EmblemaBloco{indiceBloco + 1}Concluido", 1);
                        PlayerPrefs.SetInt($"Bloco{indiceBloco + 1}_Concluido", 1);
                        PlayerPrefs.Save();

                        bloco.botaoBloco.SetActive(false);
                    }
                }
            }
        }
    }

    public void AbrirPopUpDoBloco(int indiceBloco)
    {
        var bloco = blocosMissoes[indiceBloco];
        
        if (bloco.popUp != null)
        {
            bloco.popUp.SetActive(true); 
            Debug.Log($"Pop-up do bloco {bloco.nomeBloco} aberto!");
        }
        else
        {
            Debug.LogError("Pop-up não atribuído no bloco " + bloco.nomeBloco);
        }
    }

    public void FecharPopUpDoBloco(int indiceBloco)
    {
        var bloco = blocosMissoes[indiceBloco];
        
        if (bloco.popUp != null)
        {
            bloco.popUp.SetActive(false); 
            Debug.Log($"Pop-up do bloco {bloco.nomeBloco} fechado!");
        }
        else
        {
            Debug.LogError("Pop-up não atribuído no bloco " + bloco.nomeBloco);
        }
    }

    public void TocarAudioMissaoAtiva(int indiceBloco)
    {
        if (indiceBloco >= 0 && indiceBloco < blocosMissoes.Count)
        {
            var bloco = blocosMissoes[indiceBloco];
            
            // Verifica se há uma missão ativa válida
            if (bloco.missaoAtiva >= 0 && bloco.missaoAtiva < bloco.missoes.Count)
            {
                Missao missaoAtual = bloco.missoes[bloco.missaoAtiva];
                
                if (missaoAtual.audio != null && audioSource != null)
                {
                    audioSource.Stop(); 
                    audioSource.clip = missaoAtual.audio;
                    audioSource.Play();
                    Debug.Log($"Tocando áudio da missão: {missaoAtual.nome}");
                }
                else
                {
                    Debug.LogWarning("Áudio da missão ou AudioSource não configurado!");
                }
            }
        }
    }

    
}