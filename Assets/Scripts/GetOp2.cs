// realiza conta, com base nos valores e operacoes passa ou nao para a proxima cena

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

// faz o op2 funcionar
public class GetOp2 : MonoBehaviour
{
	public ToggleGroup checks;
	public Toggle mais, menos;
	public Button ok;
	public static int op1 = -1;
	public static int op2 = -1;
	public int tipo;
	public string valor1;
	public string valor2;
	public static int opcao = 0;
	public static int valorOp;
	public static string operacao;
	public Transform WarningText1, WarningText2, WarningText3;
	public Text txt1, txt2;


	// Inicialização
	void Start()
	{
		op1 = -1;
		op2 = -1;
		opcao = 0;
	}

	// Obtém o valor do primeiro operando
	public void GetOp1(string v1)
	{
		if (v1 == "")
			valor1 = "-1";
		else
			valor1 = v1;
		Debug.Log(v1);
		op1 = int.Parse(valor1);
	}

	 // Obtém o valor do segundo operando
	public void GetOpe2(string v2)
	{
		if (v2 == "")
			valor2 = "-1";
		else
			valor2 = v2;
		Debug.Log(v2);
		op2 = int.Parse(valor2);
	}

	// Obtém a opção selecionada (adicao ou subtracao)
	public void GetOption(int ope)
	{
		opcao = ope; 
		
	}

	 // Lógica executada ao clicar no botão "Ok"
	public void botaoOk()
	{
		// Tenta converter os valores dos campos de texto em inteiros

		bool converte = int.TryParse(txt1.text, out op1);
		if (!converte)
		{
			op1 = -1;
		}

		converte = int.TryParse(txt2.text, out op2);
		if (!converte)
		{
			op2 = -1;
		}

		// Realiza a operação com base na opção selecionada
		if (opcao == 1)
		{
			valorOp = op1 + op2;
			operacao = op1 + "+" + op2;
		}
		else if (opcao == 2)
		{
			
			// Verifica se o segundo operando é maior que o primeiro
			if (op2 > op1)
			{
				WarningText2.gameObject.SetActive(true);
				WarningText1.gameObject.SetActive(true);
				//interrompe aqui a funcao
				return;
			}
			else
				valorOp = op1 - op2;
			//Debug.Log(valorOp);
			operacao = op1 + "-" + op2;
		}
		 // se tiver algo errado surge mesg de erro caso contrario ela fica escondida
		if (opcao == 0)
			WarningText3.gameObject.SetActive(true);
		else
			WarningText3.gameObject.SetActive(false);
		if (op1 < 0)
			WarningText1.gameObject.SetActive(true);
		else
			WarningText1.gameObject.SetActive(false);
		if (op2 < 0)
			WarningText2.gameObject.SetActive(true);
		else
			WarningText2.gameObject.SetActive(false);

		// se der tudo certo, vai para a proxima cena
		if (op1 >= 0 && op2 >= 0 && opcao != 0)
			changeScenes.proxCena("MatDourado");
	}

}