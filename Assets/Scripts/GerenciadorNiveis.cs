using UnityEngine;

public class GerenciadorNiveis : MonoBehaviour
{
    public GameObject niveisPart1;
    public GameObject niveisPart2;
    public GameObject botEsquerda;
    public GameObject botDireita;
    public GameObject ativo1;
    public GameObject ativo2;
    private GerenciadorEstrelas gerenciadorEstrelas;

    void Start()
    {
        gerenciadorEstrelas = FindObjectOfType<GerenciadorEstrelas>();
    }

    public void AlternarNiveis()
    {
        bool part1Ativo = niveisPart1.activeSelf;

        niveisPart1.SetActive(!part1Ativo);
        niveisPart2.SetActive(part1Ativo);

        if (gerenciadorEstrelas != null)
        {
            gerenciadorEstrelas.RevalidarEstrelas();
        }
        else
        {
            Debug.LogError("GerenciadorEstrelas não encontrado!");
        }

        // Atualizar os botões e indicadores ativos
        if (part1Ativo)
        {
            botEsquerda.SetActive(true);
            botDireita.SetActive(false);
            ativo1.SetActive(false);
            ativo2.SetActive(true);
        }
        else
        {
            botEsquerda.SetActive(false);
            botDireita.SetActive(true);
            ativo1.SetActive(true);
            ativo2.SetActive(false);
        }
    }
}
