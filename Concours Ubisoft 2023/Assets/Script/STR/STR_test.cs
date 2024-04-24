using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class STR_test : MonoBehaviour
{
    // Start is called before the first frame update
    public ProceduralManager proceduralManager;
    private float startTime;
    void Start()
    {
        StartCoroutine(test());
       


        
    }

    public static void WriteString(string t)

    {

        string path = "C:/CanadaCours/Sission Hiver/test/text.txt";

        //Write some text to the test.txt file

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine(t);

        writer.Close();

        /*StreamReader reader = new StreamReader(path);

        //Print the text from the file

        Debug.Log(reader.ReadToEnd());

        reader.Close();*/

    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 1000; i++)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            proceduralManager.load(new Vector3(0, 0, 0), true);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Debug.Log(elapsedMs);
            WriteString(elapsedMs.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
