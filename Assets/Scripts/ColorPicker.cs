using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorPicker : MonoBehaviour
{

    //texture or image of colors
    public Texture2D colorsPalette;
    //number of rows
    public int numRows;
    //number of cols
    public int numCols;
    //texture size
    public int textureSize;

    //color array, just to load all colors instead of sampling multiple times
    private Color32[] allColors;
    //total color number
    private int totalNumColors;
    //selected color
    private Color32 selectedColor;
    //Player color
    public Color32 playerColor;

    //show selected color
    private Image selectedColorImage;
    //keep count for color array
    private int count;
    //color offsets for meshes
    private Vector2[] colorOffsets;

    private bool end = false;

    private Tail player;
    private SpriteRenderer snakeColor;
    private LineRenderer tailColor;


    void OnEnable()
    {

        if (allColors == null)
            CreateColorsArray();

        Transform child = this.transform.GetChild(0);
        selectedColorImage = child.GetComponent<Image>();
        selectedColorImage.color = allColors[0];

    }

    private Color32 SamplePaletteTexture(float xOffset, float yOffset)
    {
        Color32 color = colorsPalette.GetPixel((int)(xOffset * textureSize) - 5, (int)(yOffset * textureSize) - 5);
        return color;
    }

    private void CreateColorsArray()
    {
        totalNumColors = numRows * numCols; 
        allColors = new Color32[totalNumColors];    
        colorOffsets = new Vector2[totalNumColors]; 
        int totalCount = 0; 
        for (int x = 0; x < numRows; x++)
        {
            for (int y = 0; y < numCols; y++)
            {
                allColors[totalCount] = SamplePaletteTexture(((x + 1f) / numRows), ((y + 1f) / numCols)); 
                colorOffsets[totalCount] = new Vector2(((float)(x) / numRows), ((float)(y) / numCols)); 
                totalCount++;   
            }
        }

        playerColor = allColors[count];
    }


    public void ChangeColor(int num)
    {

        if (count == 0)
            end = false;
        else if(count == totalNumColors - 1)
        {
            end = true;
        }

        if (end)
            count -= num;
        else count += num;   
        

        if (count < 0)  
            count = 0;
        if (count > totalNumColors - 1) 
            count = totalNumColors - 1;

        selectedColorImage.color = allColors[count];
        playerColor = allColors[count];    
    }

    ////Gets called when you press the aplply image button in the UI
    //public void ApplyColorToSprite(UnityEngine.UI.Image image)
    //{
    //    image.color = allColors[count];
    //}

    ////Gets called when you press apply GO button in the UI
    ////Pass in a gameobject, get its meshrenderer component, then its FIRST material in the materials list, and then we apply the correct offset of the preview color!
    //public void ApplyColorToGameObject(GameObject gO)
    //{
    //    gO.GetComponent<MeshRenderer>().material.mainTextureOffset = colorOffsets[count];
    //}

}
