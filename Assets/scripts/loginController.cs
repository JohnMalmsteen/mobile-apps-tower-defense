using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using BestHTTP;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class loginController : MonoBehaviour {

	public InputField usernameInput;
	public InputField passwordInput;
	public Text errorText;

	public string password = "";
	public string username = "";

	void Start(){
		passwordInput.onEndEdit.AddListener((value) => SubmitPass(value));
		usernameInput.onEndEdit.AddListener ((value) => SubmitUser (value));
	}

	private void SubmitPass(string value){
		this.password = value;
	}

	private void SubmitUser(string value){
		this.username = value;
	}

	public void login(){
		StartCoroutine (LogIn (username, password));
	}

	public IEnumerator LogIn(string user, string pass){
		string loginURI = "http://nebinstower.cloudapp.net:5000/login/";
		string jsonString = "{\"username\": \"" + user + "\", \"password\": \"" + pass + "\"}";

		HTTPRequest request = new HTTPRequest (new System.Uri (loginURI), HTTPMethods.Post);

		request.RawData = Encoding.UTF8.GetBytes(jsonString);
		request.AddHeader ("Content-Type", "application/json");

		request.Send();

		yield return StartCoroutine(request);
		Debug.Log (request.Response.DataAsText);
		if (request.Response.StatusCode == 200) {
			Debug.Log ("success");
			JObject o = JObject.Parse (request.Response.DataAsText);

			PlayerPrefs.SetString ("token", o ["token"].Value<string> ());
			PlayerPrefs.SetString ("username", user);

			errorText.text = "";

			loginComplete ();
		} else if (request.Response.StatusCode == 404) {
			string registerURI = "http://nebinstower.cloudapp.net:5000/register/";
			request = new HTTPRequest (new System.Uri (registerURI), HTTPMethods.Post);

			request.RawData = Encoding.UTF8.GetBytes (jsonString);
			request.AddHeader ("Content-Type", "application/json");

			request.Send ();

			yield return StartCoroutine (request);
			Debug.Log (request.Response.DataAsText);
			JObject o = JObject.Parse (request.Response.DataAsText);

			PlayerPrefs.SetString ("token", o ["token"].Value<string> ());
			PlayerPrefs.SetString ("username", user);
			errorText.text = "";

			loginComplete ();
		} else {
			errorText.text = request.Response.DataAsText;
		}
	}

	public void loginComplete(){
		CanvasGroup thisCanvas = GameObject.Find ("loginPanel").GetComponent<CanvasGroup> ();
		thisCanvas.alpha = 0;
		thisCanvas.interactable = false;
		thisCanvas.blocksRaycasts = false;

		CanvasGroup saveLoadCanvas = GameObject.Find ("saveLoadPanel").GetComponent<CanvasGroup> ();
		saveLoadCanvas.alpha = 1;
		saveLoadCanvas.interactable = true;
		saveLoadCanvas.blocksRaycasts = true;
	}

	public void save(){
		StartCoroutine (saveCharacters());
	}

	public void load(){
		StartCoroutine (loadCharacters ());
	}

	public IEnumerator loadCharacters(){
		string loadURL = "http://nebinstower.cloudapp.net:5000/loadCharacters/";

		string token = PlayerPrefs.GetString ("token");
		string user = PlayerPrefs.GetString ("username");

		string jsonString = "{\"username\": \"" + user + "\", \"token\": \"" + token + "\"}";

		HTTPRequest request = new HTTPRequest (new System.Uri (loadURL), HTTPMethods.Post);

		request.RawData = Encoding.UTF8.GetBytes (jsonString);
		request.AddHeader ("Content-Type", "application/json");
		request.Send ();

		yield return StartCoroutine (request);

		Debug.Log (request.Response.DataAsText);

		Fighter f = JsonConvert.DeserializeObject<Fighter> (request.Response.DataAsText);

	}

	public IEnumerator saveCharacters(){
		Fighter f = new Fighter ();

		string saveURL = "http://nebinstower.cloudapp.net:5000/saveCharacters/";

		string token = PlayerPrefs.GetString ("token");
		string user = PlayerPrefs.GetString ("username");

		string jsonString = "{\"username\": \"" + user + "\", \"token\": \"" + token + "\", \"savedata\": " + JsonConvert.SerializeObject (f) + "}";

		HTTPRequest request = new HTTPRequest (new System.Uri (saveURL), HTTPMethods.Post);

		request.RawData = Encoding.UTF8.GetBytes (jsonString);
		request.AddHeader ("Content-Type", "application/json");
		request.Send ();

		yield return StartCoroutine (request);

		Debug.Log (request.Response.DataAsText);
	}

	public void returnToMain(){
		Application.LoadLevel ("main");
	}
}
