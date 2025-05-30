using UnityEditor; // Ao importar isso eu interajo com o editor
using UnityEngine; // e com isso nos componentes

public class LimparPlayerPrefsEditor
{
    [MenuItem("Ferramentas/Limpar PlayerPrefs")] // MenuItem especifica onde o método deve aparecer
    public static void LimparPlayerPrefs() // quando selecionar isso no menu, eles são apagados
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs limpos com sucesso.");
    }
}
