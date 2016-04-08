using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Threading;

namespace AutoUpdate
{
    class AutoUpdater
    {

        public static WebClient Client
        {
            get { return new WebClient(); }
        }
        public readonly static Stream VeriCek = Client.OpenRead("https://raw.githubusercontent.com/tradana/TRAdanabot/master/version.txt");
         public readonly static StreamReader VeriOkuyucu = new StreamReader(VeriCek);
         public readonly static string Veri = VeriOkuyucu.ReadToEnd();
         public readonly static int VeriInt = Convert.ToInt32(Veri);
         public static readonly string dosyaUrl = "";
        //Bu kısmın altındaki programSurumu'nu programı her güncellediğinde değiştir 1 arttır mesela sonraki
        //güncellemede 1001 yap diğerinde 1002 yap...
        public static int ProgramSurumu
        {
            get { return 1000; }
        }

        public static bool GuncellemeKontrol()
        {
            try
            {
                if (VeriInt > ProgramSurumu)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Guncelle()
        {
            string lokasyon = Environment.CurrentDirectory;
            string lokasyonGuncelDosya = lokasyon + "TRadanaBotYeni.exe";

            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += Completed;
            webClient.DownloadFileAsync(new Uri(dosyaUrl), lokasyonGuncelDosya);
        }

        public static void Completed(object sender, AsyncCompletedEventArgs e)
        {
            string lokasyon = Environment.CurrentDirectory;
            string lokasyonGuncelDosya = lokasyon + "TRadanaBotYeni.exe";

            Console.WriteLine("Guncelleme Tamamlandi!");
            Process.Start(lokasyonGuncelDosya);
            Environment.Exit(0);
        }

        public static void Basla()
        {
            if (GuncellemeKontrol())
            {
                Guncelle();
            }
            else
            {
                Console.WriteLine("Surum Guncel");
                Thread.Sleep(3000);
            }
        }
    }
}
