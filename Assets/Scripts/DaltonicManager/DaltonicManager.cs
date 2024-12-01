using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaltonicManager : MonoBehaviour
{
	/*NORMAL - PROTAN - DEUTAN - DALTONICO (Ambos)

	
			TEST LIBRO: (POT SCRIPT)
			1.DERECHA: Daltonico
			2.Arriba: NO ERROR
			3.ABajo: Protan / Daltonico 
			4.ABajo: Protan

			(4-1-2-2)

			DOS POCIONES:
			1. Ambos

			TEST BANNERS:
			1 (74):
				-74: Normal
				-21: Daltonico
			2 (8):
				-8: Normal
				-3: Daltonico (leve)
			3 (45):
				-45: Normal
				-Otro: Daltonico
			4 (96):
				-96: Normal
				-6: Protan
				-9: Deutan

			32: Daltonico*/
	public float normal;
	public float protan;
	public float deutan;
	public float daltonic;
	[SerializeField] GameObject canvas;

    private void Start()
    {
        canvas.SetActive(true);
		Time.timeScale = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void SumNormal()
	{
		normal++;
	}
	public void SumProtan()
	{
		protan++;
	}
	public void SumDeutan()
	{
		deutan++;
	}
	public void SumDaltonic()
	{
		daltonic++;
	}
    public void ShowResults()
    {
        // Máximos
        float maxDaltonic = 6f;
        float maxProtan = 3f;
        float maxDeutan = 1f;

        // Cálculo de porcentajes
        float daltonicPercentage = (daltonic / maxDaltonic) * 100f;
        float protanPercentage = (protan / maxProtan) * 100f;
        float deutanPercentage = (deutan / maxDeutan) * 100f;

        // Daltonic (puede ser Protan y Deutan)
        daltonicPercentage = Mathf.Min(daltonicPercentage, 100f);

        // Calculamos el porcentaje normal como el complementario
        // Guardar en GameData
        GameData.ProtanPoints = protanPercentage;
        GameData.DeutanPoints = deutanPercentage;
        GameData.DaltonicPoints = daltonicPercentage;

        Debug.Log("Protan %: " + protanPercentage);
        Debug.Log("Deutan %: " + deutanPercentage);
        Debug.Log("Daltonic %: " + daltonicPercentage);
    }


}
