using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class PictureThisController : MonoBehaviour
{
	public GameObject drawingPanel;
	public GameObject blurPanel;
	public GameObject newWordListMenu;
	public GameObject editWordListMenu;
	public GameObject wordListPicker;
	public GameObject buttonNewWordList;
	public GameObject buttonEditWordList;
	public GameObject WordLists;
	public GameObject colorPicker;
	public GameObject slider;
	public GameObject sizeLabel;
	public GameObject CurrentColor;
	public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
	public string wordList;
	public int index;
	public int size = 1;
	public GameObject paintImage;
	PointerEventData pointerED;
    EventSystem eventSystem;
	GraphicRaycaster raycaster;
	string player1, player2;
	Color currentColor = Color.black;
	bool painting;
	Ray ray;
	RaycastHit hit;
    
	// Start is called before the first frame update
    void Start()
    {
		painting = false;
		paintImage.SetActive(false);
		raycaster = GetComponent<GraphicRaycaster>();
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
       	index = PlayerPrefs.GetInt("PT_playingIndex", 0);
		player1 = PlayerPrefs.GetString("PT_player1", "Player 1");
		player2 = PlayerPrefs.GetString("PT_player2", "Player 2");
		colorPicker.GetComponent<ColorPicker>().startPos = new Vector2(180, 300);
		changeSize();
    }

    // Update is called once per frame
    void Update()
    {
		/*IF NO SE ESTA DIBUJANDO THEN REQUEST FOCUS AL GUESS WORD INPUT*/
    }
	
	public void updateWordLists(){
		Dropdown d = wordListPicker.GetComponent<Dropdown>();
		d.ClearOptions();
		List<string> options = new List<string>();
		string s = "";
		for(int i = 1, index = PlayerPrefs.GetInt("PT_index", 0); i < index; i++){
			if((s = PlayerPrefs.GetString("PT_"+i,"")) != "")
				options.Add(s.Split(':')[0].Substring(1));
		}
		d.AddOptions(options);
	}

	public void pickColor(Color color){
		currentColor = color;
		CurrentColor.GetComponent<Image>().color = currentColor;
		Debug.Log("Selected new color:  "+currentColor.ToString());
	}
	
	public void pickedColor(string color){
		switch(color){
			case "Blue":
				currentColor = Color.blue;
				break;
			case "Red":
				currentColor = Color.red;
				break;
			case "Green":
				currentColor = Color.green;
				break;
			case "Yellow":
				currentColor = Color.yellow;
				break;
			case "Orange":
				currentColor.r = 255/255.0F;
				currentColor.g = 145/255.0F;
				currentColor.b = 0;
				currentColor.a = 255/255.0F;
				break;
			case "Purple":
				currentColor.r = 195/255.0F;
				currentColor.g = 0;
				currentColor.b = 255/255.0F;
				currentColor.a = 255/255.0F;
				break;
			case "Brown":
				currentColor.r = 135/255.0F;
				currentColor.g = 62/255.0F;
				currentColor.b = 0;
				currentColor.a = 255/255.0F;
				break;
			case "White":
				currentColor = Color.white;
				break;
			case "Gray":
				currentColor = Color.gray;
				break;
			default:
				currentColor = Color.black;
				break;
		}
		CurrentColor.GetComponent<Image>().color = currentColor;
		Debug.Log("Selected new color:  "+currentColor.ToString());
	}
	
	public void changeSize(){
		size = (int)slider.GetComponent<Slider>().value;
		sizeLabel.GetComponent<Text>().text = "Size: " + size;
	}
	
	public void exitGame(){
		SceneManager.LoadScene("PictureThisMenu");	
	}	
	
	public void exit(){
		PlayerPrefs.Save();
		Application.Quit();
	}
	
	public void start(){
		Debug.Log("Starting game with word list: "+wordList);
		SceneManager.LoadScene("PictureThis");	
	}
	
	public void newWordList(){
		Debug.Log("New word list");
		newWordListMenu.GetComponent<NewWordList>().show();
	}
	
	public void editWordList(){
		Debug.Log("Edit word list");
		editWordListMenu.GetComponent<EditWordList>().show();
	}
	
	public void paint(){
		if(!painting) return;
       
		pointerED = new PointerEventData(eventSystem);
        //Set the Pointer Event Position to that of the mouse position
        pointerED.position = Input.mousePosition;

		List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerED, results);
		//For every result returned, output the name of the GameObject on the Canvas hit by the Ray
		int i = 0;
		bool hitPanel = false;
		foreach (RaycastResult result in results)
		{
			if(result.gameObject.name == "DrawingPanel"){
				hitPanel = true;
				break;
			} else
				i++;
		}

		if(hitPanel){
			paintImage.SetActive(true);
			GameObject pI = Object.Instantiate(paintImage, paintImage.transform.parent);
			pI.GetComponent<Image>().color = currentColor;
			pI.GetComponent<RectTransform>().sizeDelta = new Vector2 (size, size);
			Vector3 p = new Vector3(Input.mousePosition.x,  Input.mousePosition.y, 0);
			p.z = results[i].distance;//distance of the plane from the camera
			pI.transform.position = Camera.main.ScreenToWorldPoint(p);
			paintImage.SetActive(false);
		}
	}

	public void hover()
    {
		painting = true;
		Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
		texture.alphaIsTransparency = true;
		Debug.Log("H: " + texture.height + "  W: " +texture.width);
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                texture.SetPixel(x, y, currentColor);
            }
        }
        texture.Apply();
        Cursor.SetCursor(texture, hotSpot, cursorMode);
    }
/*
		pointerImage.SetActive(true);
		pointerImage.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
	}*/
	
	public void stopHover(){
		painting = false;
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
		//pointerImage.SetActive(false);
	}
}
