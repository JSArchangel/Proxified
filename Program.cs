using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Proxified
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // CONSOLE OPTIONS
            Console.Title = "Proxified";

            // DRIVE VARIABLES
            string[] driveArray = new string[20];
            string driveNames = String.Empty;
            string selectedDrive = String.Empty;
            int driveIndex = 0;
            bool isDriveValid = false;

            // PROXY VARIABLES
            string ipAddress = String.Empty;
            string portAddress = String.Empty;
            string country = String.Empty;
            string proxyType = String.Empty;
            int proxyPing = 0;

            // IO PERMISSIONS
            bool isPingGood = false;
            bool isProxyHTTP = false;
            bool isCountryValid = false;
            bool isIPAddressValid = false;
            bool isPortValid = false;

            // WEBSITE PAGEINDEX
            int pageIndex = 64;

            // PROXY-DATA-ARRAY
            string[] proxyDataArray = new string[10000];
            int proxyDataArrayIndex = 0;

            // SYSTEM CONFIGURATION VARIABLES
            int systemSpeed = 0;
            int scrapePage = 0;

            // LOGO OF PROGRAM
            Console.WriteLine(@"|###########################################|");
            Console.WriteLine(@"|    __   __   __         ___    ___  __    |");
            Console.WriteLine(@"|   |__) |__) /  \ \_/ | |__  | |__  |  \   |");
            Console.WriteLine(@"|   |    |  \ \__/ / \ | |    | |___ |__/   |");
            Console.WriteLine(@"|                                           |");
            Console.WriteLine(@"|###########################################|");

            // LINE SPACE
            Space();

            // TAKES DRIVER INFORMATION
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            // WRITES DRIVERS
            foreach (DriveInfo d in allDrives)
            {
                // SETS THE ARRAY & SETS THE WHOLE STRING
                driveArray[driveIndex] = d.Name.Substring(0, 2);
                driveNames += d.Name.Substring(0, 2) + " ";

                // INCREASES INDEX OF ARRAY
                driveIndex++;
            }

            // CLEARS THE LAST SPACE
            driveNames = driveNames.Substring(0, driveNames.Length - 1);

            // SETS THE DRIVE
            do
            {
                Console.Write($"Select Drive ({driveNames}) --> ");
                selectedDrive = Console.ReadLine();

                // LINE SPACE
                Space();

                for (int i = 0; i < driveArray.Length - 1; i++)
                {
                    if (selectedDrive == driveArray[i])
                    {
                        isDriveValid = true;
                        break;
                    }
                }
            } while (!isDriveValid);

            // PROXY LIST FILEPATH
            string directoryPath = selectedDrive + @"\Proxified";
            string filePath = selectedDrive + @"\Proxified\Proxylist.txt";

            // CREATES A DIRECTORY IF IT NOT EXISTS
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // CREATES A FILE IF IT NOT EXISTS
            if (!File.Exists(filePath))
            {
                var proxyListFile = File.Create(filePath);
                proxyListFile.Close();
            }

            // TAKES THE SPEED FROM THE USER
            Console.Write("Enter Scraping Speed (Preferred: 2000) --> ");
            systemSpeed = Convert.ToInt32(Console.ReadLine());

            // LINE SPACE
            Space();

            // TAKES THE SCRAPE PAGE SITE
            Console.Write("How Many Pages Do You Want To Scrape --> ");
            scrapePage = Convert.ToInt32(Console.ReadLine());

            // LINE SPACE
            Space();

            // FIREFOX DRIVER SERVICE
            FirefoxDriverService firefoxDriverService = FirefoxDriverService.CreateDefaultService();
            firefoxDriverService.HideCommandPromptWindow = true;

            // FIREFOX OPTIONS
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("--silent-launch");
            firefoxOptions.AddArgument("--disable-dev-shm-usage");
            firefoxOptions.AddArgument("--no-sandbox");
            firefoxOptions.AddArgument("--disable-impl-side-painting");
            firefoxOptions.AddArgument("--disable-setuid-sandbox");
            firefoxOptions.AddArgument("--disable-seccomp-filter-sandbox");
            firefoxOptions.AddArgument("--disable-breakpad");
            firefoxOptions.AddArgument("--disable-client-side-phishing-detection");
            firefoxOptions.AddArgument("--disable-cast");
            firefoxOptions.AddArgument("--disable-cast-streaming-hw-encoding");
            firefoxOptions.AddArgument("--disable-cloud-import");
            firefoxOptions.AddArgument("--disable-popup-blocking");
            firefoxOptions.AddArgument("--ignore-certificate-errors");
            firefoxOptions.AddArgument("--disable-session-crashed-bubble");
            firefoxOptions.AddArgument("--disable-ipv6");
            firefoxOptions.AddArgument("--allow-http-screen-capture");
            firefoxOptions.AddArgument("--start-maximized");

            // FIREFOX DRIVER
            FirefoxDriver firefoxDriver = new FirefoxDriver(firefoxDriverService, firefoxOptions);

            // OPEN THE PROXY-SCRAPING WEBSITE
            firefoxDriver.Navigate().GoToUrl("https://hidemy.name/en/proxy-list/#list");

            // WAITS FOR THE WEBSITE
            Thread.Sleep(systemSpeed);

            // ENTERS THE SYSTEM
            for (int i = 0; i < scrapePage; i++)
            {
                // 64 TIMES CHECK & WRITE SYSTEM FOR EVERY PAGE
                for (int pI = 1; pI <= 64; pI++)
                {
                    try
                    {
                        // CHECKSUM FOR PING
                        proxyPing = Convert.ToInt32(firefoxDriver.FindElement(By.XPath($"/html/body/div[1]/div[4]/div/div[4]/table/tbody/tr[{pI}]/td[4]/div/p")).Text.Substring(0, firefoxDriver.FindElement(By.XPath($"/html/body/div[1]/div[4]/div/div[4]/table/tbody/tr[{pI}]/td[4]/div/p")).Text.Length - 3));

                        // SET PING WRITE PERMISSIONS
                        if (proxyPing < 1000)
                        {
                            isPingGood = true;
                        }
                        else
                        {
                            isPingGood = false;
                        }

                        // CHECKSUM FOR TYPE
                        proxyType = firefoxDriver.FindElement(By.XPath($"/html/body/div[1]/div[4]/div/div[4]/table/tbody/tr[{pI}]/td[5]")).Text;

                        // SET TYPE WRITE PERMISSIONS
                        if (proxyType.Contains("HTTP"))
                        {
                            isProxyHTTP = true;
                        }
                        else
                        {
                            isProxyHTTP = false;
                        }

                        // COUNTRY SET
                        country = firefoxDriver.FindElement(By.XPath($"/html/body/div[1]/div[4]/div/div[4]/table/tbody/tr[{pI}]/td[3]/span[1]")).Text;

                        // SET COUNTRY WRITE PERMISSIONS
                        if (country != "")
                        {
                            isCountryValid = true;
                        }
                        else
                        {
                            isCountryValid = false;
                        }

                        // IP ADDRESS SET
                        ipAddress = firefoxDriver.FindElement(By.XPath($"/html/body/div[1]/div[4]/div/div[4]/table/tbody/tr[{pI}]/td[1]")).Text;

                        // SET IP WRITE PERMISSIONS
                        if (ipAddress != "")
                        {
                            isIPAddressValid = true;
                        }
                        else
                        {
                            isIPAddressValid = false;
                        }

                        // PORT ADDRESS SET
                        portAddress = firefoxDriver.FindElement(By.XPath($"/html/body/div[1]/div[4]/div/div[4]/table/tbody/tr[{pI}]/td[2]")).Text;

                        // SET PORT WRITE PERMISSIONS
                        if (portAddress != "")
                        {
                            isPortValid = true;
                        }
                        else
                        {
                            isPortValid = false;
                        }

                        // ADDS THE DATA TO ARRAY
                        if (isPingGood && isProxyHTTP && isCountryValid && isIPAddressValid && isPortValid)
                        {
                            proxyDataArray[proxyDataArrayIndex] = $"{ipAddress}#{portAddress}#{country}";
                            proxyDataArrayIndex++;
                        }
                    }
                    catch (Exception ex)
                    {
                        // WRITES THE ERROR MESSAGE
                        Console.WriteLine(ex.Message);

                        // LINE SPACE
                        Space();

                        // CONTINUES TO THE LOOP
                        continue;
                    }
                }

                // WRITES ALL THE LINES AFTER READING & PROCESSING 64 LINES OF PROXY
                if (i == scrapePage - 1)
                {
                    File.WriteAllLines(filePath, proxyDataArray);
                    break;
                }

                // GOES FOR ANOTHER PAGE
                firefoxDriver.Navigate().GoToUrl($"https://hidemy.name/en/proxy-list/?start={pageIndex}#list");
                pageIndex = pageIndex + 64;

                // WAITS FOR THE NEW WEBPAGE
                Thread.Sleep(systemSpeed);
            }

            // WAITS FOR THE SYSTEM CLOSE BY CLIENT
            Console.ReadLine();
        }

        private static void Space()
        {
            Console.WriteLine("");
        }
    }
}