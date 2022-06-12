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
}
