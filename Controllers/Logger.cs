namespace E_HousingAutomation.Controllers
{
    public class Logger
    {
        //log file path and dynamic name
        string _Path = @".\Log\";
        string _Filename = DateTime.Now.ToString("yyyyMMdd");

        public void createLog(string message)
        {
            FileStream fs = new FileStream(_Path + _Filename + ".txt",
                                            FileMode.Append,
                                            FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(message + "\tTime: " + DateTime.Now.ToLongTimeString());


            sw.Flush();
            sw.Close();
            fs.Close();


        }
    }
}
