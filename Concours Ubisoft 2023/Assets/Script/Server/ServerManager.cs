using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class ServerManager : MonoBehaviour
{

    public static void sendScore(string username,string score)
    {

        var url = "http://concours-ubisoft-2.dinf.usherbrooke.ca:8081/LEADERBOARD/SEND_SCORE";
        var request = WebRequest.Create(url);
        request.Method = "POST";
        //rempalcer par des string builder
        string formContent = "pseudo=" + username +
        "&score=" + score;

        byte[] byteArray = Encoding.UTF8.GetBytes(formContent);
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;

        using var reqStream = request.GetRequestStream();
        reqStream.Write(byteArray, 0, byteArray.Length);

        using var webResponse = request.GetResponse();
        using var webStream = webResponse.GetResponseStream();

        using var reader = new StreamReader(webStream);
        var data = reader.ReadToEnd();
        Debug.Log(data);
    }

    public Player[] getLeaderBoard()
    {
        var url = "http://concours-ubisoft-2.dinf.usherbrooke.ca:8081/LEADERBOARD/DISPLAY_10_FIRST_SCORES";
        var request = WebRequest.Create(url);
        request.Method = "GET";

        using var webResponse = request.GetResponse();
        using var webStream = webResponse.GetResponseStream();

        using var reader = new StreamReader(webStream);
        var data = reader.ReadToEnd();
        Debug.Log(data);
        var leaderboard = JsonConvert.DeserializeObject<Player[]>(data);


        return leaderboard;


    }

}
