/*using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using WindowsInput;
using MakVerse;
public class CSharpForGIT : MonoBehaviour
{
    public Makscript ML;
    private string arr;
    private Vector3 v; 
    private int i=0;
    string[] s;
    public float makTime = 0.269f;
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25002;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    Vector3 receivedPos;
    Vector3 pos;
    //Vector3 PrevPos = new Vector3(0f, 0f, 0f);
    bool running;
    InputSimulator Inkey = new InputSimulator();
    private void Update()

    {

        //   Debug.Log(s[0]);
        //  v = StringToVector3(s[0]);
        StartCoroutine(Simp());

    }

    private void Start()
    {
        arr = ML.file.ToString();
        s = arr.Split('\n');
        //Debug.Log("S of CGit");
       //Debug.Log(s[0].Substring(1,s[0].Length - 3)) ;
        //PrevPos = receivedPos;
         
        
    }

    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;
        while (running)
        {
            SendAndReceiveData();
        }
        listener.Stop();
    }

    void SendAndReceiveData()
    {
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---receiving Data from the Host----
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string

        if (dataReceived != null)
        {
            //---Using received data---
            Debug.Log(dataReceived);
            receivedPos = StringToVector3(dataReceived); //<-- assigning receivedPos value from Python
            print("received pos data, and moved the Cube!");

            //---Sending Data to Host----
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes("Hey I got your message Python! Do You see this massage?"); //Converting string to byte data
            nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python
        }
    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
       
        
        sVector = sVector.Substring(1, sVector.Length - 3);
        

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    IEnumerator Simp()
    {
        v = StringToVector3(s[i]);
        if (v == Vector3.forward)
        {

            Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_W);
      
        }                                                                      //assigning receivedPos in SendAndReceiveData()

        if (v == Vector3.right)
        {
            Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_W);
            ///Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_A);
            //Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.LEFT);
            //Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_A);
        }
        if (v == Vector3.up)
        {
            Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_W);
            //Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_D);
            //Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT);
            //Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT);
        }
        i += 1;
        if (i > 62)
            i = 0;

        yield return new WaitForSeconds(makTime);
        
    }
    /*
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    
}*/
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using WindowsInput;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class CSharpForGIT : MonoBehaviour
{
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    Vector3 v = Vector3.zero;
    InputSimulator Inkey = new InputSimulator();
    int i;
   
    int count = 50;
    bool running;

    public Image left;
    public Image forward;
    public Image right;

    public TextMeshProUGUI vectorsText;


    private void Update()
    {
        Debug.Log("At this frame V = " + v);

        
        if (v == Vector3.right)
        {
            vectorsText.text = v.ToString();
            Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_W);
            left.color = Color.white;
            right.color = Color.white;
            forward.color = Color.red;


        }                                                                      //assigning receivedPos in SendAndReceiveData()

        if (v == Vector3.up)
        {
            vectorsText.text = v.ToString();
            Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_W);
            Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_A);
            left.color = Color.red;
            right.color = Color.white;
            forward.color = Color.white;
        }
        if (v == Vector3.forward)
        {
            vectorsText.text = v.ToString();
            Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_W);
            Inkey.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_D);
            left.color = Color.white;
            right.color = Color.red;
            forward.color = Color.white;
        }
    }

    private void Start()
    {
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();

    }

    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;
        while (running)
        {
            SendAndReceiveData();
        }
        listener.Stop();
    }

    void SendAndReceiveData()
    {
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---receiving Data from the Host----
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        Debug.Log("Bytes are: " + bytesRead);

        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string
       

        if (dataReceived != null)
        {
            //---Using received data---
            

            v = StringToVector3(dataReceived);
            
            //<-- assigning receivedPos value from Python
            Debug.Log("data Recieved: " + v);
            
            //---Sending Data to Host----
            //byte[] myWriteBuffer = Encoding.ASCII.GetBytes("Hey I got your message Python! Do You see this massage?"); //Converting string to byte data
            //nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python
        }
        
    }

    public static Vector3 StringToVector3(string sVector)
    {

        Debug.Log("String to be converted into vector: " + sVector);
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
    /*
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    */
}