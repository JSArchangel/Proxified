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
            string[] driveArray = new string[50];
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
            int totalProxy = 0;
            int totalPage = 0;

            // IO PERMISSIONS
            bool isPingGood = false;
            bool isProxyHTTP = false;
            bool isCountryValid = false;
            bool isIPAddressValid = false;
            bool isPortValid = false;

            // WEBSITE PAGEINDEX
            int pageIndex = 64;

            // PROXY-DATA-ARRAY
            List<string> proxyDataArray = new List<string>();

            // SYSTEM CONFIGURATION VARIABLES
            int systemSpeed = 0;
            int scrapePage = 0;

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
            firefoxOptions.AddArgument("--headless");

            // LOGO OF PROGRAM
            Console.WriteLine(@"|###########################################|");
            Console.WriteLine(@"|    __   __   __         ___    ___  __    |");
            Console.WriteLine(@"|   |__) |__) /  \ \_/ | |__  | |__  |  \   |");
            Console.WriteLine(@"|   |    |  \ \__/ / \ | |    | |___ |__/   |");
            Console.WriteLine(@"|                                           |");
            Console.WriteLine(@"|###########################################|");

            // LINE SPACE
            Space();

            // INFORMATION LOG
            Console.WriteLine("|######################################|");
            Console.WriteLine("| Please Wait For The Proxified Driver |");
            Console.WriteLine("|######################################|");

            // LINE SPACE
            Space();

            // FIREFOX DRIVER
            FirefoxDriver firefoxDriver = new FirefoxDriver(firefoxDriverService, firefoxOptions);

            // INFORMATION LOG
            Console.WriteLine("|###########################|");
            Console.WriteLine("| Proxified Driver Executed |");
            Console.WriteLine("|###########################|");

            // LINE SPACE
            Space();

            // OPEN THE PROXY-SCRAPING WEBSITE
            firefoxDriver.Navigate().GoToUrl("https://hidemy.name/en/proxy-list/#list");

            // INFORMATION LOG
            Console.WriteLine("|#############################|");
            Console.WriteLine("| Proxified Driver Configured |");
            Console.WriteLine("|#############################|");

            // LINE SPACE
            Space();

            // CHEKS THE MAX PAGE
            totalPage = Convert.ToInt32(firefoxDriver.FindElement(By.XPath("/html/body/div[1]/div[4]/div/div[5]/ul/li[9]/a")).Text);


            // INFORMATION LOG
            Console.WriteLine("|##################################|");
            Console.WriteLine("| Proxified Driver Service Started |");
            Console.WriteLine("|##################################|");

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

                // SETS THE STRING TO UPPER
                selectedDrive = selectedDrive.ToUpper();

                // ADDS THE COLON IF IT DOESN'T CONTAINS
                if (!selectedDrive.Contains(":"))
                {
                    selectedDrive += ":";
                }

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

            // EXCEPTION HANDLING FOR FILE CREATION
            try
            {
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
            }
            catch (Exception ex)
            {
                // WRITES THE ERROR LOG
                Console.WriteLine(ex.Message);

                // LINE SPACE
                Space();
            }

            // TAKES THE SPEED FROM THE USER
            Console.Write("Scraping Speed (Preferred: 2000) --> ");
            systemSpeed = Convert.ToInt32(Console.ReadLine());

            // LINE SPACE
            Space();

            // SETS THE SCRAPE PAGE QUANTITY
            do
            {
                // TAKES THE SCRAPE PAGE QUANTITY
                Console.Write($"How Many Pages Do You Want To Scrape (MAX: {totalPage}) --> ");
                scrapePage = Convert.ToInt32(Console.ReadLine());

                // LINE SPACE
                Space();
            } while (scrapePage >= totalPage);

            // WRITES SYSTEM START LOG
            Console.WriteLine("|###################|");
            Console.WriteLine("| Operation Started |");
            Console.WriteLine("|###################|");

            // LINE SPACE
            Space();

            // ENTERS THE SYSTEM
            for (int i = 0; i < scrapePage; i++)
            {
                // WRITES THE PROXY PROCESS LOG
                Console.WriteLine("- 64 Proxy Processed");

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
                            proxyDataArray.Add($"{ipAddress}#{portAddress}#{country}");
                            totalProxy++;
                        }
                    }
                    catch
                    {
                        // CONTINUES TO THE LOOP
                        continue;
                    }
                }

                // WRITES ALL THE LINES AFTER READING & PROCESSING PROXIES
                if (i == scrapePage - 1)
                {
                    // EXCEPTION HANDLING FOR FILE WRITING
                    try
                    {
                        File.WriteAllLines(filePath, proxyDataArray);
                        firefoxDriver.Dispose();
                        break;
                    }
                    catch (Exception ex)
                    {
                        // WRITES THE ERROR LOG
                        Console.WriteLine(ex.Message);
                    }
                }

                // GOES FOR ANOTHER PAGE
                firefoxDriver.Navigate().GoToUrl($"https://hidemy.name/en/proxy-list/?start={pageIndex}#list");
                pageIndex = pageIndex + 64;

                // WAITS FOR THE NEW WEBPAGE
                Thread.Sleep(systemSpeed);
            }

            // LINE SPACE
            Space();

            // WRITES TOTAL GATHERED PROXY
            Console.WriteLine($"Gathered Proxy --> {totalProxy} Out Of {pageIndex}");

            // LINE SPACE
            Space();

            // SYSTEM FINISHED LOG
            Console.WriteLine("|####################|");
            Console.WriteLine("| Operation Finished |");
            Console.WriteLine("|####################|");

            // WAITS AND CLOSES THE SYSTEM
            Thread.Sleep(4000);
            Environment.Exit(0);

            // WAITS FOR THE SYSTEM CLOSE BY CLIENT
            Console.ReadLine();
        }

        private static void Space()
        {
            Console.WriteLine("");
        }
    }
}