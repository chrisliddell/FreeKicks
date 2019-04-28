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
	public GameObject paintbrush;
	public GameObject circleBrush;
	public GameObject rectBrush;
	public GameObject highlightCircle;
	public GameObject highlightRect;
	public Text wordTyped;
	public GameObject colorPicker;
	public GameObject slider;
	public GameObject sizeLabel;
	public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
	public string wordList, letter;
	public int index;
	public int size = 1;
	PointerEventData pointerED;
	const string textPlaceholder = "Type the word that's being drawn";
    EventSystem eventSystem;
	GraphicRaycaster raycaster;
	string player1, player2;
	Color currentColor = Color.black;
	bool inCanvas;
	Ray ray;
	RaycastHit hit;
    
	// Start is called before the first frame update
    void Start()
    {
		inCanvas = false;
		raycaster = GetComponent<GraphicRaycaster>();
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
       	index = PlayerPrefs.GetInt("PT_playingIndex", 0);
		player1 = PlayerPrefs.GetString("PT_player1", "Player 1");
		player2 = PlayerPrefs.GetString("PT_player2", "Player 2");
		changeSize();
		updateHighlights();
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Backspace) && wordTyped.text != textPlaceholder){
			if(wordTyped.text.Length > 1)
				wordTyped.text = wordTyped.text.Remove(wordTyped.text.Length - 1); 
			else
				wordTyped.text = textPlaceholder;
		}
		else if((Input.GetKeyDown(KeyCode.Return))){
			if(wordTyped.text != "" && wordTyped.text != textPlaceholder)
				checkWord();
		}
		else if((letter = checkLetter()) != ""){
			if(wordTyped.text == textPlaceholder)
				wordTyped.text = letter; 
			else
				wordTyped.text += letter;
		}
	}
	
	public void checkWord(){
		
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
		updateHighlights();
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
		updateHighlights();
		Debug.Log("Selected new color:  "+currentColor.ToString());
	}
	
	public void updateHighlights(){
		if(highlightCircle.active){
			highlightRect.SetActive(true);
			highlightCircle.GetComponent<Image>().color = currentColor;
			highlightRect.GetComponent<Image>().color = currentColor;
			highlightRect.SetActive(false);
		} else {
			highlightCircle.SetActive(false);
			highlightCircle.GetComponent<Image>().color = currentColor;
			highlightRect.GetComponent<Image>().color = currentColor;
			highlightCircle.SetActive(false);
		}
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
		if(!inCanvas) return;
       
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
			GameObject pI = Object.Instantiate(paintbrush, drawingPanel.transform);
			pI.GetComponent<Image>().color = currentColor;
			pI.GetComponent<RectTransform>().sizeDelta = new Vector2 (size, size);
			Vector3 p = new Vector3(Input.mousePosition.x,  Input.mousePosition.y, 0);
			p.z = results[i].distance;//distance of the plane from the camera
			pI.transform.position = Camera.main.ScreenToWorldPoint(p);
		}
	}
	
	public void hover()
    {
		inCanvas = true;
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
		inCanvas = false;
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
		//pointerImage.SetActive(false);
	}
	
	public void letterPressed(){/*
		Debug.Log("*********************"+wordInput.GetComponent<InputField>().text + "  cccccc? "+wordInput.GetComponent<InputField>().text.Length +"ANCHOR: " +wordInput.GetComponent<InputField>().selectionAnchorPosition );
		wordInput.GetComponent<InputField>().selectionAnchorPosition = wordInput.GetComponent<InputField>().text.Length-1;
		wordInput.GetComponent<InputField>().caretPosition = wordInput.GetComponent<InputField>().text.Length-1;*/
		//wordTyped.GetComponent<InputField>().DeactivateInputField();
	}
	
	public void selectedBrush(int type){
		if(type == 0){
			paintbrush = circleBrush;
			highlightCircle.SetActive(true);
			highlightRect.SetActive(false);
		}
		else{
			paintbrush = rectBrush;
			highlightCircle.SetActive(false);
			highlightRect.SetActive(true);
		}
	}
	
	public string checkLetter(){
		string l = "";
		if((Input.GetKeyDown(KeyCode.A)))
			l = "a";
		else if((Input.GetKeyDown(KeyCode.B)))
			l = "b";
		else if((Input.GetKeyDown(KeyCode.C)))
			l = "c";
		else if((Input.GetKeyDown(KeyCode.D)))
			l = "d";
		else if((Input.GetKeyDown(KeyCode.E)))
			l = "e";
		else if((Input.GetKeyDown(KeyCode.F)))
			l = "f";
		else if((Input.GetKeyDown(KeyCode.G)))
			l = "g";
		else if((Input.GetKeyDown(KeyCode.H)))
			l = "h";
		else if((Input.GetKeyDown(KeyCode.I)))
			l = "i";
		else if((Input.GetKeyDown(KeyCode.J)))
			l = "j";
		else if((Input.GetKeyDown(KeyCode.K)))
			l = "k";
		else if((Input.GetKeyDown(KeyCode.L)))
			l = "l";
		else if((Input.GetKeyDown(KeyCode.M)))
			l = "m";
		else if((Input.GetKeyDown(KeyCode.N)))
			l = "n";
		else if((Input.GetKeyDown(KeyCode.O)))
			l = "o";
		else if((Input.GetKeyDown(KeyCode.P)))
			l = "p";
		else if((Input.GetKeyDown(KeyCode.Q)))
			l = "q";
		else if((Input.GetKeyDown(KeyCode.R)))
			l = "r";
		else if((Input.GetKeyDown(KeyCode.S)))
			l = "s";
		else if((Input.GetKeyDown(KeyCode.T)))
			l = "t";
		else if((Input.GetKeyDown(KeyCode.U)))
			l = "u";
		else if((Input.GetKeyDown(KeyCode.V)))
			l = "v";
		else if((Input.GetKeyDown(KeyCode.W)))
			l = "w";
		else if((Input.GetKeyDown(KeyCode.X)))
			l = "x";
		else if((Input.GetKeyDown(KeyCode.Y)))
			l = "y";
		else if((Input.GetKeyDown(KeyCode.Z)))
			l = "z";
		return l;
	}
}
