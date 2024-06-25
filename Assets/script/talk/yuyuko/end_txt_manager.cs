using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class end_txt_manager : MonoBehaviour
{
    public TextAsset txt;
    string[,] Sentence;
    int rowSize, colSize;

    public Text Name;
    public Text chat;
    public int currentLine = 0;
    public reimu_ani reimu;
    public GameObject re;
    public marisa_ani marisa;
    public GameObject ma;
    
    public reimu_ani remilia;
    public GameObject remilia_;
    
    public GameObject flash;
    
    
    public GameObject yuyuko_bg;
    public GameObject real_bg;
    
    public GameObject Loading;
    public GameObject credit;

    private bool next = false;
    private bool can_talk = true;
    

    // Start is called before the first frame update
    void Start()
    {
        GAMEMANAGER.instance.game_start = false;
        Name.GetComponent<Text>();
        chat.GetComponent<Text>();

        string currentText = txt.text.Trim(); 
        string[] lines = currentText.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries); 
        
        rowSize = lines.Length;
        colSize = lines[0].Split('\t').Length;

        Sentence = new string[rowSize, colSize];


        for (int i = 0; i < rowSize; i++)
        {
            string[] columns = lines[i].Split('\t');
            for (int j = 0; j < colSize; j++)
            {
                if (j < columns.Length)
                {
                    Sentence[i, j] = columns[j];
                }
                else
                {
                    Sentence[i, j] = "";
                }
                Debug.Log(i + "," + j + "," + Sentence[i, j]);
            }
        }
        strar_chat();
    }

    void strar_chat(){
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currentLine < rowSize)
        {
            Soundmanager.Instance.Playsound("btn_choice");
            Name.text = Sentence[currentLine, 1];
            chat.text = Sentence[currentLine, 2];
        }

        if(Sentence[currentLine,3]=="1"){
            re.SetActive(true);
        }
        if(Sentence[currentLine,4]=="1"){
            ma.SetActive(true);
        }
        if(Sentence[currentLine,5]=="1"){
            BGMmanager.Instance.Playsound("hakurei");
        }
        else if(Sentence[currentLine,5]=="2"){
            can_talk = false;
        }
        if(Sentence[currentLine,6]=="1")//레이무
            reimu.ani = 1;
        else if(Sentence[currentLine,6]=="2")
            reimu.ani = 2;
        else if(Sentence[currentLine,6]=="3")
            reimu.ani = 3;
        else if(Sentence[currentLine,6]=="4")
            reimu.ani = 4;
        
        if(Sentence[currentLine,7]=="1")//마리사
            marisa.ani = 1;
        else if(Sentence[currentLine,7]=="2")
            marisa.ani = 2;
        else if(Sentence[currentLine,7]=="3")
            marisa.ani = 3;
        else if(Sentence[currentLine,7]=="4")
            marisa.ani = 4;
        else if(Sentence[currentLine,7]=="5")
            marisa.ani = 5;
        else if(Sentence[currentLine,7]=="6")
            marisa.ani = 6;
        else if(Sentence[currentLine,7]=="7")
            marisa.ani = 7;

        if(Sentence[currentLine,9]=="1"){
            remilia.ani = 1;
        }
        else if(Sentence[currentLine,9]=="2"){
            remilia_.SetActive(true);
            remilia.ani = 2;
        }
        else if(Sentence[currentLine,9]=="3"){
            remilia.ani = 3;
        }
        else if(Sentence[currentLine,9]=="4"){
            remilia.ani = 4;
        }
        
        if(Sentence[currentLine,8]=="1" && next == false){
            StartCoroutine(next_scence());
            next = true;
        }
        if(Sentence[currentLine,8]=="3"){
            can_talk = false;
            flash.SetActive(true);
            StartCoroutine(next_talk());
        }
        if(Sentence[currentLine,8]=="4"){
            BGMmanager.Instance.Playsound("end_bad_apple");
            credit.SetActive(true);
        }
            
        if(Sentence[currentLine+1,2]!="end"){
            currentLine++;
        }
    }

    IEnumerator next_scence(){
        yield return new WaitForSeconds(2);
        Loading.SetActive(true);
    }
    IEnumerator next_talk(){
        yield return new WaitForSeconds(2);
        real_bg.SetActive(true);
        yuyuko_bg.SetActive(false);
        DisplayNextSentence();
        yield return new WaitForSeconds(2);
        can_talk = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)  && can_talk)
        {
            DisplayNextSentence();
        }
    }
}