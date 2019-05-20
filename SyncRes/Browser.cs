using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace SyncRes {
    class Browser {
        ChromiumWebBrowser browser;
        static bool isInitialized = false;
        bool webDocReadMode = false;
        private string currentUrl;
        bool isReading = false;

        public Browser(string url) {
            if(!isInitialized) {
                var settings = new CefSettings();
                settings.Locale = "ja";
                settings.AcceptLanguageList = "ja-JP";
                Cef.Initialize(settings);
                isInitialized = true;
            }
            browser = new ChromiumWebBrowser("https://lounge.synchronica.jp/");
            browser.FrameLoadEnd += (sender, e) => isReading = false;
        }
        
        public static void Shutdown() {
            Cef.Shutdown();
        }

        /// <summary>
        /// フォームコントロールを返す
        /// </summary>
        public Control GetControl() {
            return browser;
        }

        /// <summary>
        /// 表示しているサイトのURLを返す
        /// </summary>
        /// <returns></returns>
        public string GetURL() {
            return browser.Address;
        }

        /// <summary>
        /// 引数のサイトのHTMLを返す
        /// </summary>
        public async Task<string> ReadDocument(string url) {
            browser.Load(url);
            await WaitForDocumentDownload(500);
            if(isReading) {
                browser.Load(url);
                await WaitForDocumentDownload(5000);
                if(isReading) {
                    throw new TimeoutException("タイムアウトしました");
                }
            }
            return await browser.GetSourceAsync();
        }

        private async Task WaitForDocumentDownload(int timeout) {
            isReading = true;
            while(isReading) {
                await Task.Delay(33);
                if(timeout-- <= 0) {
                    break;
                }
            }
            await Task.Delay(33);
            return;
        }

        /// <summary>
        /// 連続読み込みモード(サイトを表示しない分高速)にする
        /// </summary>
        /// <param name="currentUrl">連続読み込みモード終了後に移動するサイトのURL</param>
        public void BeginDocReadMode(string currentUrl = null) {
            if(!webDocReadMode) {
                webDocReadMode = true;
                this.currentUrl = currentUrl;
                browser.Visible = false;
            }
        }

        /// <summary>
        /// 連続読み込みモードをやめる
        /// </summary>
        public void EndDocReadMode() {
            if(webDocReadMode) {
                webDocReadMode = false;
                if(currentUrl != null) browser.Load(currentUrl);
                browser.Visible = true;
            }
        }

        /// <summary>
        /// サイトの移動が終わった時に呼ばれる処理
        /// </summary>
        public event EventHandler OnReadEnded {
            add {
                Action action = () => value(null, null);
                browser.AddressChanged += (sender, e) => browser.BeginInvoke(action);
            }
            remove {
                Action action = () => value(null, null);
                browser.AddressChanged -= (sender, e) => browser.BeginInvoke(action);
            }
        }
    }
}
