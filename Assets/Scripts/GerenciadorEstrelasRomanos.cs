using UnityEngine;

public class GerenciadorEstrelasRomanos : MonoBehaviour
{
    void Start()
    {
        string nomeJogador = GerenciaJogador.instancia.nomeJogador;
        for (int nivel = 1; nivel <= 12; nivel++)
        {
            string chaveEstrelas = "starRomDesNiveis_" + nomeJogador + "_" + nivel;

            if (!PlayerPrefs.HasKey(chaveEstrelas))
            {
                PlayerPrefs.SetInt(chaveEstrelas, 3); // Inicia com 3 estrelas disponíveis
            }
            Debug.Log("Verificando nível " + nivel + " para o jogador " + nomeJogador);
        }
    }

    public void DefinirPorNivel(int nivel, int estDesativadas)
    {
        string nomeJogador = GerenciaJogador.instancia.nomeJogador; // Obtém o nome do jogador logado
        string chaveEstrelas = "starRomDesNiveis_" + nomeJogador + "_" + nivel;

        PlayerPrefs.SetInt(chaveEstrelas, estDesativadas);
        PlayerPrefs.Save(); // Salva as alterações
    }

    public int ObterDoNivel(int nivel)
    {
        string nomeJogador = GerenciaJogador.instancia.nomeJogador; // Obtém o nome do jogador logado
        string chaveEstrelas = "starRomDesNiveis_" + nomeJogador + "_" + nivel;
        return PlayerPrefs.GetInt(chaveEstrelas);
    }

    public int ObterEstrelasDesativadasDoNivel(int nivel)
    {
        return ObterDoNivel(nivel);
    }

    public void RevalidarEstrelas()
    {
        EstrelaControllerRomanos[] estrelaControllers = FindObjectsOfType<EstrelaControllerRomanos>();

        foreach (EstrelaControllerRomanos estrelaController in estrelaControllers)
        {
            estrelaController.AtualizarEstadoEstrela();
        }
    }
}