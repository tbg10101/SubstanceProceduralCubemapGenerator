using UnityEngine;
using UnityEditor;
using Substance.Game;

/*
	This is an examle of how to use the procedural cubemap generator and the cubemap it generates.
*/
public class ExmpleCubemapGeneratorClient : MonoBehaviour {
	//this is the procedural material being used in this example
	public SubstanceGraph inputMaterial;

	//this is the material used for the skybox in this example
	public Material skyboxMaterial;

	Cubemap cubemap;

	public void generateNewCubemap () {
		//set the procedural material's properties if needed, before rendering
		inputMaterial.SetInputFloat("$randomseed", Random.Range(0.0f, 10000.0f));

		//have the procedural material render a new texture
		inputMaterial.QueueForRender();
		Substance.Game.Substance.RenderSubstancesAsync();

		/* render the cubemap
			This takes the input material and renders it to a cubemap of the specified size. 

			Remember, this is not increasing the resolution at which the procedural material is rendering the texture, 
			that must be set in the procedural material itself. You will likely want the cubemap to be the same size
			or larger than the procedural material's size to retain all the quality.
		*/
		cubemap = CubemapGeneratorHelper.instance.generateCubemap(inputMaterial, CubemapGenerator.Sizes._512);

		//set the skybox material to use the cubemap which was just rendered
		skyboxMaterial.SetTexture("_Tex", cubemap);
	}


	/*
	this is the bit I added to ensure that the cubemap and skybox material currently being used is exported.
	has an if statement makes the directory "Assets/SavedSkyboxes, saves the skybox material and it's cubemap,
	and then clears the example skybox texture, as a way to show it's been saved to a different skybox texture. 
	It also shows the saved skybox in console, since honestly it shouldn't be used on the fly in gameplay lol. -Jay
	 */
	public void saveCubeMap()
	{
		if(AssetDatabase.IsValidFolder("Assets/SavedSkyboxes"))
		{
			AssetDatabase.CreateAsset(cubemap, "Assets/SavedSkyboxes/Cubemap_"+System.DateTime.Now.ToString("yyyyMMdd_hhmmss")+".asset");
			Material SavedSkybox = new Material(source:skyboxMaterial);
			SavedSkybox.SetTexture("_Tex", cubemap);
			skyboxMaterial.SetTexture("_Tex", null);
			AssetDatabase.CreateAsset(SavedSkybox, "Assets/SavedSkyboxes/Skybox_"+System.DateTime.Now.ToString("yyyyMMdd_hhmmss")+".mat");
			Debug.Log("Saved skybox to: "+AssetDatabase.GetAssetPath(SavedSkybox));
		}
		else
		{
			AssetDatabase.CreateFolder("Assets", "SavedSkyboxes");
			saveCubeMap();
		}
	}
}
