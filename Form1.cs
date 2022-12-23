using System;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Policy;
using HtmlAgilityPack;

namespace Fotothing
{
    public partial class FotothingHarvester : Form
    {
        //System.Collections.Generic.IEnumerable<string> AuthCookies;
        CookieContainer cookieContainer = new CookieContainer();
        List<string> linkList = new List<string>();
        string BaseDir = "";
        int completed = 0;
        bool stilltoharvest = true;
        public FotothingHarvester()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        /*
        private void HarvestImages(List<string> links)
        {
            List<string> TryAgainList = new List<string>();
            string BaseDir = @"C:\temp\Fotothing\";
            var handler = new HttpClientHandler
            {
                UseCookies = true
            };
            handler.CookieContainer = cookieContainer;

            using (HttpClient client = new HttpClient(handler))
                foreach (var link in links)
                {

                    var imagename = link.Substring(link.LastIndexOf("/") + 1);
                    var fileName = BaseDir + imagename;
                    Task<HttpResponseMessage> responseTask = client.GetAsync(link);
                    responseTask.Wait();
                    HttpResponseMessage response = responseTask.Result;
                    Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
                    streamTask.Wait();
                    Stream stream = streamTask.Result;
                    bool fail = false;
                    using (stream)
                    using (FileStream fileStream = File.Open(fileName, FileMode.Create))
                    {
                        stream.CopyTo(fileStream);
                        if (fileStream.Length < 300)
                        {
                            fail = true;
                        }
                    }
                    if (fail)
                    {
                        TryAgainList.Add(link);
                        File.Delete(fileName);
                    }
                    
                }
            if (TryAgainList.Count > 0)
                HarvestImages(TryAgainList);

        }
        */
        private async void HarvestImages(List<string> links)
        {
            List<string> TryAgainList = new List<string>();
            //string BaseDir = @"C:\temp\Fotothing\";
            var handler = new HttpClientHandler
            {
                UseCookies = true
            };
            handler.CookieContainer = cookieContainer;
            var linkcount = 0;
            using (HttpClient client = new HttpClient(handler))
            {
                //ParallelOptions options = new ParallelOptions
                //{
                //    MaxDegreeOfParallelism = 2
                //};
                
                foreach (var link in links)
                {
                    linkcount++;
                    var imagename = link.Substring(link.LastIndexOf("/") + 1);
                    var fileName = BaseDir + imagename;
                    if (!File.Exists(fileName))
                    {
                        try
                        {
                            HttpResponseMessage response = await client.GetAsync(link);
                            Stream stream = await response.Content.ReadAsStreamAsync();
                            bool fail = false;
                            using (stream)
                            using (FileStream fileStream = File.Open(fileName, FileMode.Create))
                            {
                                await stream.CopyToAsync(fileStream);
                                if (fileStream.Length < 300)
                                {
                                    fail = true;
                                }
                                else
                                {
                                    completed++;
                                }
                            }
                            if (fail)
                            {
                                lock (TryAgainList)
                                {
                                    TryAgainList.Add(link);
                                    File.Delete(fileName);
                                }

                            }
                        }
                        catch
                        {
                            lock (TryAgainList)
                            {
                                File.Delete(fileName);
                                TryAgainList.Add(link);
                            }
                        }
                    }
                    else
                    {
                        completed++;
                    }
                    progressBar1.Value = (int)(((decimal)completed / (decimal)linkList.Count) * 100);
                    if (completed % 100 == 0 )
                    {
                        SetStatus(completed.ToString() + " photos downloaded - " + (((double)completed / linkList.Count)).ToString("P"));
                    }
                }
            }
            if (TryAgainList.Count > 0)
                HarvestImages(TryAgainList);
            if (completed < links.Count)
            {
                SetStatus("Need to cycle through again and catch some that failed.");
                stilltoharvest = true;
            }
            else
            {
                stilltoharvest = false;
                progressBar1.Value = 100;
                SetStatus("All photos harvested!");
            }

        }


        private void GetLinks()
        {
            linkList.Clear();
            int page = 0;
            while (true)
            {
                List<string> pagelinkList = new List<string>();
                page++;

                var handler = new HttpClientHandler
                {
                    UseCookies = true
                };
                handler.CookieContainer = cookieContainer;
                var client = new HttpClient(handler);
                string target = "http://www.fotothing.com/listing.php?page=" + page.ToString();
                var getresult = client.GetAsync(target).Result;
                if (getresult.IsSuccessStatusCode)
                {
                    var html = getresult.Content.ReadAsStringAsync().Result;
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    var links = doc.DocumentNode.SelectNodes("//a");

                    // Iterate through the list of anchor tags and retrieve the "href" attribute for each tag

                    foreach (var link in links)
                    {
                        pagelinkList.Add(link.Attributes["href"].Value);
                    }

                }
                foreach (var link in pagelinkList)
                {
                    if (link.StartsWith("http"))
                    {
                        linkList.Add(link);
                    }
                }
                if (page >1 && pagelinkList[1].ToLower().StartsWith("http") && !(pagelinkList[0].ToLower().StartsWith("http")))
                {
                    break;
                }
            }
        }

        private bool AuthtoFotothing()
        {
            // Create a new HttpClient object to send the POST request
            var client = new HttpClient();

            // Create a new HttpContent object with the data to send in the POST request
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", UserTxt.Text.Trim()),
                new KeyValuePair<string, string>("pass", PwdTxt.Text.Trim()),
            });

            // Send the POST request
            var response = client.PostAsync("http://www.fotothing.com/login.php", content).Result;

            // Check the response status code to ensure that the POST request was successful
            if (response.IsSuccessStatusCode)
            {
                // Store the authentication cookies returned by the server
                var cookies = response.Headers.GetValues("Set-Cookie");
                foreach (var cookie in cookies)
                {
                    cookieContainer.SetCookies(new Uri("http://www.fotothing.com"), cookie);
                }

                return true;
                //MessageBox.Show("Authentication successful. Cookies: " + string.Join(", ", cookies));
            }
            else
            {
                MessageBox.Show("Authentication failed with status code: " + response.StatusCode);
                return false;
            }
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            if (UserTxt.Text.Trim().Length > 0 && PwdTxt.Text.Trim().Length > 0 && Directory.Exists(TargetFolder.Text))
                while (stilltoharvest)
                {
                    if (AuthtoFotothing())
                    {
                        SetStatus("Authenticated");
                        GetLinks();

                        if (linkList.Count() > 0)
                        {
                            SetStatus("There are " + linkList.Count + " photos to harvest");
                            HarvestImages(linkList);
                        }
                        else
                            SetStatus("Unable to find any photos to harvest");

                    }
                }
        }

        private void SetStatus(string v)
        {
           var now= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            StatusBox.Text += now + " - " + v+Environment.NewLine;
            StatusBox.Select(StatusBox.Text.Length, 0);
            StatusBox.ScrollToCaret();
        }

        private void TargetFolder_MouseDown(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                InitialDirectory = @"C:\"
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                BaseDir = folderBrowserDialog.SelectedPath + @"\";
            }
            TargetFolder.Text = BaseDir;

        }
    }
}